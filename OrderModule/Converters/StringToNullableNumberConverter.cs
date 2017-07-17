using System;
using System.Globalization;
using System.Windows.Data;

namespace OrderModule.Converters
{
    public class StringToNullableNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string strValue)
            {
                if (targetType == typeof(int?))
                {
                    if (int.TryParse(strValue, out int result))
                    {
                        return result;
                    }
                    return null;
                }

                if (targetType == typeof(decimal?))
                {
                    if (decimal.TryParse(strValue, out decimal result))
                    {
                        return result;
                    }
                    return null;
                }
            }

            return value;
        }
    }
}
