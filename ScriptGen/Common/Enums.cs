namespace ScriptGen.Common
{
    /// <summary>
    /// Тип модели
    /// </summary>
    public enum ModelType
    {
        /// <summary>
        /// Физическая модель
        /// </summary>
        Physical = 0,
        /// <summary>
        /// Логическая модель
        /// </summary>
        Logical = 1,
        /// <summary>
        /// Модель программирования
        /// </summary>
        Programming = 2
    }

    /// <summary>
    /// Элементы для фокуса
    /// </summary>
    public enum Focuses
    {
        /// <summary>
        /// Отсутствует
        /// </summary>
        None = 0,
        /// <summary>
        /// Поле названия
        /// </summary>
        Name = 1,
        /// <summary>
        /// Поле логического названия
        /// </summary>
        LogicalName = 2,
        /// <summary>
        /// Поле названия для языков программирования
        /// </summary>
        ProgrammingName = 3,
        /// <summary>
        /// Поле типа данных
        /// </summary>
        DataType = 4,
        /// <summary>
        /// Поле типа данных для языков программирования
        /// </summary>
        ProgrammingType = 5
    }

    /// <summary>
    /// Тип шаблона
    /// </summary>
    public enum TemplateType
    {
        /// <summary>
        /// Шаблон SQL
        /// </summary>
        SQL = 1,
        /// <summary>
        /// Шаблон для языков программирования
        /// </summary>
        Programming = 2
    }
}
