using HackTruda.Services.Interfaces;

namespace HackTruda.ViewModels.Notifications
{
    public class NotificationsViewModel : PageViewModel
    {
        public NotificationsViewModel(INavigationService navigationService, IDialogService dialogService, IDebuggerService debuggerService)
            : base(navigationService, dialogService, debuggerService)
        {
        }
    }
}
