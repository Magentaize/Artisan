﻿using System;
using Windows.UI.Xaml.Data;

namespace Edi.UWP.Helpers.Converters
{
    /// <summary>
    /// Convert DateTime to DateTimeOffset
    /// </summary>
    public class DateTimeToOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                var date = (DateTime)value;
                return new DateTimeOffset(date);
            }
            catch (Exception ex)
            {
                return DateTimeOffset.MinValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            try
            {
                var dto = (DateTimeOffset)value;
                return dto.DateTime;
            }
            catch (Exception ex)
            {
                return DateTime.MinValue;
            }
        }
    }
}
