using System;
using System.Threading.Tasks;
using HackTruda.ViewModels;
using Rg.Plugins.Popup.Pages;

namespace HackTruda.ViewControls
{
    public abstract class BasePopupPage : PopupPage
    {
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var bindingContext = BindingContext as PopupPageViewModel;

            if (bindingContext != null)
            {
                await bindingContext.OnAppearing();
            }
        }

        protected override async Task OnAppearingAnimationBeginAsync()
        {
            await base.OnAppearingAnimationBeginAsync();

            var bindingContext = BindingContext as PopupPageViewModel;

            if (bindingContext != null)
            {
                await bindingContext.OnAppearingAnimationBeginAsync();
            }
        }

        protected override async Task OnAppearingAnimationEndAsync()
        {
            await base.OnAppearingAnimationEndAsync();

            var bindingContext = BindingContext as PopupPageViewModel;

            if (bindingContext != null)
            {
                await bindingContext.OnAppearingAnimationEndAsync();
            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            var bindingContext = BindingContext as PopupPageViewModel;

            if (bindingContext != null)
            {
                await bindingContext.OnDisappearing();
            }
        }

        protected override async Task OnDisappearingAnimationBeginAsync()
        {
            await base.OnDisappearingAnimationBeginAsync();

            var bindingContext = BindingContext as PopupPageViewModel;

            if (bindingContext != null)
            {
                await bindingContext.OnDisappearingAnimationBeginAsync();
            }
        }

        protected override async Task OnDisappearingAnimationEndAsync()
        {
            await base.OnDisappearingAnimationEndAsync();

            var bindingContext = BindingContext as PopupPageViewModel;

            if (bindingContext != null)
            {
                await bindingContext.OnDisappearingAnimationEndAsync();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            var bindingContext = BindingContext as PopupPageViewModel;

            if (bindingContext?.OnBackButtonPressed() == true)
            {
                return true;
            }

            return base.OnBackButtonPressed();
        }

        protected override bool OnBackgroundClicked()
        {
            var bindingContext = BindingContext as PopupPageViewModel;

            if (bindingContext?.OnBackgroundClicked() == true)
            {
                return true;
            }

            return base.OnBackgroundClicked();
        }
    }
}
