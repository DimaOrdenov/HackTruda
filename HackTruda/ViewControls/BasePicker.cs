using System.Windows.Input;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms;

namespace HackTruda.ViewControls
{
    public class BasePicker : Xamarin.Forms.Picker
    {
        public BasePicker()
        {
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUpdateMode(UpdateMode.WhenFinished);

            SelectedIndexChanged += (sender, e) => SelectedIndexChangedCommand?.Execute((sender as Xamarin.Forms.Picker)?.SelectedIndex);
        }

        /// <summary>
        /// Bindable property for <see cref="SelectedIndexChangedCommand"/>.
        /// </summary>
        public static readonly BindableProperty SelectedIndexChangedCommandProperty = BindableProperty.Create(
            nameof(SelectedIndexChangedCommand),
            typeof(ICommand),
            typeof(BasePicker));

        /// <summary>
        /// Executes when <see cref="Xamarin.Forms.Picker.SelectedIndexChanged"/> event triggers.
        /// </summary>
        public ICommand SelectedIndexChangedCommand
        {
            get => (ICommand)GetValue(SelectedIndexChangedCommandProperty);
            set => SetValue(SelectedIndexChangedCommandProperty, value);
        }
    }
}
