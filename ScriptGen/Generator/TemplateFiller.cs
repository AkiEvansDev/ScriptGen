using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using ScriptGen.Generator.Keywords;
using ScriptGen.Interface;

using ScriptGenPlugin.Model;

namespace ScriptGen.Generator
{
    /// <summary>
    /// Осуществляет нормализацию шаблона
    /// </summary>
    public static class TemplateFiller
    {
        public static TableInfo[] Tables;
        public static TableInfo Table;

        private static string template;
        private static StringBuilder result;
        
        private static readonly Dictionary<string, IKeyword> keywords
            = new Dictionary<string, IKeyword>()
            {
                { "write",      new Write() },
                { "if",         new If() },
                { "for",        new For() },
                { "rec",        new Rec() },
                { "removelast", new RemoveLast() },
                { "replace",    new Replace() },
                { "once",       new Once() },
                { "set",        new Set() }
            };

        private static readonly Dictionary<string, Func<int, string>> constants
           = new Dictionary<string, Func<int, string>>()
           {
                { "tab", (param) => Pad("    ", param) },
                { "nl",  (param) => Pad("\n", param) },

                { "table.id.name",        (_) => constants["field.name"](0) },
                { "table.id.fullname",    (_) => constants["field.fullname"](0) },
                { "table.id.logicalname", (_) => constants["field.logicalname"](0) },
                { "table.id.programmingname",  (_) => constants["field.programmingname"](0) },
                { "table.id.datatype",    (_) => constants["field.datatype"](0) },
                { "table.id.programmingtype",  (_) => constants["field.programmingtype"](0) },

                { "table.name",        (_) => Table.Name },
                { "table.logicalname", (_) => Table.LogicalName },
                { "table.programmingname",  (_) => Table.ProgrammingName },

                { "field.name",        (param) => Table.Fields[param].Name },
                { "field.fullname",    (param) => $"{Table.Name}.{Table.Fields[param].Name}" },
                { "field.logicalname", (param) => Table.Fields[param].LogicalName },
                { "field.programmingname",  (param) => Table.Fields[param].ProgrammingName },
                { "field.datatype",    (param) => Table.Fields[param].DataType },
                { "field.programmingtype",  (param) => Table.Fields[param].ProgrammingType },

                { "field.reftable.id.name",        (param) => Table.Fields[param].RefTable.Fields[0].Name },
                { "field.reftable.id.fullname",    (param) => $"{Table.Fields[param].RefTable.Name}.{Table.Fields[param].RefTable.Fields[0].Name}" },
                { "field.reftable.id.logicalname", (param) => Table.Fields[param].RefTable.Fields[0].LogicalName },
                { "field.reftable.id.programmingname",  (param) => Table.Fields[param].RefTable.Fields[0].ProgrammingName },
                { "field.reftable.id.datatype",    (param) => Table.Fields[param].RefTable.Fields[0].DataType },
                { "field.reftable.id.programmingtype",  (param) => Table.Fields[param].RefTable.Fields[0].ProgrammingType },

                { "field.reftable.name",        (param) => Table.Fields[param].RefTable.Name },
                { "field.reftable.logicalname", (param) => Table.Fields[param].RefTable.LogicalName },
                { "field.reftable.programmingname",  (param) => Table.Fields[param].RefTable.ProgrammingName },
           };

        public static void Append(string content)
            => result.Append(content);

        public static void Replace(string oldValue, string newValue)
            => result.Replace(oldValue, newValue);

        public static void RemoveLast(int length)
            => result.Remove(result.Length - length, length);

        public static string Fill(TableInfo table, string template)
        {
            Rec.Reset();
            Set.Reset();

            TemplateFiller.template = template;
            result = new StringBuilder();
            Table = table;

            NextBlock();
            
            return result.ToString();
        }

        public static bool KeywordInvoke(string keyword, string code)
        {
            if (keywords.ContainsKey(keyword))
            {
                keywords[keyword].Do(code);
                return true;
            }

            return false;
        }

        public static string GetConstantValue(string word)
        {
            if (word.Contains('?'))
                return GetNotNullConstantValue(word);

            int param = FillHelper.GetConstantParameter(word.ToLower(), out string constant);

            return constants.ContainsKey(constant) 
                ? constants[constant].Invoke(param) 
                : word;
        }

        private static string GetNotNullConstantValue(string word)
        {
            string[] constants = word.Split('?');

            return constants.Length == 2
                ? GetNonNullString(GetConstantValue(constants[0]), GetConstantValue(constants[1])) 
                : word;
        }

        private static void NextBlock()
        {
            if (template.Contains("{{") && template.Contains("}}"))
            {
                int index = template.IndexOf("{{");

                result.Append(template.Substring(0, index));
                template = template.Substring(index + 2);

                index = template.IndexOf("}}");

                string code = NormalizeCode(template.Substring(0, index));
                template = template.Substring(index + 2);

                KeywordInvoke(FillHelper.GetWord(code), code);

                NextBlock();
            }
            else
                result.Append(template);
        }

        private static string NormalizeCode(string code)
        {
            string result = "";
            while (code.Contains("write"))
            {
                int index = code.IndexOf("write");

                result += Regex.Replace(code.Substring(0, index), @"\s+", "").ToLower();
                result = Regex.Replace(result, @"\[\d+\]", "");
                code = code.Substring(index + 5);

                if (code.Contains('(') && code.Contains(')'))
                {
                    string block = FillHelper.GetParameter(code);

                    result += $"write({block})";
                    code = code.Substring(block.Length + 2);
                }
            }

            result += Regex.Replace(code, @"\s+", "").ToLower();
            return Regex.Replace(result, @"\[\d+\]", "");
        }

        private static string GetNonNullString(string content, string alternativeContent)
            => string.IsNullOrWhiteSpace(content) ? alternativeContent : content;

        private static string Pad(string padding, int count)
        {
            string result = "";

            for (int i = 0; i < count; i++)
                result += padding;

            return result;
        }
    }
}
