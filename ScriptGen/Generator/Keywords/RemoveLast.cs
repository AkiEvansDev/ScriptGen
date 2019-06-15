using ScriptGen.Interface;

namespace ScriptGen.Generator.Keywords
{
    /// <summary>
    /// Ключевое слово удаления последних символов
    /// </summary>
    public class RemoveLast : IKeyword
    {
        public void Do(string code)
        {
            string parameter = FillHelper.GetParameter(code);
            code = code.Substring(parameter.Length + 12);

            if (int.TryParse(parameter, out int length))
                TemplateFiller.RemoveLast(length);
            
            TemplateFiller.KeywordInvoke(FillHelper.GetWord(code), code);
        }
    }
}