using System;
using System.IO;
using System.Linq;

using BaseMVVM.Command;

using ScriptGen.API;
using ScriptGen.ViewModel;

namespace ScriptGen.Common
{
    /// <summary>
    /// Работа с файлами для плагинов
    /// </summary>
    public static class PluginFileWork
    {
        /// <summary>
        /// Сохраняет файлы плагина локально
        /// </summary>
        /// <param name="pluginData">Данные о плагине</param>
        /// <param name="newPluginData">Измененные данные о плагине</param>
        /// <returns>true/false успешно ли сохранение</returns>
        public static bool SaveLocalPlugin(PluginData pluginData, out PluginData newPluginData)
        {
            newPluginData = new PluginData();
            string path = "";
            
            try
            {
                DirectoryInfo directory = new DirectoryInfo(@"Data\Plugins");
                if (!directory.Exists)
                    directory = Directory.CreateDirectory(@"Data\Plugins");

                path = FileWork.GetUniqueDirectoryName(directory.FullName, pluginData.Name);
                Directory.CreateDirectory(path);

                pluginData.ImagePath = GetPluginFilePath(FileWork.MoveFile(pluginData.ImagePath, path));
                pluginData.DllPath = GetPluginFilePath(FileWork.MoveFile(pluginData.DllPath, path));

                for (int i = 0; i < pluginData.Files.Length; i++)
                    pluginData.Files[i] = GetPluginFilePath(FileWork.MoveFile(pluginData.Files[i], path));
            }
            catch (Exception e)
            {
                StatusBarManagerVM.Error($"Не удалось скопировать ресурсы плагин \"{pluginData.Name}\"!", e.Message);
                return false;
            }

            path = Path.Combine(path, "config.xml");
            if (FileWork.XmlSave(pluginData, path, typeof(PluginData), out string message))
            {
                StatusBarManagerVM.Message($"Плагин \"{pluginData.Name}\" скопирован локально!");
                newPluginData = pluginData;

                return true;
            }
            else
                StatusBarManagerVM.Error($"Не удалось скопировать плагин \"{pluginData.Name}\"!");

            return false;
        }

        private static string GetPluginFilePath(string path)
            => path.Substring(path.IndexOf(@"\Data\Plugins"));

        /// <summary>
        /// Загружает данные о плагине по указанному пути
        /// </summary>
        /// <param name="path">Путь к папке плагина</param>
        /// <param name="optionData">Данные о плагине</param>
        /// <returns>true/false успешна ли загрузка</returns>
        public static bool LoadPlugin(string path, out PluginData pluginData)
        {
            pluginData = new PluginData();

            if (FileWork.XmlLoad(path, typeof(PluginData), out string message, out object pd))
            {
                pluginData = (PluginData)pd;
                pluginData.ImagePath = Path.GetFullPath(pluginData.ImagePath);

                return true;
            }
            else
            {
                StatusBarManagerVM.Error($"Не удалось загрузить плагин!", message, "перейти",
                    new SimpleCommand(() => FileWork.OpenInExplorer(path)));
            }

            return false;
        }

        /// <summary>
        /// Получает пути всех папок плагинов
        /// </summary>
        /// <param name="paths">Пути</param>
        /// <returns>true/false были ли получены пути</returns>
        public static bool GetAllPluginsPath(out string[] paths)
        {
            paths = new string[0];

            try
            {
                if (Directory.Exists(@"Data\Plugins"))
                    paths = Directory.GetDirectories(@"Data\Plugins").Select(p => Path.Combine(p, "config.xml")).ToArray();

                if (paths.Length > 0)
                    paths = RemoveDisablePlugin(paths);

                return paths.Length > 0;
            }
            catch (Exception e) { StatusBarManagerVM.Error($"Не удалось загрузить плагины!", e.Message); }

            return false;
        }

        /// <summary>
        /// Удаляет папки отключённых плагинов
        /// </summary>
        /// <param name="paths">Пути к папкам плагинов</param>
        /// <returns>Пути активных плагинов</returns>
        private static string[] RemoveDisablePlugin(string[] paths)
        {
            try
            {
                for (int i = 0; i < paths.Length; i++)
                    if (!File.Exists(paths[i]))
                    {
                        FileInfo file = new FileInfo(paths[i]);
                        Directory.Delete(file.Directory.FullName, true);

                        paths[i] = null;
                    }
            }
            catch (Exception e) { StatusBarManagerVM.Error("Не удалось удалить папку плагина!", e.Message); }

            return paths.Where(p => p != null).ToArray();
        }

        /// <summary>
        /// Отключает плагин
        /// </summary>
        /// <param name="path">Путь к папке плагина</param>
        /// <returns>true/false успешно ли отключение</returns>
        public static bool DisablePlugin(string path)
        {
            try
            {
                FileInfo file = new FileInfo(path);
                File.Delete(Path.Combine(file.Directory.FullName, "config.xml"));

                StatusBarManagerVM.Info("Плагин отключён!", "Файлы плагина будут удалены при следующем запуске программы!");

                return true;
            }
            catch (Exception e) { StatusBarManagerVM.Error("Не удалось удалить плагин!", e.Message); }

            return false;
        }
    }
}
