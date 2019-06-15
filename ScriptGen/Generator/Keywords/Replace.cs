using ScriptGen.Interface;

namespace ScriptGen.Generator.Keywords
{
    /// <summary>
    /// Ключевое слово замены слов
    /// </summary>
    public class Replace : IKeyword
    {
        public void Do(string code)
        {
            string parameter = FillHelper.GetParameter(code);
            code = code.Substring(parameter.Length + 9);

            string[] parameters = parameter.Split(',');
            if (parameters.Length == 2)
                TemplateFiller.Replace(parameters[0], parameters[1]);

            TemplateFiller.KeywordInvoke(FillHelper.GetWord(code), code);
        }
    }
}
