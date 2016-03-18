using System;
using Windows.UI;
using Windows.UI.Xaml.Data;

namespace Edi.UWP.Helpers.Converters
{
    /// <summary>
    /// Convert Color to HEX string
    /// </summary>
    public class ColorToHexStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (null == value)
            {
                return null;
            }

            if (value is Color)
            {
                var color = (Color)value;
                return color.ToString().Replace("#FF", "#");
            }

            var type = value.GetType();
            throw new InvalidOperationException($"Unsupported type [{type.Name}]");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var colorStr = ((string)value).ToLower();

            colorStr = colorStr.Replace("#", string.Empty);

            try
            {
                var r = (byte)System.Convert.ToUInt32(colorStr.Substring(0, 2), 16);
                var g = (byte)System.Convert.ToUInt32(colorStr.Substring(2, 2), 16);
                var b = (byte)System.Convert.ToUInt32(colorStr.Substring(4, 2), 16);

                return Color.FromArgb(255, r, g, b);
            }
            catch (Exception)
            {
                return Color.FromArgb(255, 0, 0, 0);
            }
        }
    }
}
