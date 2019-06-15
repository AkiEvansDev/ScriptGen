using System.Collections.Generic;

using ScriptGen.Interface;

namespace ScriptGen.Generator.Keywords
{
    /// <summary>
    /// Ключевое слово запоминания значения
    /// </summary>
    public class Set : IKeyword
    {
        private static Dictionary<string, string> variables
            = new Dictionary<string, string>() { };

        public void Do(string code)
        {
            string parameter = FillHelper.GetParameter(code);
            code = code.Substring(parameter.Length + 5);

            string[] parameters = parameter.Split(',');
            if (parameters.Length == 2)
            {
                parameters[0] = $"get[{parameters[0]}]";

                if (variables.ContainsKey(parameters[0]))
                    variables[parameters[0]] = TemplateFiller.GetConstantValue(parameters[1]);
                else
                    variables.Add(parameters[0], TemplateFiller.GetConstantValue(parameters[1]));
            }

            TemplateFiller.KeywordInvoke(FillHelper.GetWord(code), code);
        }

        public static string GetValue(string name) 
            => variables.ContainsKey(name) ? variables[name] : name;

        public static void Reset() 
            => variables = new Dictionary<string, string>();
    }
}
