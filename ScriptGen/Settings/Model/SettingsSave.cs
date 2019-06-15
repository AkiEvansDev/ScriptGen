using System;

namespace ScriptGen.Settings.Model
{
    /// <summary>
    /// Модель для сохранения настроек
    /// </summary>
    [Serializable]
    public struct SettingsSave
    {
        public CommonM Common;

        public bool IsDark;
        public string ColorName;

        public SettingsSave(CommonM common, ThemeM theme)
        {
            Common = common;

            IsDark = theme.IsDark;
            ColorName = theme.SelectColor.Name;
        }
    }
}
