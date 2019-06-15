using System;
using System.IO;

using BaseMVVM.Command;

using ScriptGen.Settings.Model;
using ScriptGen.ViewModel;

namespace ScriptGen.Common
{
    /// <summary>
    /// Работа с файлами для настроек
    /// </summary>
    public static class SettingsFileWork
    {
        /// <summary>
        /// Сохраняет файл локально
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="folderName">Путь к папку</param>
        /// <param name="errorMessage">Сообщение при ошибке</param>
        /// <returns>true/false успешно ли сохранение</returns>
        private static bool SaveLocal(string filePath, string folderName, string errorMessage)
        {
            try
            {
                DirectoryInfo directory = new DirectoryInfo(folderName);
                if (!directory.Exists)
                    directory = Directory.CreateDirectory(folderName);

                FileWork.MoveFile(filePath, directory.FullName);

                return true;
            }
            catch (Exception e) { StatusBarManagerVM.Error(errorMessage, e.Message); }

            return false;
        }

        /// <summary>
        /// Локально сохраняет файл шаблона
        /// </summary>
        /// <param name="path">Путь к файлу шаблона</param>
        /// <returns>true/false успешно ли сохранение</returns>
        public static bool SaveLocalTemplate(string path)
            => SaveLocal(path, @"Data\Templates", "Не удалось скопировать файл шаблона!");

        /// <summary>
        /// Локально сохраняет файл настроек
        /// </summary>
        /// <param name="path">Путь к файлу настроек</param>
        /// <returns>true/false успешно ли сохранение</returns>
        public static bool SaveLocalOption(string path)
            => SaveLocal(path, @"Data\Options", "Не удалось скопировать файл настроек!");

        /// <summary>
        /// Загружает шаблон по указанному пути
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="optionData">Шаблон</param>
        /// <returns>true/false успешна ли загрузка</returns>
        public static bool LoadTemplate(string path, out TemplateData templateData)
        {
            templateData = new TemplateData();

            if (FileWork.XmlLoad(path, typeof(TemplateData), out string message, out object td))
            {
                templateData = (TemplateData)td;
                return true;
            }
            else
            {
                StatusBarManagerVM.Error($"Не удалось загрузить шаблон!", message, "перейти",
                    new SimpleCommand(() => FileWork.OpenInExplorer(path)));
            }

            return false;
        }

        /// <summary>
        /// Загружает экземпляр настройки по указанному пути
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="optionData">Экземпляр настроек</param>
        /// <returns>true/false успешна ли загрузка</returns>
        public static bool LoadOption(string path, out OptionData optionData)
        {
            optionData = new OptionData();

            if (FileWork.XmlLoad(path, typeof(OptionData), out string message, out object od))
            {
                optionData = (OptionData)od;
                return true;
            }
            else
            {
                StatusBarManagerVM.Error($"Не удалось загрузить вариант настроек!", message, "перейти",
                    new SimpleCommand(() => FileWork.OpenInExplorer(path)));
            }

            return false;
        }

        /// <summary>
        /// Получает пути всех файлов в папке
        /// </summary>
        /// <param name="paths">Пути</param>
        /// <param name="folderName">Папка</param>
        /// <param name="errorMessage">Сообщение при ошибке</param>
        /// <returns>true/false были ли получены пути</returns>
        private static bool GetAllPath(out string[] paths, string folderName, string errorMessage)
        {
            paths = new string[0];

            try
            {
                if (Directory.Exists(folderName))
                    paths = Directory.GetFiles(folderName);

                return paths.Length > 0;
            }
            catch (Exception e) { StatusBarManagerVM.Error(errorMessage, e.Message); }

            return false;
        }

        /// <summary>
        /// Получает пути всех файлов шаблонов
        /// </summary>
        /// <param name="paths">Пути</param>
        /// <returns>true/false были ли получены пути</returns>
        public static bool GetAllTemplatesPath(out string[] paths)
            => GetAllPath(out paths, @"Data\Templates", "Не удалось загрузить шаблон!");

        /// <summary>
        /// Получает пути всех файлов настроек
        /// </summary>
        /// <param name="paths">Пути</param>
        /// <returns>true/false были ли получены пути</returns>
        public static bool GetAllOptionsPath(out string[] paths)
            => GetAllPath(out paths, @"Data\Options", "Не удалось загрузить вариант настроек!");
    }
}

