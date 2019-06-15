using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ScriptGen.View
{
    public partial class LineV : UserControl
    {
        private bool isSourceDarg;
        private bool isConnectionDarg;
        private bool isTargetDarg;

        public LineV() 
            => InitializeComponent();

        private void StartDrag(Cursor cursor, MouseButtonEventArgs e)
        {
            MainThumb.Visibility = Visibility.Visible;
            Mouse.OverrideCursor = cursor;

            MainThumb.RaiseEvent(e);
        }

        private void SourceLineMouseDown(object sender, MouseButtonEventArgs e)
        {
            isSourceDarg = true;

            StartDrag(((FrameworkElement)sender).Cursor, e);
        }

        private void ConnectionLineMouseDown(object sender, MouseButtonEventArgs e)
        {
            isConnectionDarg = true;

            StartDrag(((FrameworkElement)sender).Cursor, e);
        }

        private void TargetLineMouseDown(object sender, MouseButtonEventArgs e)
        {
            isTargetDarg = true;

            StartDrag(((FrameworkElement)sender).Cursor, e);
        }

        private void ThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            double hDelta = Mouse.GetPosition(this).X;
            double vDelta = Mouse.GetPosition(this).Y;

            if (isSourceDarg)
                SourceLine.Y1 = vDelta;
            else if (isConnectionDarg)
                ConnectionLine.X1 = hDelta;
            else if (isTargetDarg)
                TargetLine.Y1 = vDelta;
        }

        private void ThumbDragCompleted(object sender, DragCompletedEventArgs e)
        {
            isSourceDarg = isConnectionDarg = isTargetDarg = false;

            MainThumb.Visibility = Visibility.Collapsed;
            Mouse.OverrideCursor = null;
        }
    }
}
