using Xamarin.Forms;

namespace HackTruda.ViewControls
{
    public partial class AppActivityIndicator : Frame
    {
        public AppActivityIndicator()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty IndicatorColorProperty = BindableProperty.Create(
            nameof(IndicatorColor),
            typeof(Color),
            typeof(AppActivityIndicator),
            ActivityIndicator.ColorProperty.DefaultValue,
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
