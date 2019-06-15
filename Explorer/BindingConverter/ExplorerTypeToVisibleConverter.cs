using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Explorer.BindingConverter
{
    internal class ExplorerTypeToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => (ExplorerType)value == ExplorerType.Save ? Visibility.Visible : Visibility.Collapsed;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => null;
    }
}
