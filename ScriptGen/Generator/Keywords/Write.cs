using System;
using System.Collections.Generic;
using ScriptGen.Interface;

namespace ScriptGen.Generator.Keywords
{
    /// <summary>
    /// Ключевое слово вывода информации
    /// </summary>
    public class Write : IKeyword
    {
        private static readonly Dictionary<string, Func<string, string>> constants
            = new Dictionary<string, Func<string, string>>()
            {
                { "\\fl", (word) => $"{char.ToLower(word[0])}{word.Substring(1)}" },
                { "\\fu", (word) => $"{char.ToUpper(word[0])}{word.Substring(1)}" }
            };

        public void Do(string code)
        {
            string parameter = FillHelper.GetParameter(code);
            code = code.Substring(parameter.Length + 7);
            
            while (!string.IsNullOrEmpty(parameter))
            {
                string word = FillHelper.GetConstant(parameter);
                if (string.IsNullOrEmpty(word))
                    word = FillHelper.GetSpace(parameter);
                
                if (word.StartsWith("get"))
                    TemplateFiller.Append(Set.GetValue(word));
                else
                    TemplateFiller.Append(GetFormatConstantValue(word));

                parameter = parameter.Substring(word.Length);
            }

            TemplateFiller.KeywordInvoke(FillHelper.GetWord(code), code);
        }

        private string GetFormatConstantValue(string word)
        {
            if (word.EndsWith("\\fl") || word.EndsWith("\\fu"))
            {
                string parameter = word.Substring(word.Length - 3);
                word = TemplateFiller.GetConstantValue(word.Substring(0, word.Length - 3));

                if (word.Length > 1)
                    return constants[parameter](word);
            }

            return TemplateFiller.GetConstantValue(word);
        }
    }
}
