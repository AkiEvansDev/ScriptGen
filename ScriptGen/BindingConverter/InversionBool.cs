using System;
using System.Globalization;
using System.Windows.Data;

namespace ScriptGen.BindingConverter
{
    public class InversionBool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => !(bool)value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => null;
    }
}
