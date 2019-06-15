using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

using BaseMVVM.Command;

using ScriptGen.ViewModel;

namespace ScriptGen.Common
{
    /// <summary>
    /// Осуществляет работу с файлами
    /// </summary>
    public static class FileWork
    {
        public static void OpenInExplorer(string path)
        {
            if (Directory.Exists(path) || File.Exists(path))
                Process.Start(new ProcessStartInfo("explorer.exe", " /select, " + path));
            else
                StatusBarManagerVM.Info("Путь не найден, возможно файл удалён или перемещён!", path);
        }

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

        public static string GetUniqueDirectoryName(string path, string startName)
        {
            string name = startName;
            int count = 1;

            while (Directory.Exists(Path.Combine(path, name)))
            {
                name = $"{startName} ({count})";
                count++;
            }

            return Path.Combine(path, name);
        }

        public static string MoveFile(string filePath, string folderPath)
        {
            FileInfo file = new FileInfo(filePath);
            string newPath = Path.Combine(folderPath, file.Name);

            if (File.Exists(newPath))
                newPath = GetUniqueFileName(folderPath, file.Name.Substring(0, file.Name.Length - file.Extension.Length), file.Extension);

            File.Copy(file.FullName, newPath);

            return newPath;
        }

        public static bool XmlSave(object saveObject, string path, Type type, out string errorMessage)
        {
            errorMessage = null;

            try
            {
                XmlSerializer formatter = new XmlSerializer(type);
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    formatter.Serialize(fs, saveObject);
                }

                return true;
            }
            catch (Exception e) { errorMessage = e.Message; }

            return false;
        }

        public static bool BinarySave(object saveObject, string path, out string errorMessage)
        {
            errorMessage = null;

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    formatter.Serialize(fs, saveObject);
                }

                return true;
            }
            catch (Exception e) { errorMessage = e.Message; }

            return false;
        }

        public static bool XmlLoad(string path, Type type, out string errorMessage, out object loadObject)
        {
            loadObject = null;
            errorMessage = null;

            try
            {
                XmlSerializer formatter = new XmlSerializer(type);
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    loadObject = formatter.Deserialize(fs);
                }

                return true;
            }
            catch (Exception e) { errorMessage = e.Message; }

            return false;
        }

        public static bool BinaryLoad(string path, out string errorMessage, out object loadObject)
        {
            loadObject = null;
            errorMessage = null;

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    loadObject = formatter.Deserialize(fs);
                }

                return true;
            }
            catch (Exception e) { errorMessage = e.Message; }

            return false;
        }

        public static void TextSave(StringBuilder script, string path)
        {
            try
            {
                File.WriteAllText(path, script.ToString(), Encoding.UTF8);
                FileInfo file = new FileInfo(path);

                StatusBarManagerVM.Message($"Файл \"{file.Name}\" сохранен!", "перейти",
                    new SimpleCommand(() => OpenInExplorer(path)));
            }
            catch (Exception e) { StatusBarManagerVM.Error($"Не удалось сохранить файл!", e.Message); }
        }

        public static void TextSave(StringBuilder script, string path, string name, string extension)
        {
            if (!File.Exists(path) && Directory.Exists(path))
                path = GetUniqueFileName(path, name, extension);

            try
            {
                File.WriteAllText(path, script.ToString(), Encoding.UTF8);
                FileInfo file = new FileInfo(path);

                StatusBarManagerVM.Message($"Файл \"{file.Name}\" сохранен!", "перейти",
                    new SimpleCommand(() => OpenInExplorer(path)));
            }
            catch (Exception e) { StatusBarManagerVM.Error($"Не удалось сохранить файл!", e.Message); }
        }
    }
}
