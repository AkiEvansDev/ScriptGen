using System;
using System.Windows.Media;

using BaseMVVM.Abstraction;
using BaseMVVM.Command;

using MaterialDesignColors;

namespace ScriptGen.Settings.Model
{
    /// <summary>
    /// Модель цвета
    /// </summary>
    public class ColorM : ViewModelBase
    {
        public Action<ColorM> OnSetColor;
        
        public Swatch Swatch;
        
        private bool isSelect;

        public ColorM(Swatch swatch)
        {
            Swatch = swatch;

            Name = $"{char.ToUpper(Swatch.Name[0])}{swatch.Name.Substring(1)}";
            Color = new SolidColorBrush(Swatch.ExemplarHue.Color);

            SelectColor = new SimpleCommand(() => OnSetColor?.Invoke(this));
        }
        
        public SimpleCommand SelectColor { get; }

        public string Name { get; }
        public SolidColorBrush Color { get; }

        public bool IsSelect
        {
            get => isSelect;
            set
            {
                isSelect = value;

                OnPropertyChanged();
            }
        }
    }
}
