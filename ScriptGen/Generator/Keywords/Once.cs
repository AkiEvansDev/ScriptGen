using ScriptGen.Interface;

namespace ScriptGen.Generator.Keywords
{
    /// <summary>
    /// Ключевое слово блока кода для выполнения один раз за генерацию
    /// </summary>
    public class Once : IKeyword
    {
        private static bool was;

        public void Do(string code)
        {
            string block = FillHelper.GetBlock(code.Substring(4));
            code = code.Substring(block.Length + 6);

            if (!was)
            {
                TemplateFiller.KeywordInvoke(FillHelper.GetWord(block), block);
                was = true;
            }

            TemplateFiller.KeywordInvoke(FillHelper.GetWord(code), code);
        }

        public static void Reset() 
            => was = false;
    }
}
