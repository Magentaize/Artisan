using System;
using Windows.UI.Xaml.Data;

namespace Edi.UWP.Helpers.Converters
{
    /// <summary>
    /// Convert string to formattable string 
    /// </summary>
    public class StringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // No format provided.
            if (parameter == null)
            {
                return value;
            }

            return string.Format((string)parameter, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
