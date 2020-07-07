using System;
using HackTruda.ViewModels;
using Xamarin.Forms;

namespace HackTruda.ViewControls
{
    public abstract class BaseTabbedPage : TabbedPage
    {
        public BaseTabbedPage()
        {
            SetBinding(IsBusyProperty, new Binding(nameof(TabbedPageViewModel.IsBusy)));

            NavigationPage.SetBackButtonTitle(this, string.Empty);
        }

        protected override bool OnBackButtonPressed()
        {
            var bindingContext = BindingContext as TabbedPageViewModel;

            if (bindingContext?.OnBackButtonPressed() == true)
            {
                return true;
            }

            return base.OnBackButtonPressed();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var bindingContext = BindingContext as TabbedPageViewModel;

            bindingContext?.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            var bindingContext = BindingContext as TabbedPageViewModel;

            bindingContext?.OnDisappearing();
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            var bindingContext = BindingContext as TabbedPageViewModel;

            bindingContext?.OnCurrentPageChanged(Children.IndexOf(CurrentPage));
        }

    }
}
