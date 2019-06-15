namespace ScriptGenPlugin.Model
{
    /// <summary>
    /// Типы полей таблицы
    /// </summary>
    public enum FieldType
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        PK = 0,
        /// <summary>
        /// Внешний ключ
        /// </summary>
        FK = 1,
        /// <summary>
        /// Любое поле
        /// </summary>
        Any = 2
    }
}
