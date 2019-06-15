using System;
using System.Globalization;
using System.Windows.Data;

namespace ScriptGen.BindingConverter
{
    public class MessageOpenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => string.IsNullOrWhiteSpace((string)value) ? false : true;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => null;
    }
}
