using BaseMVVM.Abstraction;
using BaseMVVM.Command;

using ScriptGen.Common;
using ScriptGen.Interface;
using ScriptGen.Model;

namespace ScriptGen.Abstraction
{
    /// <summary>
    /// Базовый класс графического элемента приложения
    /// </summary>
    public abstract class ElementBase : ViewModelBase, IElement
    {
        /// <summary>
        /// Модель элемента
        /// </summary>
        protected ElementM element = new ElementM();

        /// <summary>
        /// Тип модели (физическая/логическая/программирование)
        /// </summary>
        private ModelType modelType;
        /// <summary>
        /// Определяет элемент с фокусом
        /// </summary>
        private Focuses focusedElement;

        /// <summary>
        /// Создаёт объект базового элемента
        /// </summary>
        public ElementBase() 
            => LostFocus = new SimpleCommand(() => FocusedElement = Focuses.None);

        /// <summary>
        /// Команда нажатия "Enter" в поле ввода для названия
        /// </summary>
        public SimpleCommand NameEnter { get; set; }
        /// <summary>
        /// Команда нажатия "Up" в поле ввода для названия
        /// </summary>
        public SimpleCommand NameUp { get; set; }
        /// <summary>
        /// Команда нажатия "Down" в поле ввода для названия
        /// </summary>
        public SimpleCommand NameDown { get; set; }

        /// <summary>
        /// Команда потери элементом фокуса
        /// </summary>
        public SimpleCommand LostFocus { get; set; }
        /// <summary>
        /// Команда загрузки элемента
        /// </summary>
        public SimpleCommand Loaded { get; set; }

        /// <summary>
        /// Команда удаления элемента
        /// </summary>
        public SimpleCommand RemoveElement { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public virtual string Name
        {
            get => element.Name;
            set
            {
                element.Name = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Логическое название
        /// </summary>
        public virtual string LogicalName
        {
            get => element.LogicalName;
            set
            {
                element.LogicalName = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Название модели для языков программирования
        /// </summary>
        public virtual string ProgrammingName
        {
            get => element.ProgrammingName;
            set
            {
                element.ProgrammingName = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// true/false существует ли ошибка у элемента
        /// </summary>
        public virtual bool IsError
        {
            get => element.IsError;
            set
            {
                element.IsError = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public virtual string Message
        {
            get => element.Message;
            set
            {
                element.Message = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Тип модели (физическая/логическая/программирование)
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
        /// Определяет элемент с фокусом
        /// </summary>
        public virtual Focuses FocusedElement
        {
            get => focusedElement;
            set
            {
                focusedElement = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Устанавливает фокус элемента в зависимости от его свойств
        /// </summary>
        public virtual void SetFocus()
        {
            switch (TypeModel)
            {
                case ModelType.Physical:
                    FocusedElement = Focuses.Name;
                    break;
                case ModelType.Logical:
                    FocusedElement = Focuses.LogicalName;
                    break;
                case ModelType.Programming:
                    FocusedElement = Focuses.ProgrammingName;
                    break;
            }
        }

        /// <summary>
        /// Устанавливает ошибку с переданным сообщением
        /// </summary>
        /// <param name="message">Сообщение</param>
        public virtual void SetError(string message)
        {
            IsError = message != null;
            Message = message;
        }
    }
}
