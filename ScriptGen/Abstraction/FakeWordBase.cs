using System;

using ScriptGen.Faker;
using ScriptGen.Interface;

namespace ScriptGen.Abstraction
{
    /// <summary>
    /// Базовый класс для генерации случайных слов
    /// </summary>
    public abstract class FakeWordBase : IFaker
    {
        /// <summary>
        /// Возвращает count слов для указанного языка
        /// </summary>
        /// <param name="count">Количество слов</param>
        /// <param name="language">Язык</param>
        /// <returns>Случайные слов</returns>
        public string Next(int count, char language)
        {
            string result = "";
            string[] words = GetWords(language);
            Random random = FakeHelper.GetRandom();

            for (int i = 0; i < count; i++)
                result += $" {words[random.Next(0, words.Length)]}";

            return result.Substring(1);
        }

        /// <summary>
        /// Возвращает от minCount до maxCount слов для указанного языка
        /// </summary>
        /// <param name="minCount">Минимальное количество слов</param>
        /// <param name="maxCount">Максимальное количество слов</param>
        /// <param name="language">Язык</param>
        /// <returns>Случайные слов</returns>
        public string Next(int minCount, int maxCount, char language)
        {
            string result = "";
            string[] words = GetWords(language);
            Random random = FakeHelper.GetRandom();

            for (int i = 0; i < random.Next(minCount, maxCount) + 1; i++)
                result += $" {words[random.Next(0, words.Length)]}";

            return result.Length > 1
                ? result.Substring(1)
                : result;
        }

        /// <summary>
        /// Возвращает случайное слова из представленных
        /// </summary>
        /// <param name="values">Возможные значения</param>
        /// <returns>Случайное слово</returns>
        public string Next(string[] values)
        {
            Random random = FakeHelper.GetRandom();

            return values[random.Next(0, values.Length)];
        }

        /// <summary>
        /// Возвращает словарь слов для выборки
        /// </summary>
        /// <param name="language">Язык</param>
        /// <returns>Словарь для выборки</returns>
        protected virtual string[] GetWords(char language)
            => new string[0];
    }
}
