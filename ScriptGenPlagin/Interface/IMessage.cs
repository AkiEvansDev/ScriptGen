using System;

namespace ScriptGenPlugin.Interface
{
    /// <summary>
    /// Часть API для сообщений
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Выводит сообщение об ошибке
        /// </summary>
        /// <param name="message">Сообщение</param>
        void Error(string message);
        /// <summary>
        /// Выводит сообщение об ошибке
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="option">Дополнительное сообщение</param>
        void Error(string message, string option);
        /// <summary>
        /// Выводит сообщение об ошибке
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="actionTitle">Заголовок кнопки действия</param>
        /// <param name="action">Действие кнопки</param>
        /// <param name="canAction">Функция, определяющая можно ли выполнить действие кнопки</param>
        void Error(string message, string actionTitle, Action action, Func<bool> canAction = null);
        /// <summary>
        /// Выводит сообщение об ошибке
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="option">Дополнительное сообщение</param>
        /// <param name="actionTitle">Заголовок кнопки действия</param>
        /// <param name="action">Действие кнопки</param>
        /// <param name="canAction">Функция, определяющая можно ли выполнить действие кнопки</param>
        void Error(string message, string option, string actionTitle, Action action, Func<bool> canAction = null);

        /// <summary>
        /// Выводит информационное сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        void Info(string message);
        /// <summary>
        /// Выводит информационное сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="option">Дополнительное сообщение</param>
        void Info(string message, string option);
        /// <summary>
        /// Выводит информационное сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="actionTitle">Заголовок кнопки действия</param>
        /// <param name="action">Действие кнопки</param>
        /// <param name="canAction">Функция, определяющая можно ли выполнить действие кнопки</param>
        void Info(string message, string actionTitle, Action action, Func<bool> canAction = null);
        /// <summary>
        /// Выводит информационное сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="option">Дополнительное сообщение</param>
        /// <param name="actionTitle">Заголовок кнопки действия</param>
        /// <param name="action">Действие кнопки</param>
        /// <param name="canAction">Функция, определяющая можно ли выполнить действие кнопки</param>
        void Info(string message, string option, string actionTitle, Action action, Func<bool> canAction = null);

        /// <summary>
        /// Выводит сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        void Message(string message);
        /// <summary>
        /// Выводит сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="option">Дополнительное сообщение</param>
        void Message(string message, string option);
        /// <summary>
        /// Выводит сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="actionTitle">Заголовок кнопки действия</param>
        /// <param name="action">Действие кнопки</param>
        /// <param name="canAction">Функция, определяющая можно ли выполнить действие кнопки</param>
        void Message(string message, string actionTitle, Action action, Func<bool> canAction = null);
        /// <summary>
        /// Выводит сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="option">Дополнительное сообщение</param>
        /// <param name="actionTitle">Заголовок кнопки действия</param>
        /// <param name="action">Действие кнопки</param>
        /// <param name="canAction">Функция, определяющая можно ли выполнить действие кнопки</param>
        void Message(string message, string option, string actionTitle, Action action, Func<bool> canAction = null);
    }
}
