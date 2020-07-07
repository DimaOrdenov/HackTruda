using System;
using HackTruda.Services.Interfaces;

namespace HackTruda.ViewModels.Profile
{
    public class ProfileViewModel : PageViewModel
    {
        public ProfileViewModel(INavigationService navigationService, IDialogService dialogService, IDebuggerService debuggerService)
            : base(navigationService, dialogService, debuggerService)
        {
        }
    }
}
