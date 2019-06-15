using System;

using BaseMVVM.Command;

using ScriptGen.ViewModel;

using ScriptGenPlugin.Interface;

namespace ScriptGen.API
{
    /// <summary>
    /// Часть API для сообщений
    /// </summary>
    public class MessageAPI : IMessage
    {
        /// <summary>
        /// Выводит сообщение об ошибке
        /// </summary>
        /// <param name="message">Сообщение</param>
        public void Error(string message)
            => StatusBarManagerVM.Error(message);

        /// <summary>
        /// Выводит сообщение об ошибке
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="option">Дополнительное сообщение</param>
        public void Error(string message, string option)
            => StatusBarManagerVM.Error(message, option);

        /// <summary>
        /// Выводит сообщение об ошибке
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="actionTitle">Заголовок кнопки действия</param>
        /// <param name="action">Действие кнопки</param>
        /// <param name="canAction">Функция, определяющая можно ли выполнить действие кнопки</param>
        public void Error(string message, string actionTitle, Action action, Func<bool> canAction = null)
            => StatusBarManagerVM.Error(message, actionTitle, new SimpleCommand(action, canAction));

        /// <summary>
        /// Выводит сообщение об ошибке
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="option">Дополнительное сообщение</param>
        /// <param name="actionTitle">Заголовок кнопки действия</param>
        /// <param name="action">Действие кнопки</param>
        /// <param name="canAction">Функция, определяющая можно ли выполнить действие кнопки</param>
        public void Error(string message, string option, string actionTitle, Action action, Func<bool> canAction = null)
            => StatusBarManagerVM.Error(message, option, actionTitle, new SimpleCommand(action, canAction));

        /// <summary>
        /// Выводит информационное сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        public void Info(string message)
            => StatusBarManagerVM.Info(message);

        /// <summary>
        /// Выводит информационное сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="option">Дополнительное сообщение</param>
        public void Info(string message, string option)
            => StatusBarManagerVM.Info(message, option);

        /// <summary>
        /// Выводит информационное сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="actionTitle">Заголовок кнопки действия</param>
        /// <param name="action">Действие кнопки</param>
        /// <param name="canAction">Функция, определяющая можно ли выполнить действие кнопки</param>
        public void Info(string message, string actionTitle, Action action, Func<bool> canAction = null)
            => StatusBarManagerVM.Info(message, actionTitle, new SimpleCommand(action, canAction));

        /// <summary>
        /// Выводит информационное сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="option">Дополнительное сообщение</param>
        /// <param name="actionTitle">Заголовок кнопки действия</param>
        /// <param name="action">Действие кнопки</param>
        /// <param name="canAction">Функция, определяющая можно ли выполнить действие кнопки</param>
        public void Info(string message, string option, string actionTitle, Action action, Func<bool> canAction = null)
            => StatusBarManagerVM.Info(message, option, actionTitle, new SimpleCommand(action, canAction));

        /// <summary>
        /// Выводит сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        public void Message(string message)
            => StatusBarManagerVM.Message(message);

        /// <summary>
        /// Выводит сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="option">Дополнительное сообщение</param>
        public void Message(string message, string option)
            => StatusBarManagerVM.Message(message, option);

        /// <summary>
        /// Выводит сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="actionTitle">Заголовок кнопки действия</param>
        /// <param name="action">Действие кнопки</param>
        /// <param name="canAction">Функция, определяющая можно ли выполнить действие кнопки</param>
        public void Message(string message, string actionTitle, Action action, Func<bool> canAction)
            => StatusBarManagerVM.Message(message, actionTitle, new SimpleCommand(action, canAction));

        /// <summary>
        /// Выводит сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="option">Дополнительное сообщение</param>
        /// <param name="actionTitle">Заголовок кнопки действия</param>
        /// <param name="action">Действие кнопки</param>
        /// <param name="canAction">Функция, определяющая можно ли выполнить действие кнопки</param>
        public void Message(string message, string option, string actionTitle, Action action, Func<bool> canAction)
            => StatusBarManagerVM.Message(message, option, actionTitle, new SimpleCommand(action, canAction));
    }
}
