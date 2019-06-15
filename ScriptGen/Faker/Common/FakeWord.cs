using System.Collections.Generic;
using System.Linq;

using ScriptGen.Abstraction;

namespace ScriptGen.Faker.Common
{
    /// <summary>
    /// Генерирует случайные слова
    /// </summary>
    public class FakeWord : FakeWordBase
    {
        private static Dictionary<char, string[]> data
            = new Dictionary<char, string[]>();

        public static void Load()
        {
            data.Add('r', FakeHelper.LoadWords("RU_Word"));
            data.Add('e', FakeHelper.LoadWords("EN_Word"));
        }
        
        protected override string[] GetWords(char language)
            => data.ContainsKey(language)
                ? data[language]
                : data.SelectMany(d => d.Value).ToArray();
    }
}
