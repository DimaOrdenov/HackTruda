using Humanizer;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HackTruda.Definitions.Converters
{
    public class DateTimeToStringHumanizeConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string humanizedString = null;

            if (value is DateTime dateTime)
            {
                humanizedString = dateTime.Humanize();
                //humanizedString = ((DateTime)value).Humanize(culture: new CultureInfo("ru-RU"));
            }

            return humanizedString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) =>
            value;

        public object ProvideValue(IServiceProvider serviceProvider) =>
            this;
    }
}
