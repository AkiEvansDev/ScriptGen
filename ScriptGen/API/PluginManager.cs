using System;
using System.Collections.ObjectModel;

using BaseMVVM.Command;

using ScriptGen.Common;

namespace ScriptGen.API
{
    /// <summary>
    /// Позволяет осуществлять работу с плагинами
    /// </summary>
    public static class PluginManager
    {
        /// <summary>
        /// Происходит при изменении списка плагинов
        /// </summary>
        public static Action OnPluginsUpdate; 

        /// <summary>
        /// Хранит экземпляр API приложения
        /// </summary>
        public static API CommonAPI = new API();

        /// <summary>
        /// Список плагинов
        /// </summary>
        public static ObservableCollection<PluginData> Plugins { get; } 
            = new ObservableCollection<PluginData>();

        /// <summary>
        /// Загружает плагин
        /// </summary>
        /// <param name="path">Путь к файлу конфигурации</param>
        public static void LoadPlugin(string path)
        {
            if (PluginFileWork.LoadPlugin(path, out PluginData plugin) && plugin.IsValid())
                if (PluginFileWork.SaveLocalPlugin(plugin, out plugin))
                {
                    plugin.RemovePlaugin = new SimpleCommand(() => RemovePlugin(plugin));
                    Plugins.Add(plugin);
                }

            OnPluginsUpdate?.Invoke();
        }

        /// <summary>
        /// Загружает все установленные плагины
        /// </summary>
        public static void LoadPlugins()
        {
            if (PluginFileWork.GetAllPluginsPath(out string[] paths))
                for (int i = 0; i < paths.Length; i++)
                    if (PluginFileWork.LoadPlugin(paths[i], out PluginData plugin) && plugin.IsValid())
                    {
                        plugin.RemovePlaugin = new SimpleCommand(() => RemovePlugin(plugin));
                        Plugins.Add(plugin);
                    }

            OnPluginsUpdate?.Invoke();
        }

        /// <summary>
        /// Удаляет плагин
        /// </summary>
        /// <param name="plugin">Данные плагина</param>
        private static void RemovePlugin(PluginData plugin)
        {
            if (PluginFileWork.DisablePlugin(plugin.DllPath))
            {
                plugin.Plugin.Close();

                Plugins.Remove(plugin);
                OnPluginsUpdate?.Invoke();
            }
        }

        /// <summary>
        /// Останавливает работу всех плагинов
        /// </summary>
        public static void ClosePlugins()
        {
            for (int i = 0; i < Plugins.Count; i++)
                Plugins[i].Plugin.Close();
        }
    }
}
