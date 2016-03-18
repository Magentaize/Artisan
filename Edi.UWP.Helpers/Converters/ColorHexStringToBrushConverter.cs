using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Edi.UWP.Helpers.Converters
{
    /// <summary>
    /// Convert HEX color string to SolidColorBrush
    /// </summary>
    public class ColorHexStringToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var colorStr = ((string)value).ToLower();

            colorStr = colorStr.Replace("#", string.Empty);

            var r = (byte)System.Convert.ToUInt32(colorStr.Substring(0, 2), 16);
            var g = (byte)System.Convert.ToUInt32(colorStr.Substring(2, 2), 16);
            var b = (byte)System.Convert.ToUInt32(colorStr.Substring(4, 2), 16);

            var myBrush = new SolidColorBrush(Color.FromArgb(255, r, g, b));
            return myBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
