using System;

using BaseMVVM.Command;
using ScriptGen.Settings.Model;

namespace ScriptGen.Settings.ViewModel
{
    /// <summary>
    /// Управляет настройками
    /// </summary>
    public class SettingsVM
    {
        public Action Close;

        public SettingsVM(bool isDark, string colorName)
        {
            Common = new CommonVM();
            Theme = new ThemeVM(isDark, colorName);

            Accept = new SimpleCommand(AcceptSettings);
            Cancel = new SimpleCommand(CancelSettings);

            SaveSettings();
        }
        
        public SimpleCommand Accept { get; }
        public SimpleCommand Cancel { get; }

        public CommonVM Common { get; }
        public ThemeVM Theme { get; }

        public void SaveSettings()
        {
            Common.Save();
            Theme.Save();
        }

        public void AcceptSettings()
        {
            Common.Accept();
            Theme.Accept();

            SettingsManager.Save(new SettingsSave(Common.GetCommon(), Theme.GetTheme()));

            Close?.Invoke();
        }

        public void CancelSettings()
        {
            Common.Cancel();
            Theme.Cancel();

            Close?.Invoke();
        }
    }
}
