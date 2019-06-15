namespace Explorer
{
    /// <summary>
    /// Тип проводника
    /// </summary>
    public enum ExplorerType
    {
        /// <summary>
        /// Открытие файлов
        /// </summary>
        Open = 0,
        /// <summary>
        /// Сохранение файлов
        /// </summary>
        Save = 1
    }

    /// <summary>
    /// Типы выделения элементов проводника
    /// </summary>
    public enum ExplorerSelectType
    {
        /// <summary>
        /// Все элементы
        /// </summary>
        All = 0,
        /// <summary>
        /// Только папки
        /// </summary>
        Folder = 1,
        /// <summary>
        /// Только файлы
        /// </summary>
        File = 2
    }
}
