using System;

using BaseMVVM.Command;

using ScriptGen.Common;
using ScriptGen.Common.SaveModel;
using ScriptGen.Interface;
using ScriptGen.Model;

using ScriptGenPlugin.Model;

namespace ScriptGen.Abstraction
{
    /// <summary>
    /// Базовый класс поля таблицы
    /// </summary>
    public abstract class FieldBase : ElementBase, IField
    {
        /// <summary>
        /// Событие удаления поля
        /// </summary>
        public event Action<IField> OnRemoveField;

        /// <summary>
        /// Модель поля
        /// </summary>
        private FieldM field;

        /// <summary>
        /// Создаёт объект базового поля
        /// </summary>
        public FieldBase()
        {
            field = new FieldM
            {
                Element = element,
                RefTable = null
            };

            NameEnter = new SimpleCommand(() =>
            {
                if (TypeModel == ModelType.Physical)
                    FocusedElement = Focuses.DataType;
                else if (TypeModel == ModelType.Programming)
                    FocusedElement = Focuses.ProgrammingType;
                else
                    NextField?.Invoke();
            });

            NameDown = DataTypeEnter = ProgrammingTypeEnter = new SimpleCommand(() => NextField?.Invoke());
            NameUp = new SimpleCommand(() => PrevField?.Invoke());

            RemoveElement = new SimpleCommand(() => OnRemoveField?.Invoke(this));
        }

        /// <summary>
        /// Вызывается при действиях, осуществляющих переключение на следующее поле
        /// </summary>
        public Action NextField { get; set; }
        /// <summary>
        /// Вызывается при действиях, осуществляющих переключение на предыдущее поле
        /// </summary>
        public Action PrevField { get; set; }

        /// <summary>
        /// Команда нажатия "Enter" в поле ввода для SQL типа данных
        /// </summary>
        public SimpleCommand DataTypeEnter { get; }
        /// <summary>
        /// Команда нажатия "Enter" в поле ввода для типа данных для языков программирования
        /// </summary>
        public SimpleCommand ProgrammingTypeEnter { get; }

        /// <summary>
        /// Тип поля
        /// </summary>
        public virtual FieldType Type
        {
            get => field.Type;
            set
            {
                field.Type = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// SQL тип данных поля
        /// </summary>
        public virtual string DataType
        {
            get => field.DataType;
            set
            {
                field.DataType = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Тип данных поля для языков программирования
        /// </summary>
        public virtual string ProgrammingType
        {
            get => field.ProgrammingType;
            set
            {
                field.ProgrammingType = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Список SQL типов данных
        /// </summary>
        public virtual string[] DataTypeSource
            => null;

        /// <summary>
        /// Список типов данных для языков программирования
        /// </summary>
        public virtual string[] ProgrammingTypeSource
           => null;

        /// <summary>
        /// true/false может ли поле быть равно null
        /// </summary>
        public virtual bool IsNull
        {
            get => field.IsNull;
            set
            {
                field.IsNull = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// true/false является ли поле уникальным
        /// </summary>
        public virtual bool IsUnique
        {
            get => field.IsUnique;
            set
            {
                field.IsUnique = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Информация о таблице на которую ссылается поле (только для FK)
        /// </summary>
        public virtual ITable RefTable
        {
            get => field.RefTable;
            set => field.RefTable = value;
        }

        /// <summary>
        /// Загружает данные поля
        /// </summary>
        /// <param name="fieldSave">Сохранённая информация</param>
        public virtual void Load(FieldSave fieldSave)
        {
            Name = fieldSave.Name;
            LogicalName = fieldSave.LogicalName;
            ProgrammingName = fieldSave.ProgrammingName;

            IsNull = fieldSave.IsNull;
            IsUnique = fieldSave.IsUnique;
        }

        /// <summary>
        /// Устанавливает ошибку с переданным сообщением
        /// </summary>
        /// <param name="message">Сообщение</param>
        public override void SetError(string message)
        {
            FocusedElement = Focuses.None;

            base.SetError(Verification.ConcatString(message, field.GetError()));
        }
    }
}
