using System;
using System.Collections.Generic;
using System.Linq;

using ScriptGen.Interface;

namespace ScriptGen.Faker.Common
{
    /// <summary>
    /// Генерирует случайные символы
    /// </summary>
    public class FakeChar : IFaker
    {
        private static readonly Dictionary<char, string> data
            = new Dictionary<char, string>()
            {
                { 'r', "йцукенгшщзхъфывапролджэячсмитьбю" },
                { 'e', "qwertyuiopasdfghjklzxcvbnm" }
            };

        public string Next(int count, char language)
        {
            string result = "";
            string chars = GetChars(language);
            Random random = FakeHelper.GetRandom();

            for (int i = 0; i < count; i++)
                result += chars[random.Next(0, chars.Length)];

            return result;
        }

        public string Next(int minCount, int maxCount, char language)
        {
            string result = "";
            string chars = GetChars(language);
            Random random = FakeHelper.GetRandom();

            for (int i = 0; i < random.Next(minCount, maxCount); i++)
                result += chars[random.Next(0, chars.Length)];

            return result;
        }

        public string Next(string[] values)
        {
            Random random = FakeHelper.GetRandom();

            return values.All(s => s.Length == 1) 
                ? values[random.Next(0, values.Length)] 
                : "";
        }

        private static string GetChars(char language) 
            => data.ContainsKey(language) 
                ? data[language] 
                : string.Join("", data.Select(d => d.Value));
    }
}
