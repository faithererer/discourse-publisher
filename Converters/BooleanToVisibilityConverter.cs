using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DiscoursePublisher.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = value is bool b && b;

            if (parameter is string str && str.Equals("Invert", StringComparison.OrdinalIgnoreCase))
            {
                boolValue = !boolValue;
            }

            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isVisible = value is Visibility visibility && visibility == Visibility.Visible;

            if (parameter is string str && str.Equals("Invert", StringComparison.OrdinalIgnoreCase))
            {
                return !isVisible;
            }

            return isVisible;
        }
    }
}