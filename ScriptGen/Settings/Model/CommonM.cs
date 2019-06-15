using System;

namespace ScriptGen.Settings.Model
{
    /// <summary>
    /// Модель настроек приложения
    /// </summary>
    [Serializable]
    public struct CommonM
    {
        public int ActiveOptionIndex;
        public int ActiveTemplateSQLIndex;
        public int ActiveTemplateProgrammingIndex;
    }
}
