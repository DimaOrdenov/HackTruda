using System.Windows.Input;
using Xamarin.Forms;

namespace HackTruda.ViewControls
{
    public class BaseDatePicker : DatePicker
    {
        public BaseDatePicker()
        {
            DateSelected += (sender, e) => DateSelectedCommand?.Execute(e.NewDate);
        }

        public static readonly BindableProperty DateSelectedCommandProperty = BindableProperty.Create(
            nameof(DateSelectedCommand),
            typeof(ICommand),
            typeof(BaseDatePicker));

        public ICommand DateSelectedCommand
        {
            get => (ICommand)GetValue(DateSelectedCommandProperty);
            set => SetValue(DateSelectedCommandProperty, value);
        }
    }
}
