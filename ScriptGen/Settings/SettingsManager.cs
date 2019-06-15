using ScriptGen.Common;
using ScriptGen.Settings.Model;

using ScriptGen.ViewModel;

namespace ScriptGen.Settings
{
    /// <summary>
    /// Осуществляет работу с данными настроек
    /// </summary>
    public static class SettingsManager
    {
        public static void Save(SettingsSave settings)
        {
            if (!FileWork.BinarySave(settings, @"Data\settings.bin", out string error))
                StatusBarManagerVM.Error("Не удалось сохранить настройки!", error);
        }

        public static SettingsSave? Load()
        {
            LoadOptions();
            LoadTemplates();

            if (FileWork.BinaryLoad(@"Data\settings.bin", out string error, out object settingsObject) 
                && settingsObject.GetType() == typeof(SettingsSave))
            {
                SettingsSave settings = (SettingsSave)settingsObject;

                Info.ActiveOptionIndex = settings.Common.ActiveOptionIndex;
                Info.ActiveTemplateSQLIndex = settings.Common.ActiveTemplateSQLIndex;
                Info.ActiveTemplateProgrammingIndex = settings.Common.ActiveTemplateProgrammingIndex;

                return settings;
            }

            return null;
        }

        public static void LoadOption(string path)
        {
            if (SettingsFileWork.LoadOption(path, out OptionData option))
                if (SettingsFileWork.SaveLocalOption(path))
                    Info.AddOption(option);
        }

        public static void LoadTemplate(string path)
        {
            if (SettingsFileWork.LoadTemplate(path, out TemplateData template))
                if (SettingsFileWork.SaveLocalTemplate(path))
                    Info.AddTemplate(template);
        }

        private static void LoadOptions()
        {
            if (SettingsFileWork.GetAllOptionsPath(out string[] paths))
                for (int i = 0; i < paths.Length; i++)
                    if (SettingsFileWork.LoadOption(paths[i], out OptionData option))
                        Info.AddOption(option);
        }

        private static void LoadTemplates()
        {
            if (SettingsFileWork.GetAllTemplatesPath(out string[] paths))
                for (int i = 0; i < paths.Length; i++)
                    if (SettingsFileWork.LoadTemplate(paths[i], out TemplateData template))
                        Info.AddTemplate(template);
        }
    }
}
