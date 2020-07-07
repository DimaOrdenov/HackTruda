using System;
using System.Linq;
using System.Threading.Tasks;
using HackTruda.Definitions.Enums;
using HackTruda.Services.Interfaces;
using HackTruda.ViewControls;
using HackTruda.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace HackTruda.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IPageBuilder _pageBuilder;

        public NavigationService(IPageBuilder pageBuilder)
        {
            _pageBuilder = pageBuilder;
        }

        /// <inheritdoc />
        public async Task NavigateBackAsync(bool animated = true)
        {
            if (PopupNavigation.Instance.PopupStack?.Count > 0)
            {
                await PopupNavigation.Instance.PopAsync(animated);

                return;
            }

            INavigation navigationStack = GetCurrentPage().Navigation;

            if (navigationStack.ModalStack.Count > 0)
            {
                await navigationStack.PopModalAsync(animated);

                return;
            }

            if (navigationStack.NavigationStack.Count > 1)
            {
                await navigationStack.PopAsync(animated);
            }
        }

        /// <inheritdoc />
        public async Task NavigateBackToRoot(bool animated = true)
        {
            var navigationStack = GetCurrentPage().Navigation;

            for (int i = 0; i < navigationStack.ModalStack?.Count; i++)
            {
                await GetCurrentPage().Navigation.PopModalAsync(animated);
            }

            await GetCurrentPage().Navigation.PopToRootAsync(animated);
        }

        /// <inheritdoc />
        public async Task NavigateModalAsync(PageType pageKey, bool animated = true)
        {
            await NavigateModalAsync(pageKey, null, animated);
        }

        /// <inheritdoc />
        public async Task NavigateModalAsync(PageType pageKey, object parameter, bool animated = true)
        {
            var page = _pageBuilder.BuildPage(pageKey, parameter);

            NavigationPage.SetHasNavigationBar(page, false);

            if (page?.BindingContext is PageViewModel viewModel)
            {
                //viewModel.HasNavigationBarBackButton = false;
            }

            await GetCurrentPage().Navigation.PushModalAsync(page, animated);
        }

        /// <inheritdoc />
        public async Task NavigateModalAsync(Page page, bool animated = true)
        {
            await NavigateModalAsync(page, null, animated);
        }

        /// <inheritdoc />
        public async Task NavigateModalAsync(Page page, object parameter, bool animated = true)
        {
            NavigationPage.SetHasNavigationBar(page, false);

            if (page?.BindingContext is PageViewModel viewModel)
            {
                //viewModel.HasNavigationBarBackButton = false;
            }

            await GetCurrentPage().Navigation.PushModalAsync(page, animated);
        }

        /// <inheritdoc />
        public async Task NavigateAsync(PageType pageKey, bool aboveTabs = true, bool animated = true)
        {
            await NavigateAsync(pageKey, null, aboveTabs, animated);
        }

        /// <inheritdoc />
        public async Task NavigateAsync(PageType pageKey, object parameter, bool aboveTabs = true, bool animated = true)
        {
            Page pageToNavigate = await Task.Run(() => _pageBuilder.BuildPage(pageKey, parameter));

            if (pageToNavigate?.BindingContext is PageViewModel viewModel)
            {
                //viewModel.HasNavigationBarBackButton = true;
            }

            await (aboveTabs ?
                GetCurrentNavigationPage(Application.Current.MainPage) :
                GetCurrentPage()).Navigation.PushAsync(pageToNavigate, animated);
        }

        /// <inheritdoc />
        public Task NavigatePopupAsync(PageType pageKey, bool animated = true) =>
            NavigatePopupAsync(pageKey, null, animated);

        /// <inheritdoc />
        public async Task NavigatePopupAsync(PageType pageKey, object parameter, bool animated = true)
        {
            PopupPage page = _pageBuilder.BuildPage(pageKey, parameter) as PopupPage;

            await PopupNavigation.Instance.PushAsync(page, animated);
        }

        /// <inheritdoc />
        public void SetRootPage(PageType pageKey)
        {
            var mainPage = new ExtendedNavigationPage(_pageBuilder.BuildPage(pageKey));

            SetMainPage(mainPage);
        }

        /// <inheritdoc />
        public void SetRootPage(TabbedPageType tabPageKey)
        {
            var mainPage = new ExtendedNavigationPage(_pageBuilder.BuildTabbedPage(tabPageKey, null, null, 0));

            SetMainPage(mainPage);
        }

        /// <inheritdoc />
        public Page GetCurrentPage()
        {
            Page currentNavigationPage = Application.Current.MainPage;

            while (currentNavigationPage is NavigationPage ||
                   currentNavigationPage is BaseTabbedPage)
            {
                currentNavigationPage = GetCurrentNavigationPage(currentNavigationPage);
                currentNavigationPage = GetCurrentTabChildPage(currentNavigationPage);
            }

            return currentNavigationPage ?? Application.Current.MainPage;
        }

        /// <inheritdoc />
        public Page GetCurrentNavigationPage(Page page)
        {
            Page currentNavigationPage = page;

            if (currentNavigationPage is NavigationPage navigationPage)
            {
                if (navigationPage.Navigation?.ModalStack?.Count > 0)
                {
                    currentNavigationPage = navigationPage.Navigation.ModalStack.LastOrDefault();
                }
                else
                {
                    currentNavigationPage = navigationPage.CurrentPage;
                }

                return GetCurrentNavigationPage(currentNavigationPage);
            }

            return currentNavigationPage;
        }

        /// <inheritdoc />
        public Page GetCurrentTabbedPage()
        {
            Page currentTabbedPage = Application.Current.MainPage;

            while (!(currentTabbedPage is BaseTabbedPage))
            {
                currentTabbedPage = GetCurrentNavigationPage(currentTabbedPage);
            }

            return currentTabbedPage;
        }

        private Page GetCurrentTabChildPage(Page page)
        {
            Page currentNavigationPage = page;

            if (currentNavigationPage is BaseTabbedPage tabbedPage)
            {
                currentNavigationPage = tabbedPage.CurrentPage;
            }

            return currentNavigationPage;
        }

        private void SetMainPage(Page rootPage)
        {
            Device.BeginInvokeOnMainThread(() =>
                Application.Current.MainPage = rootPage);
        }
    }
}
