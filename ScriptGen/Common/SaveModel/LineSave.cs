namespace ScriptGen.Common.SaveModel
{
    /// <summary>
    /// Модель сохранения линии связи
    /// </summary>
    public struct LineSave
    {
        public FieldSave Field;

        public int SourceId;
        public int TargetId;

        public double SourceX1;
        public double TargetX1;
        public double SourceY1;
        public double TargetY1;

        public double ConnectionX1;
    }
}
