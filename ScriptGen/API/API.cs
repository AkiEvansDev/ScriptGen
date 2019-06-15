using System;

using Explorer;

using ScriptGenPlugin.Interface;
using ScriptGenPlugin.Model;

namespace ScriptGen.API
{
    /// <summary>
    /// API приложения
    /// </summary>
    public class API : MessageAPI, IAPI
    {
        /// <summary>
        /// Получает ошибку текущей страницы
        /// </summary>
        public Func<bool> GetError;
        /// <summary>
        /// Получает информацию о текущей странице
        /// </summary>
        public Func<TableInfo[]> GetTables;

        /// <summary>
        /// Проводник
        /// </summary>
        private FileDialog explorer = new FileDialog();
        
        /// <summary>
        /// Существуют ли ошибки на выбранной вкладке приложения
        /// </summary>
        /// <returns>true/false есть ли ошибки</returns>
        public bool IsError()
            => (bool)GetError?.Invoke();

        /// <summary>
        /// Получает информацию о таблицах на выбранной вкладке приложения
        /// </summary>
        /// <returns>Информация о таблицах</returns>
        public TableInfo[] GetTableInfo()
            => GetTables?.Invoke();

        /// <summary>
        /// Вызывает проводник для открытия файла
        /// </summary>
        /// <param name="canSelectFolder">true/false можно ли выделять папки</param>
        /// <param name="acceptFileTypes">Разрешённые расширения файлов</param>
        /// <returns>null если диалог отменён, иначе путь</returns>
        public string OpenFile(bool canSelectFolder, params string[] acceptFileTypes)
        {
            ExplorerSelectType type = ExplorerSelectType.File;
            if (canSelectFolder)
                type = ExplorerSelectType.All;

            return explorer.Open(type, ExplorerType.Open, acceptFileTypes) ? explorer.SelectPath : null;
        }

        /// <summary>
        /// Вызывает проводник для сохранения файла
        /// </summary>
        /// <param name="canSelectFolder">true/false можно ли выделять папки</param>
        /// <param name="acceptFileTypes">Разрешённые расширения файлов</param>
        /// <returns>null если диалог отменён, иначе путь</returns>
        public string SaveFile(bool canSelectFolder, params string[] acceptFileTypes)
        {
            ExplorerSelectType type = ExplorerSelectType.File;
            if (canSelectFolder)
                type = ExplorerSelectType.All;

            return explorer.Open(type, ExplorerType.Save, acceptFileTypes) ? explorer.SelectPath : null;
        }
    }
}
