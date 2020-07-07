using HackTruda.Definitions.Enums;
using HackTruda.Services.Interfaces;

namespace HackTruda.ViewModels
{
    public class MainViewModel : PageViewModel
    {
        public MainViewModel(INavigationService navigationService, IDialogService dialogService, IDebuggerService debuggerService)
            : base(navigationService, dialogService, debuggerService)
        {
        }
    }
}
