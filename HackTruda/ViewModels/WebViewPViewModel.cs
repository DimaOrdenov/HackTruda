using HackTruda.Services.Interfaces;
using Xamarin.Forms;

namespace HackTruda.ViewModels
{
    public class WebViewPViewModel : PageViewModel
    {
        public WebViewPViewModel(INavigationService navigationService, IDialogService dialogService, IDebuggerService debuggerService)
            : base(navigationService, dialogService, debuggerService)
        {
        }

        public WebViewSource Source { get; private set; }

        public override void Prepare(object parameter)
        {
            base.Prepare(parameter);

            if (parameter is WebViewSource webViewSource)
            {
                Source = webViewSource;
            }
        }
    }
}
