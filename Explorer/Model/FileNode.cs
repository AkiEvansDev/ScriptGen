using System;
using System.IO;

using Explorer.Abstraction;

using MaterialDesignThemes.Wpf;

namespace Explorer.Model
{
    /// <summary>
    /// Файл проводника
    /// </summary>
    internal class FileNode : ExplorerNodeBase
    {
        /// <summary>
        /// Создаёт новый объект с информацией о файле
        /// </summary>
        /// <param name="file">Информация о файле</param>
        public FileNode(FileInfo file) :
            base(file.Name, file.FullName)
        {
            IsEditable = true;

            switch (file.Extension)
            {
                case ".xml":
                    Kind = PackIconKind.FileXml;
                    break;
                case ".png":
                case ".jpg":
                case ".jpeg":
                case ".ttf":
                    Kind = PackIconKind.FileImage;
                    break;
                default:
                    Kind = PackIconKind.File;
                    break;
            }
        }

        /// <summary>
        /// Имя файла
        /// </summary>
        public override string Name
        {
            get => base.Name;
            set
            {
                if (string.IsNullOrEmpty(base.Name))
                    base.Name = value;
                else if (Rename(value))
                    base.Name = value;
            }
        }

        /// <summary>
        /// Переименовывает файл
        /// </summary>
        /// <param name="newName">Новое имя</param>
        /// <returns>true/false успешно ли переименование</returns>
        private bool Rename(string newName)
        {
            if (!string.IsNullOrEmpty(newName))
            {
                try
                {
                    FileInfo file = new FileInfo(FullPatch);
                    string path = Path.Combine(file.DirectoryName, newName);

                    file.MoveTo(path);
                    FullPatch = path;

                    return true;
                }
                catch (Exception e)
                {
                    FileDialog.Error(new ExplorerException($"Не удалось переименовать {Name} в {newName}!", e.Message, "перейти",
                        () => FileDialog.OpenExplorer(FullPatch)));
                }
            }

            return false;
        }
    }
}
