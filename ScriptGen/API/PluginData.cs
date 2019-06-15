using System;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

using BaseMVVM.Command;

using ScriptGen.ViewModel;
using ScriptGenPlugin.Interface;
using ScriptGen.Common;

namespace ScriptGen.API
{
    /// <summary>
    /// Модель загружаемого плагина
    /// </summary>
    public struct PluginData
    {
        /// <summary>
        /// Название плагина
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание плагина
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Путь к изображению
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Путь к dll файлу плагина 
        /// </summary>
        public string DllPath;
        /// <summary>
        /// Пространство имён для точки входа в плагин
        /// </summary>
        public string Namespace;
        /// <summary>
        /// Название класса, являющийся точкой входа в плагин
        /// </summary>
        public string ClassName;

        /// <summary>
        /// Дополнительные файлы плагина
        /// </summary>
        public string[] Files;

        /// <summary>
        /// Контент кнопки действия плагина
        /// </summary>
        [XmlIgnore]
        public object ActionContent { get; private set; }
        /// <summary>
        /// Подсказка кнопки действия плагина
        /// </summary>
        [XmlIgnore]
        public string ActionToolTip { get; private set; }

        /// <summary>
        /// Команда плагина
        /// </summary>
        [XmlIgnore]
        public SimpleCommand PluginCommand { get; set; }
        /// <summary>
        /// Удаление плагина
        /// </summary>
        [XmlIgnore]
        public SimpleCommand RemovePlaugin { get; set; }
        /// <summary>
        /// Команда открытия папки плагина
        /// </summary>
        [XmlIgnore]
        public SimpleCommand OpenInExplorer { get; set; }

        /// <summary>
        /// Загруженный плагин
        /// </summary>
        [XmlIgnore]
        public IPlugin Plugin;

        /// <summary>
        /// Проверяет, содержат ли данные плагина ошибки
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            if (IsNotEmptyStringsError())
            {
                StatusBarManagerVM.Info("Для загрузки плагина необходимо заполнить все данные!");
                return false;
            }

            if (IsFileExistsError())
            {
                StatusBarManagerVM.Info($"Не удалось загрузить файлы плагина \"{Name}\"!");
                return false;
            }

            return IsDataError();
        }

        /// <summary>
        /// Проверяет строковые значения плагина на пустоту
        /// </summary>
        /// <returns>Есть ли пустые значения</returns>
        private bool IsNotEmptyStringsError() 
            => string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Description) ||
            string.IsNullOrWhiteSpace(ImagePath) || string.IsNullOrWhiteSpace(DllPath) ||
            string.IsNullOrWhiteSpace(Namespace) || string.IsNullOrWhiteSpace(ClassName);

        /// <summary>
        /// Проверяет существование всех файлов плагина
        /// </summary>
        /// <returns>Есть ли несуществующий файл</returns>
        private bool IsFileExistsError()
            => !File.Exists(ImagePath) || !File.Exists(DllPath) || (Files.Length > 0 && !Files.All(f => File.Exists(f)));

        /// <summary>
        /// Проверяет корректность точки входа в плагин
        /// </summary>
        private bool IsDataError()
        {
            try
            {
                Assembly asm = Assembly.LoadFrom(DllPath);
                Type type = asm.GetType($"{Namespace}.{ClassName}", true, false);

                if (type.GetInterface("IPlugin") == null)
                {
                    StatusBarManagerVM.Info($"Не удалось найти реализацию интерфейса в плагине \"{Name}\"!");
                    return false;
                }
                
                Plugin = (IPlugin)Activator.CreateInstance(type);
                Plugin.Start(PluginManager.CommonAPI);

                ActionContent = Plugin.ActionContent;
                ActionToolTip = Plugin.ActionToolTip;
                PluginCommand = new SimpleCommand(Plugin.PluginAction, Plugin.CanPluginAction);

                OpenInExplorer = new SimpleCommand(OpenPluginFolder);

                return true;
            }
            catch (Exception e) { StatusBarManagerVM.Error($"При загрузки плагина \"{Name}\" обнаружена ошибка!", e.Message); }

            return false;
        }

        /// <summary>
        /// Открывает папку плагина в проводнике
        /// </summary>
        private void OpenPluginFolder() 
            => FileWork.OpenInExplorer(DllPath);
    }
}
