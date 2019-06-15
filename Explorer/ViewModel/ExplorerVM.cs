using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

using BaseMVVM.Abstraction;
using BaseMVVM.Command;

using Explorer.Interface;
using Explorer.Model;

using MaterialDesignThemes.Wpf;

using static System.Environment;

namespace Explorer.ViewModel
{
    /// <summary>
    /// Логики проводника
    /// </summary>
    internal class ExplorerVM : ViewModelBase
    {
        /// <summary>
        /// Вызывается при нажатии на кнопку "ОК" ("Подтвердить")
        /// </summary>
        public Action<string> OnAccept;
        /// <summary>
        /// Вызывается при нажатии на кнопку "Отмена"
        /// </summary>
        public Action OnCancel;

        /// <summary>
        /// true/false какие элементы проводника можно выделять
        /// </summary>
        public ExplorerSelectType SelectType;

        /// <summary>
        /// Выбранный путь
        /// </summary>
        private string selectPath;
        /// <summary>
        /// Выбранный элемент
        /// </summary>
        private INodeTree selectItem;

        /// <summary>
        /// Тип проводника (сохранение/загрузка)
        /// </summary>
        private ExplorerType type;

        /// <summary>
        /// true/false открыто ли диалоговое окно
        /// </summary>
        private bool isDialogOpen;

        /// <summary>
        /// Создаёт объект управления проводникам
        /// </summary>
        public ExplorerVM()
        {
            Reset(ExplorerSelectType.All, ExplorerType.Save);

            SetSpecialFolder();
            SetDrivers();

            NewFolder = new SimpleCommand(CreateFolder, () => !string.IsNullOrEmpty(SelectPath) && SelectItem is DirectoryNode);
            NewFile = new SimpleCommand(CreateFile, () => !string.IsNullOrEmpty(SelectPath) && SelectItem is DirectoryNode);

            Accept = new SimpleCommand(PreviewAccept, CanAccept);
            Cancel = new SimpleCommand(() => OnCancel?.Invoke());

            AcceptDialog = new SimpleCommand(() =>
            {
                IsDialogOpen = false;
                OnAccept?.Invoke(SelectPath);
            });
            CancelDialog = new SimpleCommand(() => IsDialogOpen = false);
        }

        /// <summary>
        /// Команда создания новой папки
        /// </summary>
        public SimpleCommand NewFolder { get; set; }
        /// <summary>
        /// Команда создания нового файла
        /// </summary>
        public SimpleCommand NewFile { get; set; }

        /// <summary>
        /// Команда подтверждения
        /// </summary>
        public SimpleCommand Accept { get; set; }
        /// <summary>
        /// Команда отмены
        /// </summary>
        public SimpleCommand Cancel { get; set; }

        /// <summary>
        /// Команда подтверждения диалога
        /// </summary>
        public SimpleCommand AcceptDialog { get; set; }
        /// <summary>
        /// Команда отмены диалога
        /// </summary>
        public SimpleCommand CancelDialog { get; set; }

        /// <summary>
        /// Список корневых элементов проводника
        /// </summary>
        public ObservableCollection<INodeTree> Drives { get; }
            = new ObservableCollection<INodeTree>();

        /// <summary>
        /// Выбранный путь
        /// </summary>
        public string SelectPath
        {
            get => selectPath;
            set
            {
                selectPath = value;

                SetSelect(value);
            }
        }

        /// <summary>
        /// Выбранный элемент
        /// </summary>
        public object SelectItem
        {
            get => selectItem;
            set
            {
                if (value is DirectoryNode directory)
                {
                    selectPath = directory.FullPatch;
                    selectItem = directory;
                }
                else if (value is FileNode file)
                {
                    selectPath = file.FullPatch;
                    selectItem = file;
                }
                else
                    selectItem = null;

                OnPropertyChanged("SelectPath");
            }
        }

        /// <summary>
        /// Тип проводника (сохранение/загрузка)
        /// </summary>
        public ExplorerType TypeExplorer
        {
            get => type;
            set
            {
                type = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// true/false открыто ли диалоговое окно
        /// </summary>
        public bool IsDialogOpen
        {
            get => isDialogOpen;
            set
            {
                isDialogOpen = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Выделяет папки по указанному пути
        /// </summary>
        /// <param name="path">Путь</param>
        public void SetSelect(string path)
        {
            for (int i = 0; i < Drives.Count; i++)
                Drives[i].IsSelected = Drives[i].IsExpanded = false;

            if (string.IsNullOrEmpty(path))
                return;

            string[] folders = path.Split('\\');

            if (folders.Length > 0)
            {
                INodeTree node = Drives.FirstOrDefault((n)
                    => n is DirectoryNode directory && directory.Name.StartsWith(folders[0]));

                if (node != null)
                {
                    node.IsExpanded = true;

                    ((DirectoryNode)node).Select(folders, 1);
                }
            }
        }

        /// <summary>
        /// Получает информацию о специальной папке
        /// </summary>
        /// <param name="folder">Специальная папка</param>
        /// <returns>Информация о специальной папке</returns>
        private DirectoryInfo GetSpecialFolder(SpecialFolder folder)
            => new DirectoryInfo(GetFolderPath(folder));

        /// <summary>
        /// Заполняет информацию о специальных папках (корневые элементы проводника)
        /// </summary>
        private void SetSpecialFolder()
        {
            try
            {
                DirectoryInfo dir = GetSpecialFolder(SpecialFolder.MyDocuments);
                Drives.Add(new DirectoryNode(dir.Name, dir.FullName, PackIconKind.FileDocument));

                dir = GetSpecialFolder(SpecialFolder.Desktop);
                Drives.Add(new DirectoryNode(dir.Name, dir.FullName, PackIconKind.DesktopMac));
            }
            catch (Exception e)
            {
                FileDialog.Error(new ExplorerException("Не удалось загрузить специальные папки!", e.Message));
            }
        }

        /// <summary>
        /// Заполняет информацию о системных дисках (корневые элементы проводника)
        /// </summary>
        private void SetDrivers()
        {
            DriveInfo drive;

            foreach (string name in GetLogicalDrives())
            {
                try { drive = new DriveInfo(name); }
                catch (Exception e)
                {
                    FileDialog.Error(new ExplorerException($"Не удалось получить доступ к {name}!", e.Message));

                    continue;
                }

                if (drive.IsReady)
                    Drives.Add(new DirectoryNode(drive));
            }
        }

        /// <summary>
        /// Обновляет информацию о выделенном элементе
        /// </summary>
        private void UpdateSelectItem()
        {
            if (SelectItem is DirectoryNode directory)
                directory.Update();
        }

        /// <summary>
        /// Получает уникальный путь для элемента с данным именем
        /// </summary>
        /// <param name="name">Имя элемента</param>
        /// <returns>Уникальный путь</returns>
        private string GetUniquePath(string name)
        {
            string path = Path.Combine(SelectPath, name);
            int count = 1;

            while (Directory.Exists(path))
            {
                path = Path.Combine(SelectPath, $"{name} ({count})");
                count++;
            }

            return path;
        }

        /// <summary>
        /// Создаёт папку
        /// </summary>
        private void CreateFolder()
        {
            string path = null;

            try
            {
                path = GetUniquePath("Новая папка");

                if (Directory.Exists(SelectPath))
                {
                    Directory.CreateDirectory(path);

                    UpdateSelectItem();
                }
            }
            catch { FileDialog.Error(new ExplorerException("Не удалось создать папку!", path)); }
        }

        /// <summary>
        /// Создаёт файл
        /// </summary>
        private void CreateFile()
        {
            string path = null;

            try
            {
                if (Directory.Exists(SelectPath))
                {
                    path = DirectoryNode.AcceptFileTypes.Length == 0
                        ? GetUniquePath("Новый файл.txt")
                        : GetUniquePath($"Новый файл{DirectoryNode.AcceptFileTypes[0]}");

                    File.Create(path);

                    UpdateSelectItem();
                    SelectPath = path;
                }
            }
            catch { FileDialog.Error(new ExplorerException("Не удалось создать файл!", path)); }
        }

        /// <summary>
        /// При необходимости вызывает диалоговое окно
        /// </summary>
        public void PreviewAccept()
        {
            if (TypeExplorer == ExplorerType.Save && SelectItem is FileNode && File.Exists(SelectPath))
                IsDialogOpen = true;
            else
                OnAccept?.Invoke(SelectPath);
        }

        /// <summary>
        /// Проверяет можно ли нажать на кнопку "ОК" ("Подтвердить")
        /// </summary>
        /// <returns>true/false можно ли подтвердить</returns>
        private bool CanAccept()
        {
            if (string.IsNullOrEmpty(SelectPath))
                return false;

            if (SelectType == ExplorerSelectType.All)
                return true;

            if (SelectType == ExplorerSelectType.Folder && SelectItem is DirectoryNode)
                return true;
            else if (SelectItem is FileNode)
                return true;

            return false;
        }

        /// <summary>
        /// Изменяет настройки проводника
        /// </summary>
        public void Reset(ExplorerSelectType selectType, ExplorerType type)
        {
            if (SelectType != selectType)
                SelectType = selectType;
            if (TypeExplorer != type)
                TypeExplorer = type;

            for (int i = 0; i < Drives.Count; i++)
                (Drives[i] as DirectoryNode).Reset();
        }
    }
}
