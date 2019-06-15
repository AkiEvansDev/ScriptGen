using System;
using System.Collections.Generic;
using System.Linq;

using ScriptGen.Settings.Model;

namespace ScriptGen.Common
{
    /// <summary>
    /// Содержит общую информацию приложения
    /// </summary>
    public static class Info
    {
        /// <summary>
        /// Происходит при изменении настроек
        /// </summary>
        public static event Action OnSettingsChange;
        /// <summary>
        /// Происходит при добавлении настроек
        /// </summary>
        public static event Action OnSettingsAdd;

        /// <summary>
        /// Экземпляры настроек приложения
        /// </summary>
        private static List<Option> options = new List<Option>();
        /// <summary>
        /// Шаблоны для SQL
        /// </summary>
        private static List<TemplateData> sqlTemplates = new List<TemplateData>();
        /// <summary>
        /// Шаблоны для языков программирования
        /// </summary>
        private static List<TemplateData> programmingTemplates = new List<TemplateData>();

        /// <summary>
        /// Индекс активных настроек
        /// </summary>
        private static int activeOptionIndex = -1;
        /// <summary>
        /// Индекс активного SQL шаблона
        /// </summary>
        private static int activeTemplateSQLIndex = -1;
        /// <summary>
        /// Индекс активного шаблона для языков программирования
        /// </summary>
        private static int activeTemplateProgrammingIndex = -1;
        
        /// <summary>
        /// Индекс активных настроек
        /// </summary>
        public static int ActiveOptionIndex
        {
            get => activeOptionIndex;
            set
            {
                activeOptionIndex = (value >= 0 && value < options.Count) ? value : -1;

                OnSettingsChange?.Invoke();
            }
        }

        /// <summary>
        /// Индекс активного SQL шаблона
        /// </summary>
        public static int ActiveTemplateSQLIndex
        {
            get => activeTemplateSQLIndex;
            set
            {
                activeTemplateSQLIndex = (value >= 0 && value < options.Count) ? value : -1;

                OnSettingsChange?.Invoke();
            }
        }

        /// <summary>
        /// Индекс активного шаблона для языков программирования
        /// </summary>
        public static int ActiveTemplateProgrammingIndex
        {
            get => activeTemplateProgrammingIndex;
            set
            {
                activeTemplateProgrammingIndex = (value >= 0 && value < options.Count) ? value : -1;

                OnSettingsChange?.Invoke();
            }
        }

        /// <summary>
        /// Активные настройки
        /// </summary>
        private static Option option
            => ActiveOptionIndex != -1 ? options[ActiveOptionIndex] : null;

        /// <summary>
        /// Активный SQL шаблон
        /// </summary>
        public static string TemplateSQL
            => ActiveTemplateSQLIndex != -1 ? sqlTemplates[ActiveTemplateSQLIndex].Template : "";

        /// <summary>
        /// Активный шаблон для языков программирования
        /// </summary>
        public static string TemplateProgramming
            => ActiveTemplateProgrammingIndex != -1 ? programmingTemplates[ActiveTemplateProgrammingIndex].Template : "";
        
        /// <summary>
        /// Названия всех настроек
        /// </summary>
        public static string[] OptionNames
            => options.Select(x => x.Name).ToArray();

        /// <summary>
        /// Названия всех шаблонов SQL
        /// </summary>
        public static string[] SqlNames
            => sqlTemplates.Select(x => x.Name).ToArray();

        /// <summary>
        /// Названия всех шаблонов для языков программирования
        /// </summary>
        public static string[] ProgrammingNames
            => programmingTemplates.Select(x => x.Name).ToArray();

        /// <summary>
        /// Список недопустимых слов
        /// </summary>
        public static string[] FalseWords
            => option != null ? option.FalseWords : new string[0];

        /// <summary>
        /// Список SQL типов данных
        /// </summary>
        public static string[] SQLTypes
            => option != null ? option.SQLTypes : new string[0];

        /// <summary>
        /// Список типов данных для языков программирования
        /// </summary>
        public static string[] ProgrammingTypes
            => option != null ? option.ProgrammingTypes : new string[0];

        /// <summary>
        /// Добавляет экземпляр настроек
        /// </summary>
        /// <param name="optionData">Экземпляр настроек</param>
        public static void AddOption(OptionData optionData)
        {
            options.Add(new Option(optionData));

            OnSettingsAdd?.Invoke();
        }

        /// <summary>
        /// Добавляет шаблон
        /// </summary>
        /// <param name="templateData">Данные о шаблоне</param>
        public static void AddTemplate(TemplateData templateData)
        {
            if (templateData.Type == TemplateType.SQL)
                sqlTemplates.Add(templateData);
            else if (templateData.Type == TemplateType.Programming)
                programmingTemplates.Add(templateData);

            OnSettingsAdd?.Invoke();
        }
    }
}
