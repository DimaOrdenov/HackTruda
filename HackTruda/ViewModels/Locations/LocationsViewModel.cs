using System;
using HackTruda.Services.Interfaces;

namespace HackTruda.ViewModels.Locations
{
    public class LocationsViewModel : PageViewModel
    {
        public LocationsViewModel(INavigationService navigationService, IDialogService dialogService, IDebuggerService debuggerService)
            : base(navigationService, dialogService, debuggerService)
        {
        }
    }
}
