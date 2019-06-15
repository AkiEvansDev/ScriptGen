using System.Diagnostics;
using System.IO;

namespace DataDictionary
{
    /// <summary>
    /// Класс для работы с файлами
    /// </summary>
    public static class FileWork
    {
        /// <summary>
        /// Открывает путь в проводнике windows
        /// </summary>
        /// <param name="path">Путь</param>
        public static void OpenInExplorer(string path)
        {
            if (Directory.Exists(path) || File.Exists(path))
                Process.Start(new ProcessStartInfo("explorer.exe", " /select, " + path));
            else
                App.MainAPI.Info("Путь не найден, возможно файл удалён или перемещён!", path);
        }

        /// <summary>
        /// Получает уникальное имя для файла
        /// </summary>
        /// <param name="path">Путь к папке</param>
        /// <param name="startName">Имя файла</param>
        /// <param name="extension">Расширение</param>
        /// <returns>Уникальное имя</returns>
        public static string GetUniqueFileName(string path, string startName, string extension)
        {
            string name = $"{startName}{extension}";
            int count = 1;

            while (File.Exists(Path.Combine(path, name)))
            {
                name = $"{startName} ({count}){extension}";
                count++;
            }

            return Path.Combine(path, name);
        }
    }
}
