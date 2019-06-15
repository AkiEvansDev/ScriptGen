using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Input;

namespace Explorer.View
{
    public partial class ExplorerV : UserControl
    {
        public ExplorerV()
            => InitializeComponent();

        private void SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
            => PathText.Tag = e.NewValue;

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

        private void LostFocusOnEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ClearFocus(PathText);

                e.Handled = true;
            }
        }

        private void UserControlPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Scroll.ScrollToVerticalOffset(Scroll.VerticalOffset - e.Delta);

            e.Handled = true;
        }

        private void ButtonClick(Button button)
        {
            ButtonAutomationPeer peer = new ButtonAutomationPeer(button);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();
        }

        private void UserControlKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Dialog.IsOpen)
                    ButtonClick(AcceptDialog);
                else
                {
                    Accept.Focus();
                    ButtonClick(Accept);
                }
            }
            else if (e.Key == Key.Escape)
            {
                if (Dialog.IsOpen)
                    ButtonClick(CancelDialog);
                else
                    ButtonClick(Cancel);
            }
        }
    }
}
