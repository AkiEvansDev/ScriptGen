using System;

namespace Explorer
{
    /// <summary>
    /// Класс, содержащий информацию об ошибке
    /// </summary>
    public class ExplorerException
    {
        /// <summary>
        /// Создаёт новый объект с информацией об ошибке
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="option">Дополнительное сообщение</param>
        public ExplorerException(string message, string option)
        {
            Message = message;
            Option = option;
        }

        /// <summary>
        /// Создаёт новый объект с информацией об ошибке
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="option">Дополнительное сообщение</param>
        /// <param name="actionTitle">Заголовок кнопки действия</param>
        /// <param name="action">Действие кнопки</param>
        public ExplorerException(string message, string option, string actionTitle, Action action) :
            this(message, option)
        {
            ActionTitle = actionTitle;
            Action = action;
        }

        /// <summary>
        /// Действие кнопки
        /// </summary>
        public Action Action { get; private set; }
        /// <summary>
        /// Заголовок кнопки действия
        /// </summary>
        public string ActionTitle { get; private set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; private set; }
        /// <summary>
        /// Дополнительное сообщение
        /// </summary>
        public string Option { get; private set; }
    }
}
