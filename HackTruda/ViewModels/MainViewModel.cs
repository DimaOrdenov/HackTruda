using HackTruda.Services.Interfaces;

namespace HackTruda.ViewModels
{
    /// <summary>
    /// VM для главной таб-страницы.
    /// </summary>
    public class MainViewModel : TabbedPageViewModel
    {
        public MainViewModel(INavigationService navigationService, IDialogService dialogService, IDebuggerService debuggerService)
            : base(navigationService, dialogService, debuggerService)
        {
        }
    }
}
