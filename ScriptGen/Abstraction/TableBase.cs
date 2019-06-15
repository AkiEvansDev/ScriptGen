using System;
using System.Collections.ObjectModel;
using System.Windows;

using BaseMVVM.Command;

using ScriptGen.Common;
using ScriptGen.Common.SaveModel;
using ScriptGen.Interface;
using ScriptGen.Model;

namespace ScriptGen.Abstraction
{
    /// <summary>
    /// Базовый класс таблицы
    /// </summary>
    public abstract class TableBase : ElementBase, ITable
    {
        /// <summary>
        /// Происходит при удалении таблицы
        /// </summary>
        public event Action<ITable> OnRemoveTable;
        /// <summary>
        /// Происходит при выделении таблицы
        /// </summary>
        public event Action<ITable> OnSelectTable;

        /// <summary>
        /// Событие изменения ширины элемента
        /// </summary>
        public event Action<double, IMove> OnWidthChanged;
        /// <summary>
        /// Событие изменения высоты элемента
        /// </summary>
        public event Action<double, IMove> OnHeightChanged;
        /// <summary>
        /// Событие изменения положения элемента
        /// </summary>
        public event Action<Thickness, IMove> OnMarginChanged;

        /// <summary>
        /// Модель таблицы
        /// </summary>
        private TableM table;

        /// <summary>
        /// true/false выделена ли таблица
        /// </summary>
        private bool isSelect;

        /// <summary>
        /// Создаёт базовый объект таблицы
        /// </summary>
        public TableBase()
        {
            table = new TableM()
            {
                Element = element,
                Width = 350,
                Height = 50
            };

            RemoveElement = new SimpleCommand(() => OnRemoveTable?.Invoke(this));
            SelectElement = new SimpleCommand(() => OnSelectTable?.Invoke(this));
        }
        
        /// <summary>
        /// Команда выделения таблицы
        /// </summary>
        public SimpleCommand SelectElement { get; set; }

        /// <summary>
        /// true/false можно ли изменить ширину
        /// </summary>
        public Func<double, IMove, bool> CanWidthChange { get; set; }
        /// <summary>
        /// true/false можно ли изменить высоту
        /// </summary>
        public Func<double, IMove, bool> CanHeightChange { get; set; }

        /// <summary>
        /// Функция, определяющая можно ли изменить положение элемента по горизонтали
        /// </summary>
        public Func<double, IMove, bool> CanHorizontalChange { get; set; }
        /// <summary>
        /// Функция, определяющая можно ли изменить положение элемента по вертикали
        /// </summary>
        public Func<double, IMove, bool> CanVerticalChange { get; set; }

        /// <summary>
        /// Получает стандартное поле для добавления
        /// </summary>
        public Func<IField> GetNewField { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public virtual int Id
        {
            get => table.Id;
            set => table.Id = value;
        }

        /// <summary>
        /// true/false выделена ли таблица
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
        /// Поля таблицы
        /// </summary>
        public ObservableCollection<IField> Fields
            => table.Fields;

        /// <summary>
        /// Ширина таблицы
        /// </summary>
        public virtual double Width
        {
            get => table.Width;
            set
            {
                if (CanWidthChange?.Invoke(value, this) == true)
                {
                    table.Width = value;

                    OnWidthChanged?.Invoke(value, this);
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Высота таблицы
        /// </summary>
        public virtual double Height
        {
            get => table.Height;
            set
            {
                table.Height = value;

                OnHeightChanged?.Invoke(value, this);
            }
        }

        /// <summary>
        /// Положение таблицы
        /// </summary>
        public virtual Thickness Margin
        {
            get => table.Margin;
            set
            {
                if (CanHorizontalChange?.Invoke(value.Left, this) == false)
                    value.Left = Margin.Left;

                if (CanVerticalChange?.Invoke(value.Top, this) == false)
                    value.Top = Margin.Top;
                
                OnMarginChanged?.Invoke(value, this);

                table.Margin = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Устанавливает ошибку с переданным сообщением
        /// </summary>
        /// <param name="message">Сообщение</param>
        public override void SetError(string message)
        {
            FocusedElement = Focuses.None;

            table.SetFieldError();

            if (Fields.Count < 2)
                message = Verification.ConcatString(message, "Таблица должна содержать поля, кроме PK!");

            base.SetError(Verification.ConcatString(message, table.Element.GetError()));
        }

        /// <summary>
        /// Добавляет поле
        /// </summary>
        /// <param name="field">Поле</param>
        public virtual void AddField(IField field)
        {
            field.OnRemoveField += RemoveField;

            Fields.Add(field);
        }

        /// <summary>
        /// Удаляет поле
        /// </summary>
        /// <param name="field">Поле</param>
        public virtual void RemoveField(IField field)
            => Fields.Remove(field);

        /// <summary>
        /// Загружает данные таблицы
        /// </summary>
        /// <param name="tableSave">Сохранённая информация</param>
        public virtual void Load(TableSave tableSave)
        {
            Id = tableSave.Id;

            Name = tableSave.Name;
            LogicalName = tableSave.LogicalName;
            ProgrammingName = tableSave.ProgrammingName;

            Width = tableSave.Width;
            Margin = tableSave.Margin;
        }
    }
}
