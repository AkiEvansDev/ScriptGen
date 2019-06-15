using System.Collections.Generic;
using System.Text;
using System.Linq;

using ScriptGen.Generator.Keywords;

using ScriptGenPlugin.Model;
using ScriptGen.Common;

namespace ScriptGen.Generator
{
    /// <summary>
    /// Осуществляет генерацию по шаблону
    /// </summary>
    public static class ScriptGenerator
    {
        private static StringBuilder sqlResult;
        private static StringBuilder programmingResult;

        private static List<string> wasTableNames;

        public static void Generate(TableInfo[] tables, string savePath)
        {
            Reset();
            TemplateFiller.Tables = tables;
            
            for (int i = 0; i < tables.Length; i++)
                NextTable(tables[i]);

            Save(savePath);
        }

        private static void NextTable(TableInfo table)
        {
            if (wasTableNames.Contains(table.Name) || table == null)
                return;

            foreach (FieldInfo field in table.Fields.Where(f => f.Type == FieldType.FK))
                NextTable(field.RefTable);

            sqlResult.Append(TemplateFiller.Fill(table, Info.TemplateSQL));
            programmingResult.Append(TemplateFiller.Fill(table, Info.TemplateProgramming));

            wasTableNames.Add(table.Name);
        }

        private static void Reset()
        {
            Once.Reset();

            wasTableNames = new List<string>();
            sqlResult = new StringBuilder();
            programmingResult = new StringBuilder();
        }

        private static void Save(string path)
        {
            FileWork.TextSave(sqlResult, path, "script", ".sql");
            FileWork.TextSave(programmingResult, path, "script", ".txt");
        }
    }
}
