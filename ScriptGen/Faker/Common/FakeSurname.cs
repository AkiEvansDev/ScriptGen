using System.Collections.Generic;
using System.Linq;

using ScriptGen.Abstraction;

namespace ScriptGen.Faker.Common
{
    /// <summary>
    /// Генерирует случайные фамилии
    /// </summary>
    public class FakeSurname : FakeWordBase
    {
        private static Dictionary<char, string[]> data
            = new Dictionary<char, string[]>();

        public static void Load()
        {
            data.Add('r', FakeHelper.LoadWords("RU_Surname"));
            data.Add('e', FakeHelper.LoadWords("EN_Surname"));
        }

        protected override string[] GetWords(char language)
            => data.ContainsKey(language)
                ? data[language]
                : data.SelectMany(d => d.Value).ToArray();
    }
}
