using System;
using Windows.UI;
using Windows.UI.Xaml.Data;

namespace Edi.UWP.Helpers.Converters
{
    /// <summary>
    /// Convert Color to RGB string
    /// </summary>
    public class ColorToRgbStringConverter : IValueConverter
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

                string sRgb = $"{color.R},{color.G},{color.B}";
                return sRgb;
            }

            var type = value.GetType();
            throw new InvalidOperationException($"Unsupported type [{type.Name}]");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var colorStr = ((string)value).ToLower();
            var arrRgb = colorStr.Split(',');
            if (arrRgb.Length == 3)
            {
                var r = (byte)System.Convert.ToUInt32(arrRgb[0]);
                var g = (byte)System.Convert.ToUInt32(arrRgb[1]);
                var b = (byte)System.Convert.ToUInt32(arrRgb[2]);
                return Color.FromArgb(255, r, g, b);
            }
            throw new FormatException("Invalid RGB Format");
        }
    }
}
