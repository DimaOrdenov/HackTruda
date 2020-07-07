using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HackTruda.Containers;
using HackTruda.Definitions.Enums;
using HackTruda.Definitions.PageStacks;
using HackTruda.Services.Interfaces;
using HackTruda.ViewModels;
using Xamarin.Forms;

namespace HackTruda.Services
{
    public class PageBuilder : IPageBuilder
    {
        private readonly Dictionary<PageType, PageStack> _pageStacks = new Dictionary<PageType, PageStack>();
        private readonly Dictionary<TabbedPageType, TabbedPageStack> _tabbedPageStacks = new Dictionary<TabbedPageType, TabbedPageStack>();

        public Dictionary<TabbedPageType, TabbedPageStack> GetTabbedPageStacks() => _tabbedPageStacks;

        public void Configure(PageStack pageStack)
        {
            if (pageStack == null)
            {
                throw new ArgumentNullException(nameof(PageStack), $"{nameof(PageStack)} cannot be null.");
            }

            if (_pageStacks.ContainsKey(pageStack.PageKey))
            {
                _pageStacks[pageStack.PageKey] = pageStack;
            }
            else
            {
                _pageStacks.Add(pageStack.PageKey, pageStack);
            }
        }

        public void ConfigureTabbed(TabbedPageStack tabbedPageStack)
        {
            if (tabbedPageStack == null)
            {
                throw new ArgumentNullException(nameof(TabbedPageStack), "TabbedPageStack cannot be null.");
            }

            if (_tabbedPageStacks.ContainsKey(tabbedPageStack.TabPageKey))
            {
                _tabbedPageStacks[tabbedPageStack.TabPageKey] = tabbedPageStack;
            }
            else
            {
                _tabbedPageStacks.Add(tabbedPageStack.TabPageKey, tabbedPageStack);
            }
        }

        public Page BuildPage(PageType pageKey, object parameter = null)
        {
            if (!_pageStacks.ContainsKey(pageKey))
            {
                throw new ArgumentException(
                    $"No such page: {pageKey}. Did you forget to call NavigationService.Configure?");
            }

            PageStack pageStack = _pageStacks[pageKey];

            ConstructorInfo pageConstructor = pageStack.PageClassType.GetTypeInfo()
                .DeclaredConstructors
                .FirstOrDefault(c => !c.GetParameters().Any());

            if (pageConstructor == null)
            {
                throw new InvalidOperationException(
                    $"No suitable constructor found for page {pageKey}");
            }

            var page = pageConstructor.Invoke(new object[] { }) as Page;

            var viewModel = IocInitializer.Container.Resolve(pageStack.ViewModelClassType) as PageViewModel;

            if (viewModel == null)
            {
                throw new ArgumentException(
                    $"No such view model type: {pageStack.ViewModelClassType}. Did you forget to register it?");
            }

            viewModel.PageTypeValue = pageKey;
            viewModel.Prepare(parameter);

            page.BindingContext = viewModel;

            return page;
        }

        public TabbedPage BuildTabbedPage(TabbedPageType pageKey, object tabbedPageParameter, IDictionary<PageType, object> childrenPageParameters, int currentPage)
        {
            if (!_tabbedPageStacks.ContainsKey(pageKey))
            {
                throw new ArgumentException(
                    $"No such tabbed page: {pageKey}. Did you forget to call NavigationService.ConfigureTabbed?");
            }

            TabbedPageStack tabbedPageStack = _tabbedPageStacks[pageKey];

            ConstructorInfo pageConstructor = tabbedPageStack.PageClassType.GetTypeInfo()
                .DeclaredConstructors
                .FirstOrDefault(c => !c.GetParameters().Any());

            if (pageConstructor == null)
            {
                throw new InvalidOperationException(
                    $"No suitable constructor found for tabbed page {pageKey}");
            }

            var tabbedPage = pageConstructor.Invoke(new object[] { }) as BaseTabbedPage;

            var viewModel = IocInitializer.Container.Resolve(tabbedPageStack.ViewModelClassType) as TabbedPageVM;

            if (viewModel == null)
            {
                throw new ArgumentException(
                    $"No such view model type: {tabbedPageStack.ViewModelClassType}. Did you forget to register it in IocInitializer.InitViewModels?");
            }

            viewModel.TabbedPageTypeValue = pageKey;
            viewModel.Prepare(tabbedPageParameter);
            viewModel.TabPagesCount = tabbedPageStack.ChildrenPageKeys.Count;

            tabbedPage.BindingContext = viewModel;

            foreach (PageType childrenKey in tabbedPageStack.ChildrenPageKeys)
            {
                ExtendedNavigationPage childPage = new ExtendedNavigationPage(BuildTabChildPage(childrenKey, childrenPageParameters?.FirstOrDefault(x => x.Key == childrenKey).Value));

                tabbedPageStack.ChildrenPageTitles.TryGetValue(childrenKey, out string childTitle);

                FileImageSource childIcon = null;
                tabbedPageStack.ChildrenPageIcons?.TryGetValue(childrenKey, out childIcon);

                if (!string.IsNullOrEmpty(childTitle))
                {
                    childPage.Title = childTitle;
                }

                if (childIcon != null)
                {
                    childPage.IconImageSource = childIcon;
                }

                tabbedPage.Children.Add(childPage);

                viewModel.TabPagesTypes.Add(childrenKey);
            }

            tabbedPage.CurrentPage = tabbedPage.Children[currentPage];

            return tabbedPage;
        }

        private Page BuildTabChildPage(PageType pageKey, object parameter = null)
        {
            if (!_pageStacks.ContainsKey(pageKey))
            {
                throw new ArgumentException(
                    $"No such page: {pageKey}. Did you forget to call NavigationService.Configure?");
            }

            PageStack pageStack = _pageStacks[pageKey];

            ConstructorInfo pageConstructor = pageStack.PageClassType.GetTypeInfo()
                .DeclaredConstructors
                .FirstOrDefault(c => !c.GetParameters().Any());

            if (pageConstructor == null)
            {
                throw new InvalidOperationException(
                    $"No suitable constructor found for page {pageKey}");
            }

            var page = pageConstructor?.Invoke(new object[] { }) as Page;

            var viewModel = IocInitializer.Container.Resolve(pageStack.ViewModelClassType) as PageVM;

            if (viewModel == null)
            {
                throw new ArgumentException(
                    $"No such view model type: {pageStack.ViewModelClassType}. Did you forget to register it?");
            }

            viewModel.PageTypeValue = pageKey;
            viewModel.Prepare(parameter);

            page.BindingContext = viewModel;

            return page;
        }
    }
}
