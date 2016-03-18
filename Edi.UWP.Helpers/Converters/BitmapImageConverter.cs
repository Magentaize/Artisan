using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace Edi.UWP.Helpers.Converters
{
    /// <summary>
    /// Convert URL of an image file to BitmapImage
    /// </summary>
    public class BitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var s = value as string;
            if (s != null)
            {
                return new BitmapImage(new Uri(s, UriKind.RelativeOrAbsolute));
            }

            var uri = value as Uri;
            if (uri != null)
            {
                return new BitmapImage(uri);
            }

            throw new NotSupportedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
