using HackTruda.ViewModels;
using Xamarin.Forms;

namespace HackTruda.ViewControls
{
    public partial class AppActivityIndicator : Frame
    {
        public AppActivityIndicator()
        {
            InitializeComponent();

            SetBinding(IsVisibleProperty, new Binding(nameof(PageViewModel.IsLoading)));
        }

        public static readonly BindableProperty IndicatorColorProperty = BindableProperty.Create(
            nameof(IndicatorColor),
            typeof(Color),
            typeof(AppActivityIndicator),
            AppColors.Primary,
            propertyChanged: (sender, oldValue, newValue) =>
            {
                AppActivityIndicator view = (AppActivityIndicator)sender;

                view.indicator.Color = (Color)newValue;
            });

        public Color IndicatorColor
        {
            get => (Color)GetValue(IndicatorColorProperty);
            set => SetValue(IndicatorColorProperty, value);
        }
    }
}
