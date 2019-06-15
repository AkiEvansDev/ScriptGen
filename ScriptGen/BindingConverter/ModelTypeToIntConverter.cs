using System;
using System.Globalization;
using System.Windows.Data;

using ScriptGen.Common;

namespace ScriptGen.BindingConverter
{
    public class ModelTypeToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => (int)(ModelType)value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => (ModelType)(int)value;
    }
}
