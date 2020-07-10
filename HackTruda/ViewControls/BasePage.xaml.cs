using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using HackTruda.Extensions;
using HackTruda.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace HackTruda.ViewControls
{
    public partial class BasePage : ContentPage
    {
        public BasePage()
        {
            InitializeComponent();

            icArrowBack.SetStrokeTintColor(Color.Black);

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetLargeTitleDisplay(LargeTitleDisplayMode.Never);

            SetPaddingForIOS();

            //SetBinding(BindableToolbarItemsProperty, new Binding(nameof(PageViewModel.ToolbarItems)));
        }

        //public static readonly BindableProperty BindableToolbarItemsProperty = BindableProperty.Create(
        //    nameof(BindableToolbarItems),
        //    typeof(ObservableCollection<ToolbarItem>),
        //    typeof(BasePage),
        //    propertyChanged: (sender, oldValue, newValue) =>
        //    {
        //        BasePage view = (BasePage)sender;

        //        view.ToolbarItems.Clear();

        //        if (!(newValue is IList<ToolbarItem> newToolbarItems))
        //        {
        //            return;
        //        }

        //        foreach (ToolbarItem toolbarItem in newToolbarItems)
        //        {
        //            view.ToolbarItems.Add(toolbarItem);
        //        }
        //    });

        //public ObservableCollection<ToolbarItem> BindableToolbarItems
        //{
        //    get => (ObservableCollection<ToolbarItem>)GetValue(BindableToolbarItemsProperty);
        //    set => SetValue(BindableToolbarItemsProperty, value);
        //}

        public View TitleViewContent
        {
            get => titleViewContent.Content;
            set => titleViewContent.Content = value;
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
