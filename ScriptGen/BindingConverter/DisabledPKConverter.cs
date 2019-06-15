using System;
using System.Globalization;
using System.Windows.Data;

using ScriptGenPlugin.Model;

namespace ScriptGen.BindingConverter
{
    public class DisabledPKConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => (FieldType)value == FieldType.PK ? false : true;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => null;
    }
}
