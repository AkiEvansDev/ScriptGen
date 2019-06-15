using System;
using System.Globalization;
using System.Windows.Data;

using MaterialDesignThemes.Wpf;

namespace ScriptGen.BindingConverter
{
    public class FieldTypeToIconConverter : IValueConverter
    {
        private static readonly PackIconKind[] kinds =
        {
            PackIconKind.Key,
            PackIconKind.VectorLine,
            PackIconKind.TextShort,
            PackIconKind.Bracket
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => kinds[(int)value];

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => null;
    }
}
