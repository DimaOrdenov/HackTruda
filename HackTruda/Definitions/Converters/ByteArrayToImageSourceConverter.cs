using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HackTruda.Definitions.Converters
{
    /// <summary>
    /// Конвертер byte[] в <see cref="ImageSource"/>.
    /// </summary>
    public class ByteArrayToImageSourceConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ImageSource imgSource = null;
            if (value != null)
            {
                byte[] imageAsBytes = (byte[])value;
                imgSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
            }
            return imgSource;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}
