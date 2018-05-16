using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Helper
{
    [ValueConversion(typeof(decimal), typeof(string))]
    public class DecimalZeroToEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal decVal = (decimal)value;
            return decVal != 0 ? decVal.ToString() : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal decVal = 0;
            decimal.TryParse((string)value, out decVal);
            return decVal;
        }
    }

    [ValueConversion(typeof(int), typeof(string))]
    public class IntZeroToEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intVal = (int)value;
            //return intVal != 0 ? intVal.ToString() : string.Empty;
            return intVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intVal = 0;
            int.TryParse((string)value, out intVal);
            return intVal;
        }
    }
}
