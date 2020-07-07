using System.Threading.Tasks;
using HackTruda.Definitions.Enums;
using Xamarin.Forms;

namespace HackTruda.Services.Interfaces
{
    public interface INavigationService
    {
        Task NavigateBackAsync(bool animated = true);

        Task NavigateBackToRoot(bool animated = true);

        Task NavigateModalAsync(PageType pageKey, bool animated = true);

        Task NavigateModalAsync(PageType pageKey, object parameter, bool animated = true);

        Task NavigateModalAsync(Page page, bool animated = true);

        Task NavigateModalAsync(Page page, object parameter, bool animated = true);

        Task NavigateAsync(PageType pageKey, bool aboveTabs = true, bool animated = true);

        Task NavigateAsync(PageType pageKey, object parameter, bool aboveTabs = true, bool animated = true);

        Task NavigatePopupAsync(PageType pageKey, bool animated = true);

        Task NavigatePopupAsync(PageType pageKey, object parameter, bool animated = true);

        void SetRootPage(PageType pageKey);

        void SetRootPage(TabbedPageType tabPageKey);

        Page GetCurrentPage();

        Page GetCurrentNavigationPage(Page page);

        Page GetCurrentTabbedPage();
    }
}
