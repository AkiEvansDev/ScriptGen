using System.Windows.Controls;
using System.Windows.Input;

namespace ScriptGen.View
{
    public partial class PageV : UserControl
    {
        public PageV() 
            => InitializeComponent();

        private void UserControlPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Scroll.Focus();

            if (Keyboard.IsKeyDown(Key.LeftCtrl))
                Scroll.ScrollToHorizontalOffset(Scroll.HorizontalOffset - e.Delta);
            else
                Scroll.ScrollToVerticalOffset(Scroll.VerticalOffset - e.Delta);

            e.Handled = true;
        }
    }
}
