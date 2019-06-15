using System;
using System.Linq;
using System.Collections.Generic;

using ScriptGen.Interface;

using ScriptGenPlugin.Model;

namespace ScriptGen.Generator.Keywords
{
    /// <summary>
    /// Ключевое слово условия
    /// </summary>
    public class If : IKeyword
    {
        private readonly static Dictionary<string, Func<int, bool>> conditions
            = new Dictionary<string, Func<int, bool>>()
            {
                { "true", (_) => true },

                { "field.isfk",     (param) => TemplateFiller.Table.Fields[param].Type == FieldType.FK },
                { "field.ispk",     (param) => TemplateFiller.Table.Fields[param].Type == FieldType.PK },
                { "field.isany",    (param) => TemplateFiller.Table.Fields[param].Type == FieldType.Any },
                { "field.isnull",   (param) => TemplateFiller.Table.Fields[param].IsNull },
                { "field.isunique", (param) => TemplateFiller.Table.Fields[param].IsUnique }
            };

        public void Do(string code)
        {
            List<string> parameters = new List<string>() { FillHelper.GetParameter(code) };
            List<string> blocks = new List<string>() { FillHelper.GetBlock(code) };

            code = code.Substring(parameters[0].Length + blocks[0].Length + 6);

            while (true)
            {
                if (code.StartsWith("elseif"))
                {
                    parameters.Add(FillHelper.GetParameter(code));
                    blocks.Add(FillHelper.GetBlock(code));

                    code = code.Substring(parameters.Last().Length + blocks.Last().Length + 10);
                }
                else if (code.StartsWith("else"))
                {
                    parameters.Add("true");
                    blocks.Add(FillHelper.GetBlock(code));

                    code = code.Substring(blocks.Last().Length + 6);
                }
                else
                    break;
            }

            Do(parameters.ToArray(), blocks.ToArray());

            TemplateFiller.KeywordInvoke(FillHelper.GetWord(code), code);
        }

        private void Do(string[] parameters, string[] blocks)
        {
            if (parameters.Length != blocks.Length)
                return;

            for (int i = 0; i < parameters.Length; i++)
                if (ConditionInvoke(parameters[i]))
                {
                    TemplateFiller.KeywordInvoke(FillHelper.GetWord(blocks[i]), blocks[i]);
                    break;
                }
        }

        private bool ConditionInvoke(string condition)
        {
            bool isInverse = false;
            if (!string.IsNullOrEmpty(condition) && condition[0] == '!')
            {
                condition = condition.Substring(1);
                isInverse = true;
            }

            int param = FillHelper.GetConstantParameter(condition, out condition);

            if (conditions.ContainsKey(condition))
            {
                bool result = conditions[condition].Invoke(param);
                return isInverse ? !result : result;
            }
            else
                return false;
        }
    }
}
  
