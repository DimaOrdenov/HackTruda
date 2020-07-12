using System;
using System.Collections.Generic;
using System.Linq;
using HackTruda.Definitions.Enums;
using HackTruda.Services.Interfaces;
using HackTruda.ViewModels;
using Xamarin.Forms;

namespace HackTruda.Services
{
    /// <summary>
    /// Хэлпер для навигации.
    /// </summary>
    public class NavigationExtensions
    {
        private readonly INavigationService _navigationService;

        public NavigationExtensions(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public bool GetIsCurrentPageNavigatedOverTabs()
        {
            foreach (Page page in _navigationService.GetCurrentPage()?.Navigation?.NavigationStack?.Reverse() ?? new List<Page>())
            {
                if (page is TabbedPage)
                {
                    return true;
                }
            }

            return false;
        }

        public bool RemovePreviousPagesTillSpecificPage(PageType pageType)
        {
            INavigation navigation = _navigationService.GetCurrentPage()?.Navigation;

            // We cannot obviously remove first page in stack and/or current
            if (navigation?.NavigationStack.Count <= 2)
            {
                return false;
            }

            IEnumerable<Page> pagesToRemove = navigation?.NavigationStack?.Skip(1)?.ToList();
            pagesToRemove = pagesToRemove?.Reverse()?.Skip(1);

            if (!(pagesToRemove?.FirstOrDefault(x => (x.BindingContext as PageViewModel)?.PageTypeValue == pageType) is Page specificPage))
            {
                return false;
            }

            foreach (Page page in pagesToRemove?.TakeWhile(x =>
                (x.BindingContext as PageViewModel)?.PageTypeValue != (specificPage.BindingContext as PageViewModel)?.PageTypeValue)
                ?? new List<Page>())
            {
                navigation.RemovePage(page);
            }

            return true;
        }
    }
}
