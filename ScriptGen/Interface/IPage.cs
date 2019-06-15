using System;
using System.Collections.ObjectModel;

using ScriptGen.Common;
using ScriptGen.Common.SaveModel;

namespace ScriptGen.Interface
{
    /// <summary>
    /// Страница приложения
    /// </summary>
    public interface IPage : IError
    {
        /// <summary>
        /// Вызывается при удалении страницы
        /// </summary>
        event Action<IPage> OnRemovePage;
        /// <summary>
        /// Вызывается при выделении страницы
        /// </summary>
        event Action<IPage> OnSelectPage;

        /// <summary>
        /// Название
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// true/false выбрана ли страница
        /// </summary>
        bool IsSelect { get; set; }

        /// <summary>
        /// Выделенная таблица
        /// </summary>
        ITable SelectTable { get; set; }
        /// <summary>
        /// Тип модели
        /// </summary>
        ModelType TypeModel { get; set; }

        /// <summary>
        /// Смещения по вертикали
        /// </summary>
        double ScrollVertical { get; set; }
        /// <summary>
        /// Смещения по горизонтали
        /// </summary>
        double ScrollHorizontal { get; set; }

        /// <summary>
        /// Ширина
        /// </summary>
        double ActualWidth { get; set; }
        /// <summary>
        /// Высота
        /// </summary>
        double ActualHeight { get; set; }

        /// <summary>
        /// Таблицы
        /// </summary>
        ObservableCollection<ITable> Tables { get; }
        /// <summary>
        /// Линии
        /// </summary>
        ObservableCollection<ILine> Lines { get; }

        /// <summary>
        /// Добавляет таблицу
        /// </summary>
        /// <param name="table">Таблица</param>
        void AddTable(ITable table);
        /// <summary>
        /// Удаляет таблицу
        /// </summary>
        /// <param name="table">Таблица</param>
        void RemoveTable(ITable table);

        /// <summary>
        /// Загружает данные страницы
        /// </summary>
        /// <param name="dataBaseSave">Сохранённая информация</param>
        void Load(DataBaseSave dataBaseSave);
    }
}
