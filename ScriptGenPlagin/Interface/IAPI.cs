using ScriptGenPlugin.Model;

namespace ScriptGenPlugin.Interface
{
    /// <summary>
    /// API приложения
    /// </summary>
    public interface IAPI : IMessage
    {
        /// <summary>
        /// Существуют ли ошибки на выбранной вкладке приложения
        /// </summary>
        /// <returns>true/false есть ли ошибки</returns>
        bool IsError();
        /// <summary>
        /// Получает информацию о таблицах на выбранной вкладке приложения
        /// </summary>
        /// <returns>Информация о таблицах</returns>
        TableInfo[] GetTableInfo();

        /// <summary>
        /// Вызывает проводник для открытия файла
        /// </summary>
        /// <param name="canSelectFolder">true/false можно ли выделять папки</param>
        /// <param name="acceptFileTypes">Разрешённые расширения файлов</param>
        /// <returns>null если диалог отменён, иначе путь</returns>
        string OpenFile(bool canSelectFolder, params string[] acceptFileTypes);
        /// <summary>
        /// Вызывает проводник для сохранения файла
        /// </summary>
        /// <param name="canSelectFolder">true/false можно ли выделять папки</param>
        /// <param name="acceptFileTypes">Разрешённые расширения файлов</param>
        /// <returns>null если диалог отменён, иначе путь</returns>
        string SaveFile(bool canSelectFolder, params string[] acceptFileTypes);
    }
}
