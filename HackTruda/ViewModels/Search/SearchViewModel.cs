using System;
using HackTruda.Services.Interfaces;

namespace HackTruda.ViewModels.Search
{
    public class SearchViewModel : PageViewModel
    {
        public SearchViewModel(INavigationService navigationService, IDialogService dialogService, IDebuggerService debuggerService)
            : base(navigationService, dialogService, debuggerService)
        {
        }
    }
}
