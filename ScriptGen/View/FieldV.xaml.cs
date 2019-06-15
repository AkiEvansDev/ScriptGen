using System.Windows.Controls;

namespace ScriptGen.View
{
    public partial class FieldV : UserControl
    {
        public FieldV() 
            => InitializeComponent();

        private void StopScrollChanged(object sender, ScrollChangedEventArgs e) 
            => e.Handled = true;
    }
}
