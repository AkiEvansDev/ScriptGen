using System;
using System.Collections.ObjectModel;
using System.Linq;

using BaseMVVM.Abstraction;
using BaseMVVM.Command;

using ScriptGen.Common;
using ScriptGen.Common.SaveModel;
using ScriptGen.Interface;
using ScriptGen.Model;

namespace ScriptGen.Abstraction
{
    /// <summary>
    /// Базовый класс страницы приложения
    /// </summary>
    public abstract class PageBase : ViewModelBase, IPage
    {
        /// <summary>
        /// Вызывается при удалении страницы
        /// </summary>
        public event Action<IPage> OnRemovePage;
        /// <summary>
        /// Вызывается при выделении страницы
        /// </summary>
        public event Action<IPage> OnSelectPage;

        /// <summary>
        /// Модель базы данных
        /// </summary>
        private DataBaseM dataBase = new DataBaseM();

        /// <summary>
        /// true/false выбрана ли страница
        /// </summary>
        private bool isSelect;

        /// <summary>
        /// Выделенная таблица
        /// </summary>
        private ITable selectTable;
        /// <summary>
        /// Тип модели
        /// </summary>
        private ModelType modelType;

        /// <summary>
        /// Смещения по вертикали
        /// </summary>
        public double scrollVertical;
        // <summary>
        /// Смещения по горизонтали
        /// </summary>
        public double scrollHorizontal;

        /// <summary>
        /// Ширина
        /// </summary>
        private double actualWidth;
        /// <summary>
        /// Высота
        /// </summary>
        private double actualHeight;

        /// <summary>
        /// Создаёт базовый объект страницы приложения
        /// </summary>
        public PageBase()
        {
            RemovePage = new SimpleCommand(() => OnRemovePage?.Invoke(this));
            SelectPage = new SimpleCommand(() => OnSelectPage?.Invoke(this));
        }

        /// <summary>
        /// Команда удаления страницы
        /// </summary>
        public SimpleCommand RemovePage { get; }
        /// <summary>
        /// Команда выделения (выбора) страницы
        /// </summary>
        public SimpleCommand SelectPage { get; }

        /// <summary>
        /// Таблицы
        /// </summary>
        public ObservableCollection<ITable> Tables
            => dataBase.Tables;

        /// <summary>
        /// Линии
        /// </summary>
        public ObservableCollection<ILine> Lines
            => dataBase.Lines;

        /// <summary>
        /// Название
        /// </summary>
        public virtual string Name
        {
            get => dataBase.Name;
            set
            {
                dataBase.Name = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// true/false выделена ли страница
        /// </summary>
        public virtual bool IsSelect
        {
            get => isSelect;
            set
            {
                isSelect = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Выделенная таблица
        /// </summary>
        public virtual ITable SelectTable
        {
            get => selectTable;
            set
            {
                selectTable = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Тип модели
        /// </summary>
        public virtual ModelType TypeModel
        {
            get => modelType;
            set
            {
                modelType = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Смещения по вертикали
        /// </summary>
        public virtual double ScrollVertical
        {
            get => scrollVertical;
            set
            {
                scrollVertical = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Смещения по горизонтали
        /// </summary>
        public virtual double ScrollHorizontal
        {
            get => scrollHorizontal;
            set
            {
                scrollHorizontal = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Ширина
        /// </summary>
        public virtual double ActualWidth
        {
            get => actualWidth;
            set => actualWidth = value;
        }

        /// <summary>
        /// Высота
        /// </summary>
        public virtual double ActualHeight
        {
            get => actualHeight;
            set => actualHeight = value;
        }

        /// <summary>
        /// true/false существует ли ошибка у элемента
        /// </summary>
        public virtual bool IsError
        {
            get => dataBase.IsError;
            set
            {
                dataBase.IsError = value;

                OnPropertyChanged();
            }
        }

        // <summary>
        /// Сообщение об ошибке
        /// </summary>
        public virtual string Message { get; set; } = null;

        /// <summary>
        /// Устанавливает ошибку с переданным сообщением
        /// </summary>
        /// <param name="message">Сообщение</param>
        public virtual void SetError(string message = null)
        {
            dataBase.SetTableError();

            IsError = !Tables.All(t => !t.IsError && t.Fields.All(f => !f.IsError));
        }

        /// <summary>
        /// Добавляет таблицу
        /// </summary>
        /// <param name="table">Таблица</param>
        public virtual void AddTable(ITable table)
        {
            table.OnRemoveTable += RemoveTable;
            table.OnSelectTable += (t) => SelectTable = t;

            Tables.Add(table);
        }

        /// <summary>
        /// Удаляет таблицу
        /// </summary>
        /// <param name="table">Таблица</param>
        public virtual void RemoveTable(ITable table)
            => Tables.Remove(table);

        /// <summary>
        /// Загружает данные страницы
        /// </summary>
        /// <param name="dataBaseSave">Сохранённая информация</param>
        public virtual void Load(DataBaseSave dataBaseSave)
            => Name = dataBaseSave.Name;
    }
}
