using System.Windows;

namespace ScriptGen.Common.SaveModel
{
    /// <summary>
    /// Модель сохранения таблицы
    /// </summary>
    public struct TableSave
    {
        public int Id;

        public double Width;
        public Thickness Margin;

        public string Name;
        public string LogicalName;
        public string ProgrammingName;

        public FieldSave[] Fields;
    }
}
