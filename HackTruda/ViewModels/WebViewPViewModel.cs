using HackTruda.Services.Interfaces;
using Xamarin.Forms;

namespace HackTruda.ViewModels
{
    /// <summary>
    /// VM для страницы с <see cref="WebView"/>.
    /// </summary>
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
