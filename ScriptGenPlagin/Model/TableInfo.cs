namespace ScriptGenPlugin.Model
{
    /// <summary>
    /// Информация о таблице
    /// </summary>
    public class TableInfo
    {
        /// <summary>
        /// Название таблицы
        /// </summary>
        public string Name;
        /// <summary>
        /// Логическое название таблицы
        /// </summary>
        public string LogicalName;
        /// <summary>
        /// Название таблицы для языков программирования
        /// </summary>
        public string ProgrammingName;

        /// <summary>
        /// Таблицы, имеющие связь с данной таблицей
        /// </summary>
        public TableInfo[] RefTables;
        /// <summary>
        /// Поля таблицы
        /// </summary>
        public FieldInfo[] Fields;
    }
}
