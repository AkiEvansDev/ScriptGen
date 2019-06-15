using System;

namespace ScriptGenPlugin.Interface
{
    /// <summary>
    /// Плагина для приложения ScriptGen
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Возвращает содержимое кнопки доступа к плагину
        /// </summary>
        object ActionContent { get; }
        /// <summary>
        /// Возвращает текст подсказки для плагина
        /// </summary>
        string ActionToolTip { get; }

        /// <summary>
        /// Вызывается при нажатии на кнопку плагина в приложении
        /// </summary>
        Action PluginAction { get; }
        /// <summary>
        /// Функция, определяющая возможность вызова PluginAction
        /// </summary>
        Func<bool> CanPluginAction { get; }

        /// <summary>
        /// Вызывается при загрузки плагина
        /// </summary>
        /// <param name="api">API</param>
        void Start(IAPI api);
        /// <summary>
        /// Вызывается при закрытии программы, использующей плагин
        /// </summary>
        void Close();
    }
}
