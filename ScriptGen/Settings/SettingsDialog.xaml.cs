using System.ComponentModel;

using ScriptGen.Settings.Model;
using ScriptGen.Settings.ViewModel;

namespace ScriptGen.Settings
{
    public partial class SettingsDialog
    {
        private SettingsVM settings;

        public SettingsDialog()
        {
            InitializeComponent();

            SettingsSave? loadSettings = SettingsManager.Load();

            settings = loadSettings != null 
                ? new SettingsVM(loadSettings.Value.IsDark, loadSettings.Value.ColorName) 
                : new SettingsVM(true, "Teal");
            settings.Close = DialogClose;

            DataContext = settings;
        }

        public void Open()
        {
            settings.SaveSettings();

            ShowDialog();
        }

        private void SettingsClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            settings.CancelSettings();

            DialogClose();
        }

        private void DialogClose() 
            => Hide();
    }
}
