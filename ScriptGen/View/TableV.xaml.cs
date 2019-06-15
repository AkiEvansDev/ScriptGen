using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ScriptGen.View
{
    public partial class TableV : UserControl
    {
        public TableV() 
            => InitializeComponent();

        private void Move_DragDelta(object sender, DragDeltaEventArgs e)
            => Margin = new Thickness(Margin.Left + e.HorizontalChange, Margin.Top + e.VerticalChange, 0, 0);

        private void Resize_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (Width + e.HorizontalChange > 0)
                Width = Width + e.HorizontalChange;
        }

        private void StopScrollChanged(object sender, ScrollChangedEventArgs e) 
            => e.Handled = true;
    }
}
