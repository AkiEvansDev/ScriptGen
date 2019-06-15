using System.Collections.Generic;
using System.Linq;

using ScriptGen.Interface;

using ScriptGenPlugin.Model;

namespace ScriptGen.Generator.Keywords
{
    /// <summary>
    /// Ключевое слово рекурсии
    /// </summary>
    public class Rec : IKeyword
    {
        private static string recBlock;
        
        private static List<TableInfo> wasTable;
        private static List<string> wasTableNames;

        public void Do(string code)
        {
            string parameter = FillHelper.GetParameter(code);
            code = code.Substring(parameter.Length + 5);

            if (parameter == "start")
                RecStart(code);
            else
                RecInvoke(GetRefTable(parameter));
        }

        private void RecStart(string code)
        {
            wasTable = new List<TableInfo>();
            wasTableNames = new List<string>();

            recBlock = FillHelper.GetBlock(code);
            code = code.Substring(recBlock.Length + 2);

            TemplateFiller.KeywordInvoke(FillHelper.GetWord(recBlock), recBlock);
            TemplateFiller.KeywordInvoke(FillHelper.GetWord(code), code);
        }

        private void RecInvoke(TableInfo table)
        {
            if (string.IsNullOrEmpty(recBlock) || table == null || wasTableNames.Contains(table.Name))
                return;
            
            wasTable.Add(TemplateFiller.Table);
            wasTableNames.Add(TemplateFiller.Table.Name);
            TemplateFiller.Table = table;

            TemplateFiller.KeywordInvoke(FillHelper.GetWord(recBlock), recBlock);

            TemplateFiller.Table = wasTable.Last();
            wasTable.RemoveAt(wasTable.Count - 1);
        }

        private TableInfo GetRefTable(string command)
        {
            int param = FillHelper.GetConstantParameter(command, out command);
            return TemplateFiller.Table.Fields[param].RefTable;
        }

        public static void Reset()
        {
            recBlock = "";
            wasTable = null;
            wasTableNames = null;
        }
    }
}
