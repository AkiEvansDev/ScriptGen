using System.Collections.Generic;
using System.Linq;

namespace ScriptGen.Common
{
    /// <summary>
    /// Проверка строк
    /// </summary>
    public static class Verification
    {
        /// <summary>
        /// Возвращает объединение двух строк через \n, если одна из строк пустая вернёт другую
        /// </summary>
        /// <param name="string1">Первая строка</param>
        /// <param name="string2">Вторая строка</param>
        /// <returns>Результат</returns>
        public static string ConcatString(string string1, string string2)
            => !string.IsNullOrEmpty(string1) && !string.IsNullOrEmpty(string2)
                ? $"{string1}\n{string2}"
                : !string.IsNullOrEmpty(string1)
                    ? string1
                    : string2;

        /// <summary>
        /// Проверяет, содержит ли строка только английские буквы
        /// </summary>
        /// <param name="data">Строка</param>
        /// <returns>true/false содержит ли строка только английские буквы</returns>
        public static bool IsOnlyEnLetter(string data)
            => data.ToLower().All(c => "qwertyuiopasdfghjklzxcvbnm".Contains(c));

        /// <summary>
        /// Проверяет, содержит ли строка только английские буквы и разрешённые символы
        /// </summary>
        /// <param name="data">Строка</param>
        /// <param name="allowedChar">Разрешённые символы</param>
        /// <returns>true/false содержит ли строка только английские буквы и разрешённые символы</returns>
        public static bool IsOnlyEnLetterOrAllowedChar(string data, params char[] allowedChar)
            => data.ToLower().All(c => "qwertyuiopasdfghjklzxcvbnm".Contains(c) || allowedChar.Contains(c));

        /// <summary>
        /// Проверяет, содержит ли строка только буквы и разрешённые символы
        /// </summary>
        /// <param name="data">Строка</param>
        /// <param name="allowedChar">Разрешённые символы</param>
        /// <returns>true/false содержит ли строка только буквы и разрешённые символы</returns>
        public static bool IsOnlyLetterOrAllowedChar(string data, params char[] allowedChar)
            => data.ToLower().All(c => char.IsLetter(c) || allowedChar.Contains(c));

        /// <summary>
        /// Проверяет, содержит ли строка только буквы, цифры и разрешённые символы
        /// </summary>
        /// <param name="data">Строка</param>
        /// <param name="allowedChar">Разрешённые символы</param>
        /// <returns>true/false содержит ли строка только буквы, цифры и разрешённые символы</returns>
        public static bool IsOnlyEnLetterOrDigitOrAllowedChar(string data, params char[] allowedChar)
            => data.ToLower().All(c => "qwertyuiopasdfghjklzxcvbnm1234567890".Contains(c) || allowedChar.Contains(c));

        /// <summary>
        /// Проверяет, является ли строка словом из списка недопустимых
        /// </summary>
        /// <param name="data">Строка</param>
        /// <returns>true/false является ли строка словом из списка недопустимых</returns>
        public static bool IsFalseWord(string data)
            => Info.FalseWords.Contains(data.ToLower());

        /// <summary>
        /// Проверяет является ли элемент уникальным для последовательности
        /// </summary>
        /// <param name="data">Последовательность</param>
        /// <param name="index">Индекс элемента, который необходимо проверить</param>
        /// <returns>true/false является ли элемент уникальным для последовательности</returns>
        public static bool IsUniqueElement(IEnumerable<string> data, int index)
        {
            if (index < 0 || index > data.Count())
                return true;

            string element = data.ElementAt(index);

            return string.IsNullOrEmpty(element) 
                ? true 
                : data.Count(s => !string.IsNullOrWhiteSpace(s) && s.Equals(element)) == 1;
        }

    }
}