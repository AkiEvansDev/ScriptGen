using System.Linq;

namespace ScriptGen.Generator
{
    /// <summary>
    /// Осуществляет выделение ключевых слов и параметров из текста
    /// </summary>
    public static class FillHelper
    {
        public static string GetSpace(string code)
           => new string(code.TakeWhile(c => !char.IsLetter(c)).ToArray());

        public static string GetWord(string code)
            => new string(code.TakeWhile(c => char.IsLetter(c)).ToArray());

        public static string GetConstant(string code)
        {
            string result = new string(code.TakeWhile(c => char.IsLetterOrDigit(c) || ".?\\[]{".Contains(c)).ToArray());

            return code.Length > result.Length && code[result.Length] == '}'
                ? result + '}'
                : result;
        }

        public static string GetParameter(string code)
            => GetBlock(code, '(', ')');

        public static string GetBlock(string code)
            => GetBlock(code, '{', '}');

        private static string GetBlock(string code, char startDivider, char endDivider)
        {
            if (code.Contains(startDivider) && code.Contains(endDivider))
            {
                code = code.Substring(code.IndexOf(startDivider));

                int openCount = 0;
                int closeCount = 0;

                for (int i = 0; i < code.Length; i++)
                {
                    if (code[i] == startDivider)
                        openCount++;
                    else if (code[i] == endDivider)
                        closeCount++;

                    if (openCount == closeCount && openCount != 0)
                        return code.Substring(1, i - 1);
                }
            }

            return "";
        }

        public static int GetConstantParameter(string constant, out string newConstant)
        {
            newConstant = constant;
            int startIndex = 0;
            int endIndex = 0;

            if (constant.Contains('{') && constant.Contains('}'))
            {
                startIndex = constant.IndexOf('{');
                endIndex = constant.IndexOf('}');
            }
            else if (constant.Contains('[') && constant.Contains(']'))
            {
                startIndex = constant.IndexOf('[');
                endIndex = constant.IndexOf(']');
            }

            if (startIndex == 0 || endIndex == 0)
                return 0;

            if (int.TryParse(constant.Substring(startIndex + 1, endIndex - startIndex - 1), out int parameter))
            {
                newConstant = constant.Substring(0, startIndex) + constant.Substring(endIndex + 1);
                return parameter;
            }
            else
                return 0;
        }
    }
}
