using System;
using System.Collections.ObjectModel;

using ScriptGen.Common.SaveModel;

namespace ScriptGen.Interface
{
    /// <summary>
    /// Таблица
    /// </summary>
    public interface ITable : IElement, IMove
    {
        /// <summary>
        /// Происходит при удалении таблицы
        /// </summary>
        event Action<ITable> OnRemoveTable;
        /// <summary>
        /// Происходит при выделении таблицы
        /// </summary>
        event Action<ITable> OnSelectTable;

        /// <summary>
        /// Id
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// true/false выделена ли таблица
        /// </summary>
        bool IsSelect { get; set; }

        /// <summary>
        /// Получает стандартное поле для добавления
        /// </summary>
        Func<IField> GetNewField { get; set; }

        /// <summary>
        /// Поля
        /// </summary>
        ObservableCollection<IField> Fields { get; }

        /// <summary>
        /// Добавляет поле
        /// </summary>
        /// <param name="field">Поле</param>
        void AddField(IField field);
        /// <summary>
        /// Удаляет поле
        /// </summary>
        /// <param name="field">Поле</param>
        void RemoveField(IField field);

        /// <summary>
        /// Загружает данные таблицы
        /// </summary>
        /// <param name="tableSave">Сохранённая информация</param>
        void Load(TableSave tableSave);
    }
}
