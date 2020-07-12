using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HackTruda.Definitions.Converters
{
    /// <summary>
    /// Конвертер bool значения в противоположное.
    /// </summary>
    public class BoolToNegativeConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            !(bool)value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            !(bool)value;

        public object ProvideValue(IServiceProvider serviceProvider) =>
            this;
    }
}
