using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Edi.UWP.Helpers.Converters
{
    /// <summary>
    /// Convert Color to SolidColorBrush
    /// </summary>
    public class ColorToSolidColorBrushValueConverter : IValueConverter
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
                return new SolidColorBrush(color);
            }

            var type = value.GetType();
            throw new InvalidOperationException($"Unsupported type [{type.Name}]");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
