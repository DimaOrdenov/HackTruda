using HackTruda.Services.Interfaces;

namespace HackTruda.ViewModels.Feed
{
    public class FeedViewModel : PageViewModel
    {
        public FeedViewModel(INavigationService navigationService, IDialogService dialogService, IDebuggerService debuggerService)
            : base(navigationService, dialogService, debuggerService)
        {
        }
    }
}
