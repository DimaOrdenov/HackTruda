using System.Runtime.CompilerServices;
using HackTruda.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace HackTruda.ViewControls
{
    public abstract class BasePage : ContentPage
    {
        public BasePage()
        {
            SetBinding(IsBusyProperty, new Binding(nameof(PageViewModel.IsBusy)));

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetLargeTitleDisplay(LargeTitleDisplayMode.Never);

            SetPaddingForIOS();

            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, string.Empty);
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == Xamarin.Forms.NavigationPage.HasNavigationBarProperty.PropertyName)
            {
                SetPaddingForIOS();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            var bindingContext = BindingContext as PageViewModel;

            if (bindingContext?.OnBackButtonPressed() == true)
            {
                return true;
            }

            return base.OnBackButtonPressed();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var bindingContext = BindingContext as PageViewModel;

            if (bindingContext != null)
            {
                await bindingContext.OnAppearing();
            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            var bindingContext = BindingContext as PageViewModel;

            if (bindingContext != null)
            {
                await bindingContext.OnDisappearing();
            }
        }

        private void SetPaddingForIOS()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                if (DeviceInfo.Version.Major <= 10 &&
                    !Xamarin.Forms.NavigationPage.GetHasNavigationBar(this))
                {
                    Padding = new Thickness(Padding.Left, Padding.Top + 20, Padding.Right, Padding.Bottom);
                }
            }
        }
    }
}
