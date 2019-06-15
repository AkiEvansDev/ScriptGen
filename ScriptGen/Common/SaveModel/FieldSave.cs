using ScriptGenPlugin.Model;

namespace ScriptGen.Common.SaveModel
{
    /// <summary>
    /// Модель сохранения поля таблицы
    /// </summary>
    public struct FieldSave
    {
        public FieldType Type;

        public string Name;
        public string LogicalName;
        public string ProgrammingName;

        public string DataType;
        public string ProgrammingType;

        public bool IsNull;
        public bool IsUnique;

        public int? RefTableId;
    }
}
