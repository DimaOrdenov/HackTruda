using System.ComponentModel;

namespace HackTruda.Definitions.Enums
{
    public enum CategoryType
    {
        // Main

        [Description("Все")]
        All,

        [Description("Люди")]
        People,

        [Description("Места")]
        Places,

        [Description("События")]
        Events,

        [Description("Объявления")]
        Ads,

        [Description("Игры")]
        Games,

        [Description("Подкасты")]
        Podcasts,

        [Description("Рецепты")]
        Recipes,

        // Subcategories

        [Description("Санкт-Петербург")]
        PeopleCity,

        [Description("Россия")]
        PeopleCountry,

        [Description("Развлечения")]
        PlacesFun,

        [Description("Где поесть")]
        PlacesEat,

        [Description("Продукты")]
        PlacesFood,

        [Description("Дни рождения")]
        EventsHappyBirthday,

        [Description("Культура")]
        EventsKulture,

        [Description("Праздники")]
        EventsCelebration,

        [Description("В аренду")]
        AdsRent,

        [Description("Работа")]
        AdsWork,

        [Description("Услуги")]
        AdsService,

        [Description("Горячие блюда")]
        RecipesHotDish,

        [Description("Закуски")]
        RecipesSnack,

        [Description("Супы")]
        RecipesSoup,

        [Description("Аркады")]
        GamesArcade,

        [Description("Викторины")]
        GamesQuiz,

        [Description("Головоломки")]
        GamesPuzzle,

        [Description("Язык")]
        PodcastsLanguage,

        [Description("Обучение")]
        PodcastsLearning,

        [Description("Кухня")]
        PodcastsKitchen,

        [Description("Мода")]
        PodcastsFashion,
    }
}
