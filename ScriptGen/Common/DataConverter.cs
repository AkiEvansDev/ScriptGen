using System.Linq;

using ScriptGen.Common.SaveModel;
using ScriptGen.Interface;

using ScriptGenPlugin.Model;

namespace ScriptGen.Common
{
    /// <summary>
    /// Осуществляет преобразование данных
    /// </summary>
    public static class DataConverter
    {
        private static FieldSave IFieldToSave(IField field)
            => new FieldSave()
            {
                Type = field.Type,

                Name = field.Name,
                LogicalName = field.LogicalName,
                ProgrammingName = field.ProgrammingName,

                DataType = field.DataType,
                ProgrammingType = field.ProgrammingType,

                IsNull = field.IsNull,
                IsUnique = field.IsUnique,

                RefTableId = field.RefTable != null ? (int?)field.RefTable.Id : null
            };

        private static TableSave ITableToSave(ITable table)
            => new TableSave()
            {
                Id = table.Id,

                Width = table.Width,
                Margin = table.Margin,
                
                Name = table.Name,
                LogicalName = table.LogicalName,
                ProgrammingName = table.ProgrammingName,

                Fields = table.Fields
                    .Where(f => f.Type != FieldType.FK)
                    .Select(f => IFieldToSave(f)).ToArray()
            };

        private static LineSave ILineToSave(ILine line)
            => new LineSave()
            {
                Field = IFieldToSave(line.Field),

                SourceId = line.Source.Id,
                TargetId = line.Target.Id,

                SourceX1 = line.SourceX,
                TargetX1 = line.TargetX,
                SourceY1 = line.SourceY,
                TargetY1 = line.TargetY,

                ConnectionX1 = line.ConnectionX
            };
            
        public static DataBaseSave IPageToSave(IPage page)
        {
            for (int i = 0; i < page.Tables.Count; i++)
                page.Tables[i].Id = i + 1;

            return new DataBaseSave()
            {
                Name = page.Name,

                Tables = page.Tables.Select(t => ITableToSave(t)).ToArray(),
                Lines = page.Lines.Select(l => ILineToSave(l)).ToArray()
            };
        }

        private static TableInfo[] Tables;

        private static FieldInfo IFieldToInfo(IField field)
            => new FieldInfo()
            {
                Type = field.Type,

                Name = field.Name,
                LogicalName = field.LogicalName,
                ProgrammingName = field.ProgrammingName,

                DataType = field.Type == FieldType.FK 
                    ? field.RefTable.Fields[0].DataType
                    : field.DataType,
                ProgrammingType = field.Type == FieldType.FK
                    ? field.RefTable.Fields[0].ProgrammingType
                    : field.ProgrammingType,

                IsNull = field.IsNull,
                IsUnique = field.IsUnique,

                RefTable = field.RefTable != null
                    ? Tables[field.RefTable.Id]
                    : null
            };

        private static TableInfo ITableToInfo(ITable table)
            => new TableInfo()
            {
                Name = table.Name,
                LogicalName = table.LogicalName,
                ProgrammingName = table.ProgrammingName
            };

        public static TableInfo[] IPageToInfo(IPage page)
        {
            Tables = new TableInfo[page.Tables.Count];
            for (int i = 0; i < page.Tables.Count; i++)
            {
                page.Tables[i].Id = i;
                Tables[i] = ITableToInfo(page.Tables[i]);
            }

            for (int i = 0; i < Tables.Length; i++)
                Tables[i].Fields = page.Tables[i].Fields.Select(f => IFieldToInfo(f)).ToArray();

            for (int i = 0; i < Tables.Length; i++)
                Tables[i].RefTables = Tables.Where(t => t.Fields.Count(f => f.RefTable == Tables[i]) > 0).ToArray();

            return Tables;
        }
    }
}
