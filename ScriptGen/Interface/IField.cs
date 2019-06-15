using System;

using ScriptGen.Common.SaveModel;

using ScriptGenPlugin.Model;

namespace ScriptGen.Interface
{
    /// <summary>
    /// Поле таблицы
    /// </summary>
    public interface IField : IElement
    {
        /// <summary>
        /// Событие удаления поля
        /// </summary>
        event Action<IField> OnRemoveField;

        /// <summary>
        /// Вызывается при действиях, осуществляющих переключение на следующее поле
        /// </summary>
        Action NextField { get; set; }
        /// <summary>
        /// Вызывается при действиях, осуществляющих переключение на предыдущее поле
        /// </summary>
        Action PrevField { get; set; }

        /// <summary>
        /// Тип поля
        /// </summary>
        FieldType Type { get; set; }

        /// <summary>
        /// SQL тип данных поля
        /// </summary>
        string DataType { get; set; }
        /// <summary>
        /// Тип данных поля для языков программирования
        /// </summary>
        string ProgrammingType { get; set; }

        /// <summary>
        /// true/false может ли поле быть равно null
        /// </summary>
        bool IsNull { get; set; }
        /// <summary>
        /// true/false является ли поле уникальным
        /// </summary>
        bool IsUnique { get; set; }

        /// <summary>
        /// Информация о таблице на которую ссылается поле (только для FK)
        /// </summary>
        ITable RefTable { get; set; }

        /// <summary>
        /// Загружает данные поля
        /// </summary>
        /// <param name="fieldSave">Сохранённая информация</param>
        void Load(FieldSave fieldSave);
    }
}
