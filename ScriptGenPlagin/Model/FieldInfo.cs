namespace ScriptGenPlugin.Model
{
    /// <summary>
    /// Информация о поле в таблице
    /// </summary>
    public class FieldInfo
    {
        /// <summary>
        /// Тип поля (PK - первичный ключ, FK - внешний ключ, ANY - любое)
        /// </summary>
        public FieldType Type;

        /// <summary>
        /// Название поля
        /// </summary>
        public string Name;
        /// <summary>
        /// Логическое название поля
        /// </summary>
        public string LogicalName;
        /// <summary>
        /// Название поля для языков программирования
        /// </summary>
        public string ProgrammingName;

        /// <summary>
        /// SQL тип данных
        /// </summary>
        public string DataType;
        /// <summary>
        /// Тип данных для языков программирования
        /// </summary>
        public string ProgrammingType;

        /// <summary>
        /// Может ли поле равняться null
        /// </summary>
        public bool IsNull;
        /// <summary>
        /// Является ли поле уникальным
        /// </summary>
        public bool IsUnique;

        /// <summary>
        /// Информация о таблице на которую ссылается поле (только для FK)
        /// </summary>
        public TableInfo RefTable;
    }
}
