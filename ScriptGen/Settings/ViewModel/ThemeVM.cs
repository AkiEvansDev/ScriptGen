using System.Collections.ObjectModel;
using System.Linq;

using BaseMVVM.Abstraction;

using MaterialDesignColors;

using MaterialDesignThemes.Wpf;

using ScriptGen.Settings.Model;

namespace ScriptGen.Settings.ViewModel
{
    /// <summary>
    /// Управляет темами приложения
    /// </summary>
    public class ThemeVM : ViewModelBase
    {
        private ThemeM theme;
        private ThemeM oldTheme;

        public ThemeVM(bool isDark, string colorName)
        {
            Swatch[] swatches = new SwatchesProvider().Swatches.ToArray();

            foreach (Swatch swatch in new SwatchesProvider().Swatches)
            {
                ColorM color = new ColorM(swatch)
                {
                    OnSetColor = SetColor
                };

                Colors.Add(color);
            }

            IsDark = isDark;
            theme.SelectColor = Colors.FirstOrDefault(c => c.Name == colorName);
            SelectColor.IsSelect = true;

            Accept();
        }

        public ObservableCollection<ColorM> Colors { get; }
            = new ObservableCollection<ColorM>();

        public bool IsDark
        {
            get => theme.IsDark;
            set
            {
                theme.IsDark = value;

                OnPropertyChanged();
            }
        }

        public ColorM SelectColor
        {
            get => theme.SelectColor;
            set
            {
                SelectColor.IsSelect = false;
                value.IsSelect = true;

                theme.SelectColor = value;

                OnPropertyChanged();
            }
        }

        private void SetColor(ColorM newColor)
        {
            if (newColor != SelectColor)
                SelectColor = newColor;
        }

        public void Save()
            => oldTheme = theme;

        public void Accept()
        {
            new PaletteHelper().SetLightDark(IsDark);

            new PaletteHelper().ReplacePrimaryColor(SelectColor.Swatch);
            if (SelectColor.Swatch.IsAccented)
                new PaletteHelper().ReplaceAccentColor(SelectColor.Swatch);
        }

        public void Cancel()
        {
            IsDark = oldTheme.IsDark;
            SelectColor = oldTheme.SelectColor;
        }

        public ThemeM GetTheme()
            => theme;
    }
}
