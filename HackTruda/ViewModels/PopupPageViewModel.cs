using System.Threading.Tasks;
using HackTruda.Services.Interfaces;

namespace HackTruda.ViewModels
{
    public abstract class PopupPageViewModel : PageViewModel
    {
        protected PopupPageViewModel(
            INavigationService navigationService,
            IDialogService dialogService,
            IDebuggerService debuggerService)
            : base(navigationService, dialogService, debuggerService)
        {
        }

        public virtual Task OnAppearingAnimationBeginAsync()
        {
            return Task.Run(() => { });
        }

        public override bool OnBackButtonPressed()
        {
            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();

            return false;
        }

        public virtual Task OnAppearingAnimationEndAsync()
        {
            return Task.Run(() => { });
        }

        public virtual Task OnDisappearingAnimationEndAsync()
        {
            return Task.Run(() => { });
        }

        public virtual Task OnDisappearingAnimationBeginAsync()
        {
            return Task.Run(() => { });
        }

        public virtual bool OnBackgroundClicked()
        {
            return true;
        }
    }
}
