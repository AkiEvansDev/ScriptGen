using System.Windows;
using System.Windows.Input;

namespace ScriptGen.View
{
    public partial class ScriptGenV
    {
        public ScriptGenV() 
            => InitializeComponent();

        private void ClearFocus(FrameworkElement focusElement)
        {
            if (focusElement == null)
                return;

            FrameworkElement parent = (FrameworkElement)focusElement.Parent;
            while (parent != null && parent is IInputElement && !((IInputElement)parent).Focusable)
                parent = (FrameworkElement)parent.Parent;

            DependencyObject scope = FocusManager.GetFocusScope(focusElement);
            FocusManager.SetFocusedElement(scope, parent as IInputElement);

            Keyboard.ClearFocus();
        }

        private void ClearFocus(object sender, RoutedEventArgs e)
            => ClearFocus((FrameworkElement)FocusManager.GetFocusedElement(this));

        private void LostFocusOnEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ClearFocus((FrameworkElement)sender);

                e.Handled = true;
            }
        }
    }
}
