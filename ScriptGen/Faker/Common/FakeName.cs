using System.Collections.Generic;
using System.Linq;

using ScriptGen.Abstraction;

namespace ScriptGen.Faker.Common
{
    /// <summary>
    /// Генерирует случайные имена
    /// </summary>
    public class FakeName : FakeWordBase
    {
        private static Dictionary<char, string[]> data
            = new Dictionary<char, string[]>();

        public static void Load()
        {
            data.Add('r', FakeHelper.LoadWords("RU_Name"));
            data.Add('e', FakeHelper.LoadWords("EN_Name"));
        }

        protected override string[] GetWords(char language)
            => data.ContainsKey(language)
                ? data[language]
                : data.SelectMany(d => d.Value).ToArray();
    }
}
