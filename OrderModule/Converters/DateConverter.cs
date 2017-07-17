using System;
using System.Globalization;
using System.Windows.Data;

namespace OrderModule.Converters
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;
            return date.ToString("d", culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value.ToString();
            if (DateTime.TryParse(strValue, culture, DateTimeStyles.None, out DateTime resultDateTime))
            {
                return resultDateTime;
            }
            return value;
        }
    }
}
