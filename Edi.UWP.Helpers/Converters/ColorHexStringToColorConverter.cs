using System;
using Windows.UI;
using Windows.UI.Xaml.Data;

namespace Edi.UWP.Helpers.Converters
{
    /// <summary>
    /// Covert HEX color string to Color
    /// </summary>
    public class ColorHexStringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var colorStr = ((string)value).ToLower();

            colorStr = colorStr.Replace("#", string.Empty);

            var r = (byte)System.Convert.ToUInt32(colorStr.Substring(0, 2), 16);
            var g = (byte)System.Convert.ToUInt32(colorStr.Substring(2, 2), 16);
            var b = (byte)System.Convert.ToUInt32(colorStr.Substring(4, 2), 16);

            return Color.FromArgb(255, r, g, b);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
