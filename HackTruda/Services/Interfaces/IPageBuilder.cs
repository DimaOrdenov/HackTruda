using System.Collections.Generic;
using HackTruda.Definitions.Enums;
using HackTruda.Definitions.PageStacks;
using Xamarin.Forms;

namespace HackTruda.Services.Interfaces
{
    public interface IPageBuilder
    {
        Dictionary<TabbedPageType, TabbedPageStack> GetTabbedPageStacks();

        void Configure(PageStack pageStack);

        void ConfigureTabbed(TabbedPageStack tabbedPageStack);

        Page BuildPage(PageType pageKey, object parameter = null);

        TabbedPage BuildTabbedPage(TabbedPageType pageKey, object tabbedPageParameter, IDictionary<PageType, object> childrenPageParameters, int currentPage);
    }
}
