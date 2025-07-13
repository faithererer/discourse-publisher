using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace DiscoursePublisher.Converters
{
    public class StringListToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<string> stringList)
            {
                string separator = ", ";
                if (parameter is string str && str.Equals("NewLine", StringComparison.OrdinalIgnoreCase))
                {
                    separator = Environment.NewLine;
                }
                return string.Join(separator, stringList);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}