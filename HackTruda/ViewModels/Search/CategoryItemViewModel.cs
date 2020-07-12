using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HackTruda.Definitions.Enums;
using HackTruda.Extensions;
using Xamarin.Forms;

namespace HackTruda.ViewModels.Search
{
    public class CategoryItemViewModel : BaseViewModel
    {
        public ICommand TapCommand { get; set; }

        public CategoryItemViewModel(CategoryType type, IEnumerable<SubCategoryItemViewModel> subCategoryItems = null)
        {
            Type = type;
            SubCategoryItems = subCategoryItems;
            Title = Type.GetEnumDescription();
            Description = GetDescription();
            Image = GetImage();
        }

        public CategoryType Type { get; }

        public IEnumerable<SubCategoryItemViewModel> SubCategoryItems { get; }

        public string Title { get; set; }

        public string Description { get; protected set; }

        public ImageSource Image { get; protected set; }

        private string GetDescription()
        {
            switch (Type)
            {
                case CategoryType.People:
                    return "50 232 человека";
                case CategoryType.Places:
                    return "620 мест";
                case CategoryType.Events:
                    return "40 событий";
                case CategoryType.Ads:
                    return "3 000 объявлений";
                case CategoryType.Games:
                    return "43 игры";
                case CategoryType.Podcasts:
                    return "540 подкастов";
                case CategoryType.Recipes:
                    return "3 004 рецепта";
            }

            return null;
        }

        private ImageSource GetImage()
        {
            switch (Type)
            {
                case CategoryType.People:
                    return AppImages.ImageSearchPeople;
                case CategoryType.Places:
                    return AppImages.ImageSearchPlaces;
                case CategoryType.Events:
                    return AppImages.ImageSearchEvents;
                case CategoryType.Ads:
                    return AppImages.ImageSearchAds;
                case CategoryType.Games:
                    return AppImages.ImageSearchGames;
                case CategoryType.Podcasts:
                    return AppImages.ImageSearchPodcasts;
                case CategoryType.Recipes:
                    return AppImages.ImageSearchRecipes;
                default:
                    IEnumerable<ImageSource> restImages = new List<ImageSource>
                    {
                        AppImages.Placeholder1,
                        AppImages.Placeholder2,
                        AppImages.Placeholder3,
                        AppImages.Placeholder4,
                        AppImages.Placeholder5,
                        AppImages.Placeholder6,
                    };

                    int random = new Random().Next(0, restImages.Count());

                    return restImages.ElementAtOrDefault(random);
            }
        }
    }
}
