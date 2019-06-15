using System.ComponentModel;

using BaseMVVM.Command;

using ScriptGen.Common;

namespace ScriptGen.Interface
{
    /// <summary>
    /// Графический элемент приложения
    /// </summary>
    public interface IElement : IError, INotifyPropertyChanged
    {
        /// <summary>
        /// Команда нажатия "Enter" в поле ввода для названия
        /// </summary>
        SimpleCommand NameEnter { get; set; }
        /// <summary>
        /// Команда нажатия "Up" в поле ввода для названия
        /// </summary>
        SimpleCommand NameUp { get; set; }
        /// <summary>
        /// Команда нажатия "Down" в поле ввода для названия
        /// </summary>
        SimpleCommand NameDown { get; set; }

        /// <summary>
        /// Команда потери элементом фокуса
        /// </summary>
        SimpleCommand LostFocus { get; set; }
        /// <summary>
        /// Команда загрузки элемента
        /// </summary>
        SimpleCommand Loaded { get; set; }

        /// <summary>
        /// Команда удаления элемента
        /// </summary>
        SimpleCommand RemoveElement { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Логическое название
        /// </summary>
        string LogicalName { get; set; }
        /// <summary>
        /// Название для модели для языков программирования
        /// </summary>
        string ProgrammingName { get; set; }

        /// <summary>
        /// Тип модели (физическая/логическая/программирование)
        /// </summary>
        ModelType TypeModel { get; set; }
        /// <summary>
        /// Определяет элемент с фокусом
        /// </summary>
        Focuses FocusedElement { get; set; }

        /// <summary>
        /// Устанавливает фокус элемента в зависимости от его свойств
        /// </summary>
        void SetFocus();
    }
}
