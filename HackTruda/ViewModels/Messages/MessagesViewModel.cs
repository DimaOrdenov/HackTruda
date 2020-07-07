using System;
using HackTruda.Services.Interfaces;

namespace HackTruda.ViewModels.Messages
{
    public class MessagesViewModel : PageViewModel
    {
        public MessagesViewModel(INavigationService navigationService, IDialogService dialogService, IDebuggerService debuggerService)
            : base(navigationService, dialogService, debuggerService)
        {
        }
    }
}
