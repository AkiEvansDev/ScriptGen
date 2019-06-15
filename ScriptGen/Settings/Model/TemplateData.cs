using ScriptGen.Common;

namespace ScriptGen.Settings.Model
{
    /// <summary>
    /// Модель для загрузки шаблонов
    /// </summary>
    public struct TemplateData
    {
        public string Name;
        public TemplateType Type;

        public string Template;
    }
}
