using System;
using System.Linq;

using ScriptGen.Interface;

namespace ScriptGen.Faker.Common
{
    /// <summary>
    /// Генерирует случайные цифры
    /// </summary>
    public class FakeNumber : IFaker
    {
        private static readonly string data = "0123456789";
        
        public string Next(int count, char language)
        {
            string result = "";
            Random random = FakeHelper.GetRandom();

            for (int i = 0; i < count; i++)
                result += data[random.Next(0, 10)];

            return result;
        }

        public string Next(int minCount, int maxCount, char language)
        {
            string result = "";
            Random random = FakeHelper.GetRandom();

            for (int i = 0; i < random.Next(minCount, maxCount); i++)
                result += data[random.Next(0, 10)];

            return result;
        }

        public string Next(string[] values)
        {
            Random random = FakeHelper.GetRandom();

            return values.All(s => double.TryParse(s.Replace('.', ','), out double value))
                ? values[random.Next(0, values.Length)]
                : "";
        }
    }
}
