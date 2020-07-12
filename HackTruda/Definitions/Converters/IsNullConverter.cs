using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HackTruda.Definitions.Converters
{
    /// <summary>
    /// Конвертер object в bool. True, если object == null.
    /// </summary>
    public class IsNullConverter : IValueConverter, IMarkupExtension
    {
        // TODO Добавить поддержку других типов
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                return string.IsNullOrEmpty(str);
            }

            return value == null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                return !string.IsNullOrEmpty(str);
            }

            return value != null;
        }

        public object ProvideValue(IServiceProvider serviceProvider) =>
            this;
    }
}
