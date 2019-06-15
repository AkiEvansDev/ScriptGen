using System.Windows;

namespace ScriptGen.BindingProperty
{
    public class DataBinding
    {
        public static readonly DependencyProperty DataBindProperty =
            DependencyProperty.RegisterAttached("DataBind", typeof(DataBindCollection),
            typeof(DataBinding), new UIPropertyMetadata(null));

        public static void SetDataBind(DependencyObject obj, DataBindCollection value) 
            => obj.SetValue(DataBindProperty, value);

        public static DataBindCollection GetDataBind(DependencyObject obj) 
            => (DataBindCollection)obj.GetValue(DataBindProperty);
    }

    public class DataBindCollection : FreezableCollection<DataBind> { }

    public class DataBind : Freezable
    {
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(object), typeof(DataBind),
            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnSourceChanged)));

        public object Source
        {
            get => GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        private static void OnSourceChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e) 
            => ((DataBind)depObj).OnSourceChanged(e);

        protected virtual void OnSourceChanged(DependencyPropertyChangedEventArgs e)
            => Target = e.NewValue;
        
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target", typeof(object),
            typeof(DataBind), new FrameworkPropertyMetadata(null));
        
        public object Target
        {
            get => GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }

        protected override Freezable CreateInstanceCore() 
            => new DataBind();
    }
}
