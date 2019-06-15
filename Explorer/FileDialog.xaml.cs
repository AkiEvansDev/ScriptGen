using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

using Explorer.Model;
using Explorer.ViewModel;

namespace Explorer
{
    /// <summary>
    /// Диалог проводника
    /// </summary>
    public partial class FileDialog
    {
        /// <summary>
        /// Вызывается при получении сообщения от проводника
        /// </summary>
        public static Action<string, string> OnInfo;
        /// <summary>
        /// Вызывается при получения сообщения об ошибке в проводнике
        /// </summary>
        public static Action<ExplorerException> OnError;

        /// <summary>
        /// Объект проводника
        /// </summary>
        private ExplorerVM explorer;
        /// <summary>
        /// true/false подтверждён ли диалог
        /// </summary>
        private bool isAccept;

        /// <summary>
        /// Создаёт новый файловый диалог
        /// </summary>
        public FileDialog()
        {
            InitializeComponent();

            explorer = new ExplorerVM
            {
                OnAccept = Accept,
                OnCancel = Cancel
            };

            DataContext = explorer;
        }

        /// <summary>
        /// true/false сохранять ли последний выбранный путь
        /// </summary>
        public bool SaveState { get; set; }
        /// <summary>
        /// Выбранный путь
        /// </summary>
        public string SelectPath { get; private set; }

        /// <summary>
        /// Вызывает ошибку проводника
        /// </summary>
        /// <param name="e">Информация об ошибке</param>
        internal static void Error(ExplorerException e)
            => OnError?.Invoke(e);

        /// <summary>
        /// Открывает путь в проводнике windows
        /// </summary>
        /// <param name="path">Путь</param>
        internal static void OpenExplorer(string path)
        {
            if (Directory.Exists(path) || File.Exists(path))
                Process.Start(new ProcessStartInfo("explorer.exe", " /select, " + path));
            else
                OnInfo?.Invoke("Путь не найден, возможно файл удалён!", path);
        }

        /// <summary>
        /// Открывает диалог
        /// </summary>
        /// <param name="selectType">Какие элементы проводника можно выделять</param>
        /// <param name="type">Тип проводника (сохранение/загрузка)</param>
        /// <param name="acceptFileTypes">Расширения файлов для отображения</param>
        /// <returns>true/false подтвердил ли пользователь диалог</returns>
        public bool Open(ExplorerSelectType selectType, ExplorerType type, params string[] acceptFileTypes)
        {
            DirectoryNode.AcceptFileTypes = acceptFileTypes;

            isAccept = false;
            explorer.Reset(selectType, type);

            ShowDialog();

            return isAccept;
        }

        /// <summary>
        /// Подтверждает диалог
        /// </summary>
        /// <param name="path">Выбранный путь</param>
        private void Accept(string path)
        {
            isAccept = true;

            SelectPath = explorer.SelectPath;

            DialogClose();
        }

        /// <summary>
        /// Отменяет диалог
        /// </summary>
        private void Cancel()
            => DialogClose();

        /// <summary>
        /// Событие закрытия проводника
        /// </summary>
        private void ExplorerClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;

            DialogClose();
        }

        /// <summary>
        /// Закрывает проводник
        /// </summary>
        private void DialogClose()
        {
            if (!SaveState)
                explorer.SetSelect("");

            Hide();
        }
    }
}
