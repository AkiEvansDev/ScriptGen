using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Explorer
{
    public partial class EditableTextBlock : UserControl
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string),
            typeof(EditableTextBlock), new PropertyMetadata(""));

        public static readonly DependencyProperty IsEditableProperty =
            DependencyProperty.Register("IsEditable", typeof(bool),
            typeof(EditableTextBlock), new PropertyMetadata(true));

        public static readonly DependencyProperty IsInEditModeProperty =
            DependencyProperty.Register("IsInEditMode", typeof(bool),
            typeof(EditableTextBlock), new PropertyMetadata(false));

        private string oldText;

        public EditableTextBlock()
        {
            InitializeComponent();

            Focusable = true;
            FocusVisualStyle = null;
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public bool IsEditable
        {
            get => (bool)GetValue(IsEditableProperty);
            set => SetValue(IsEditableProperty, value);
        }

        public bool IsInEditMode
        {
            get => IsEditable ? (bool)GetValue(IsInEditModeProperty) : false;
            set
            {
                if (IsEditable)
                {
                    if (value)
                        oldText = Text;

                    SetValue(IsInEditModeProperty, value);
                }
            }
        }

        private void TextBoxLoaded(object sender, RoutedEventArgs e)
        {
            TextBox txt = sender as TextBox;

            txt.Focus();
            txt.SelectAll();
        }

        private void TextBoxLostFocus(object sender, RoutedEventArgs e)
            => IsInEditMode = false;

        private void TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MainControl.Focus();

                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                IsInEditMode = false;
                Text = oldText;

                e.Handled = true;
            }
        }

        private void TextBlockMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2 && IsEditable)
            {
                IsInEditMode = true;

                e.Handled = true;
            }
        }
    }
}
