using System;
using System.Collections.Generic;
using System.Windows.Input;
using FFImageLoading.Svg.Forms;
using Xamarin.Forms;

namespace HackTruda.Views.Common
{
    public partial class FabView : ContentView
    {
        public FabView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(
            nameof(TapCommand),
            typeof(ICommand),
            typeof(FabView));

        public static readonly BindableProperty ImageProperty = BindableProperty.Create(
            nameof(Image),
            typeof(SvgImageSource),
            typeof(FabView));

        public ICommand TapCommand
        {
            get => (ICommand)GetValue(TapCommandProperty);
            set => SetValue(TapCommandProperty, value);
        }

        public SvgImageSource Image
        {
            get => (SvgImageSource)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }
    }
}
