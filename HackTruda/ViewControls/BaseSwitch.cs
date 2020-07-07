using System.Windows.Input;
using Xamarin.Forms;

namespace HackTruda.ViewControls
{
    public class BaseSwitch : Switch
    {
        public BaseSwitch()
        {
            Toggled += (sender, e) => Command?.Execute(e.Value);
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(BaseSwitch));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
    }
}
