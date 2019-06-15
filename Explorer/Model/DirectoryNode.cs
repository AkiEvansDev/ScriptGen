using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

using Explorer.Abstraction;
using Explorer.Interface;

using MaterialDesignThemes.Wpf;

namespace Explorer.Model
{
    /// <summary>
    /// Папка проводника
    /// </summary>
    internal class DirectoryNode : ExplorerNodeBase
    {
        /// <summary>
        /// Расширения файлов для отображения
        /// </summary>
        public static string[] AcceptFileTypes { get; set; }
            = new string[0] { };

        /// <summary>
        /// Создаёт новый объект с информацией о папке
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="fullname">Путь</param>
        /// <param name="kind">Иконка</param>
        public DirectoryNode(string name, string fullname, PackIconKind kind) :
            base(name, fullname)
        {
            Reset();

            Kind = kind;

            OnExpanded += (e) =>
            {
                if (e)
                {
                    IsSelected = true;

                    Update();
                }
            };
        }

        /// <summary>
        /// Создаёт новый объект с информацией о папке
        /// </summary>
        /// <param name="directory">Информация о папке</param>
        public DirectoryNode(DirectoryInfo directory) :
            this(directory.Name, directory.FullName, PackIconKind.Folder)
            => IsEditable = true;

        /// <summary>
        /// Создаёт новый объект с информацией о диске
        /// </summary>
        /// <param name="drive">Информация о диске</param>
        public DirectoryNode(DriveInfo drive) :
            this(drive.Name, drive.RootDirectory.FullName, PackIconKind.Harddisk)
            => IsEditable = false;

        /// <summary>
        /// Имя папки
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
        /// Проверяет содержит ли папка дочерние папки
        /// </summary>
        /// <returns>true/false существуют ли дочерние папки</returns>
        private bool HaveFolder()
        {
            DirectoryInfo dir;

            foreach (string path in Directory.EnumerateDirectories(FullPatch))
            {
                try
                {
                    Directory.EnumerateDirectories(path);

                    dir = new DirectoryInfo(path);
                }
                catch { continue; }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Проверяет содержит ли папка дочерние файлы
        /// </summary>
        /// <returns>true/false существуют ли дочерние файлы</returns>
        private bool HaveFile()
        {
            FileInfo file;

            foreach (string path in Directory.EnumerateFiles(FullPatch))
            {
                try { file = new FileInfo(path); }
                catch { continue; }

                if (AcceptFileTypes.Length > 0 && AcceptFileTypes.Contains(file.Extension))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Заполняет информацию о дочерних папках
        /// </summary>
        private void SetDirectories()
        {
            DirectoryInfo dir;

            foreach (string path in Directory.EnumerateDirectories(FullPatch))
            {
                try
                {
                    Directory.EnumerateDirectories(path);

                    dir = new DirectoryInfo(path);
                }
                catch { continue; }

                ChildNode.Add(new DirectoryNode(dir));
            }

        }

        /// <summary>
        /// Заполняет информацию о дочерних файлах
        /// </summary>
        private void SetFiles()
        {
            FileInfo file;

            foreach (string path in Directory.EnumerateFiles(FullPatch))
            {
                try { file = new FileInfo(path); }
                catch { continue; }

                if (AcceptFileTypes.Contains(file.Extension))
                    ChildNode.Add(new FileNode(file));
            }
        }

        /// <summary>
        /// Обновляет информацию о дочерних элементах папки
        /// </summary>
        public void Update()
        {
            ChildNode.Clear();

            SetDirectories();
            SetFiles();
        }

        /// <summary>
        /// Переименовывает папку
        /// </summary>
        /// <param name="newName">Новое имя</param>
        /// <returns>true/false успешно ли переименование</returns>
        private bool Rename(string newName)
        {
            if (!string.IsNullOrEmpty(newName) && Name != newName)
            {
                try
                {
                    DirectoryInfo directory = new DirectoryInfo(FullPatch);
                    directory.MoveTo(Path.Combine(directory.Parent.FullName, newName));

                    FullPatch = directory.FullName;

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

        /// <summary>
        /// Выделяет дочернюю папку
        /// </summary>
        /// <param name="folders">Список папок для выделения</param>
        /// <param name="index">Индекс папки для выделения</param>
        public void Select(string[] folders, int index)
        {
            if (index >= folders.Length || string.IsNullOrEmpty(folders[index]))
                return;

            INodeTree node = ChildNode.FirstOrDefault((n)
                => n is DirectoryNode directory && directory.Name == folders[index] ? true : false);

            if (node != null)
            {
                node.IsExpanded = true;
                index++;

                ((DirectoryNode)node).Select(folders, index);
            }
            else
            {
                node = ChildNode.FirstOrDefault((n)
                    => n is FileNode file && file.Name == folders[index] ? true : false);

                if (node != null)
                    node.IsSelected = true;
            }
        }

        /// <summary>
        /// Обновляет объект
        /// </summary>
        public void Reset()
        {
            ChildNode = new ObservableCollection<INodeTree>();

            if (HaveFolder() || HaveFile())
                ChildNode.Add(null);
        }
    }
}
