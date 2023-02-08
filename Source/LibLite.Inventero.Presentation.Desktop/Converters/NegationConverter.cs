using System;
using System.Globalization;
using System.Windows.Data;

namespace LibLite.Inventero.Presentation.Desktop.Converters
{
    public class NegationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool boolean ? !boolean : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool boolean ? !boolean : value;
        }
    }
}
