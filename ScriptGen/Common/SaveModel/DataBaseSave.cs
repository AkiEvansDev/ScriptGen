namespace ScriptGen.Common.SaveModel
{
    /// <summary>
    /// Модель сохранения данных о базе данных
    /// </summary>
    public struct DataBaseSave
    {
        public string Name;

        public TableSave[] Tables;
        public LineSave[] Lines;
    }
}
