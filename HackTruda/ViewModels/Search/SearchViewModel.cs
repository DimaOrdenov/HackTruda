using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HackTruda.BusinessLogic.Interfaces;
using HackTruda.Definitions;
using HackTruda.Definitions.Enums;
using HackTruda.Extensions;
using HackTruda.Services.Interfaces;

namespace HackTruda.ViewModels.Search
{
    public class SearchViewModel : PageViewModel
    {
        private readonly IUsersLogic _usersLogic;

        private ObservableCollection<CategoryItemViewModel> _categories;

        public ICommand CategoryTapCommand { get; }

        public SearchViewModel(
            INavigationService navigationService,
            IDialogService dialogService,
            IDebuggerService debuggerService,
            IUsersLogic usersLogic)
            : base(navigationService, dialogService, debuggerService)
        {
            _usersLogic = usersLogic;

            CategoryTapCommand = BuildPageVmCommand<CategoryItemViewModel>(item =>
                NavigationService.NavigateAsync(item.Type == CategoryType.Places ? PageType.MapCategoryPage : PageType.CategoryPage, item));
        }

        public ObservableCollection<CategoryItemViewModel> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        public override async Task OnAppearing()
        {
            if (PageDidAppear)
            {
                return;
            }

            State = PageStateType.Loading;

            await ExceptionHandler.PerformCatchableTask(
                new ViewModelPerformableAction(
                    async () =>
                    {
                        ObservableCollection<CategoryItemViewModel> categories = new ObservableCollection<CategoryItemViewModel>();

                        foreach (CategoryType category in
                            new List<CategoryType>
                            {
                                CategoryType.People,
                                CategoryType.Places,
                                CategoryType.Events,
                                CategoryType.Ads,
                                CategoryType.Games,
                                CategoryType.Podcasts,
                                CategoryType.Recipes,
                            })
                        {
                            categories.Add(new CategoryItemViewModel(category, await GetSubCategories(category)));
                        }

                        Categories = categories;
                    }));

            State = PageStateType.Default;

            await base.OnAppearing();
        }

        private async Task<IEnumerable<SubCategoryItemViewModel>> GetSubCategories(CategoryType parentCategory)
        {
            List<SubCategoryItemViewModel> result = new List<SubCategoryItemViewModel>();

            List<string> titles = new List<string>();
            List<string> descriptions = new List<string>();

            switch (parentCategory)
            {
                case CategoryType.People:
                    titles = (await _usersLogic.Get(CancellationToken))
                        .Select(x => $"{x.FirstName} {x.LastName}")
                        .ToList();

                    for (int i = 0; i < titles.Count; i++)
                    {
                        result.Add(new SubCategoryItemViewModel(
                            i % 2 == 0 ? CategoryType.PeopleCity : CategoryType.PeopleCountry,
                            parentCategory,
                            titles.ElementAtOrDefault(i),
                            (i % 2 == 0 ? "Таганрог" : "Новосибирск") + ", из России"));
                    }

                    break;
                case CategoryType.Places:
                    titles = new List<string>
                    {
                        "Музей",
                        "Цирк",
                        "Концертный зал",
                        "Кинотеатр",

                        "Ресторан",
                        "Кафе",
                        "Бар",

                        "Рынок",
                        "Продуктовый магазин",
                        "Ярмарка",
                    };

                    descriptions = new List<string>
                    {
                        "Москва, Полковая улица, 3с2",
                        "Москва, Проспект Мира, 72",
                        "Москва, ВДНХ",
                        "Москва, Остаповский проезд, 16с4",

                        "Москва, Угрешская улица, 2с57",
                        "Москва, 2-й Автозаводский проезд, 3А",
                        "Москва, Автозаводская улица, 22",

                        "Москва, 3-й Павелецкий проезд, 11",
                        "Москва, Большая Тульская улица, 15",
                        "Москва, Донская улица, 43с7",
                    };

                    for (int i = 0; i < titles.Count; i++)
                    {
                        CategoryType category =
                            i < 4 ? CategoryType.PlacesFun :
                                i < 7 ? CategoryType.PlacesEat :
                                    CategoryType.PlacesFood;

                        result.Add(new SubCategoryItemViewModel(
                            category,
                            parentCategory,
                            titles.ElementAtOrDefault(i),
                            descriptions.ElementAtOrDefault(i)));
                    }

                    break;
                case CategoryType.Events:
                    titles = new List<string>
                    {
                        "День рождения Вовы",
                        "День рождения Димы",
                        "День рождения Кати",
                        "День рождения собаки",

                        "Выставка на 5ой улице",
                        "Концерт Би-2",
                        "Спектакль",

                        "Пасха",
                        "День города",
                        "Хороший праздник",
                    };

                    for (int i = 0; i < titles.Count; i++)
                    {
                        CategoryType category =
                            i < 4 ? CategoryType.EventsHappyBirthday :
                                i < 7 ? CategoryType.EventsKulture :
                                    CategoryType.EventsCelebration;

                        result.Add(new SubCategoryItemViewModel(
                            category,
                            parentCategory,
                            titles.ElementAtOrDefault(i),
                            category.GetEnumDescription()));
                    }

                    break;
                case CategoryType.Ads:
                    titles = new List<string>
                    {
                        "Автомобиль, 10000 руб/мес",
                        "Красная машина, 15000 руб/мес",
                        "2-к квартира, 25000 руб/мес",
                        "Велосипед, 2000 руб/день",

                        "Разнорабочий",
                        "Программист",
                        "Слесарь 5-го разряда",

                        "Парикмахерская",
                        "Маникюр",
                        "Массаж",
                    };

                    for (int i = 0; i < titles.Count; i++)
                    {
                        CategoryType category =
                            i < 4 ? CategoryType.AdsRent :
                                i < 7 ? CategoryType.AdsWork :
                                    CategoryType.AdsService;

                        result.Add(new SubCategoryItemViewModel(
                            category,
                            parentCategory,
                            titles.ElementAtOrDefault(i),
                            category.GetEnumDescription()));
                    }

                    break;
                case CategoryType.Games:
                    titles = new List<string>
                    {
                        "Марио",
                        "Гонки",
                        "Гонки 2 часть",
                        "Counter-Strike",

                        "Зарядка для мозга",
                        "Кроссворды",
                        "Викторина для всей компании",

                        "Шарики и квадратики",
                        "На логику",
                        "Детектив",
                    };

                    for (int i = 0; i < titles.Count; i++)
                    {
                        CategoryType category =
                            i < 4 ? CategoryType.GamesArcade :
                                i < 7 ? CategoryType.GamesQuiz :
                                    CategoryType.GamesPuzzle;

                        result.Add(new SubCategoryItemViewModel(
                            category,
                            parentCategory,
                            titles.ElementAtOrDefault(i),
                            category.GetEnumDescription()));
                    }

                    break;
                case CategoryType.Podcasts:
                    titles = new List<string>
                    {
                        "Первый раз в стране",
                        "А-Я",
                        "Азбука",
                        "Необычные выражения",

                        "Записки мигранта",
                        "Русский язык",
                        "Жить счастливо",

                        "Готовим вкусно",
                        "Готовим с Галкиным",
                        "Рецепт отличного стейка",

                        "Платья",
                        "Как выбрать сумку",
                        "Что надеть?",
                    };

                    for (int i = 0; i < titles.Count; i++)
                    {
                        CategoryType category =
                            i < 4 ? CategoryType.PodcastsLanguage :
                                i < 7 ? CategoryType.PodcastsLearning :
                                    i < 10 ? CategoryType.PodcastsKitchen :
                                        CategoryType.PodcastsFashion;

                        result.Add(new SubCategoryItemViewModel(
                            category,
                            parentCategory,
                            titles.ElementAtOrDefault(i),
                            category.GetEnumDescription()));
                    }

                    break;
                case CategoryType.Recipes:
                    titles = new List<string>
                    {
                        "Вареники",
                        "Голубцы",
                        "Стейк",
                        "Шашлык",

                        "Бутерброд",
                        "Сэндвич",
                        "Салат",

                        "Гаспачо",
                        "Крем-суп",
                        "Куриный суп",
                    };

                    for (int i = 0; i < titles.Count; i++)
                    {
                        CategoryType category =
                            i < 4 ? CategoryType.RecipesHotDish :
                                i < 7 ? CategoryType.RecipesSnack :
                                    CategoryType.RecipesSoup;

                        result.Add(new SubCategoryItemViewModel(
                            category,
                            parentCategory,
                            titles.ElementAtOrDefault(i),
                            category.GetEnumDescription()));
                    }

                    break;
            }

            return result;
        }
    }
}
