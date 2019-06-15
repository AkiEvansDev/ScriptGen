using System;
using System.Collections.Generic;

using ScriptGen.Interface;

using ScriptGenPlugin.Model;

namespace ScriptGen.Generator.Keywords
{
    /// <summary>
    /// Ключевое слово цикла
    /// </summary>
    public class For : IKeyword
    {
        private readonly static Dictionary<string, Action<string>> parameters
            = new Dictionary<string, Action<string>>()
            {
                { "table.fields",    (s) => FieldFor(s) },
                { "table.reftables", (s) => RefTableFor(s) },
                { "tables",          (s) => AllTableFor(s) }
            };

        public void Do(string code)
        {
            string parameter = FillHelper.GetParameter(code);
            code = code.Substring(parameter.Length + 5);

            string block = FillHelper.GetBlock(code);
            code = code.Substring(block.Length + 2);           
            
            if (parameters.ContainsKey(parameter))
                parameters[parameter].Invoke(block);

            TemplateFiller.KeywordInvoke(FillHelper.GetWord(code), code);
        }

        private static void FieldFor(string block)
        {
            for (int i = 0; i < TemplateFiller.Table.Fields.Length; i++)
                TemplateFiller.KeywordInvoke(FillHelper.GetWord(block), block.Replace("field", $"field[{i}]"));
        }
        
        private static void RefTableFor(string block)
        {
            TableInfo saveTable = TemplateFiller.Table;
            
            for (int i = 0; i < TemplateFiller.Table.RefTables.Length; i++)
            {
                TemplateFiller.Table = saveTable.RefTables[i];
                TemplateFiller.KeywordInvoke(FillHelper.GetWord(block), block);
            }

            TemplateFiller.Table = saveTable;
        }

        private static void AllTableFor(string block)
        {
            TableInfo saveTable = TemplateFiller.Table;

            for (int i = 0; i < TemplateFiller.Tables.Length; i++)
            {
                TemplateFiller.Table = TemplateFiller.Tables[i];
                TemplateFiller.KeywordInvoke(FillHelper.GetWord(block), block);
            }

            TemplateFiller.Table = saveTable;
        }
    }
}
