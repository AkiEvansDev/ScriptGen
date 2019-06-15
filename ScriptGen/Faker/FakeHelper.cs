using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ScriptGen.Faker
{
    /// <summary>
    /// Осуществляет выделение параметров шаблона
    /// </summary>
    public static class FakeHelper
    {
        public static Random GetRandom()
            => new Random(Guid.NewGuid().GetHashCode());

        public static int[] GetRange(string parameter)
            => parameter.Split('-').Select(s => int.TryParse(s, out int value) ? value : -1).Where(i => i > 0).ToArray();

        public static string[] GetParameters(string parameter)
            => parameter.Split(',').Where(s => !string.IsNullOrEmpty(s)).ToArray();

        public static char GetLanguageChar(ref string parameter)
        {
            char result = '-';

            if (!parameter.Contains(',') && parameter.Length > 0 && char.IsLetter(parameter[0]))
            {
                result = parameter[0];
                parameter = parameter.Substring(1);
            }

            return result;
        }

        public static string[] LoadWords(string resource)
        {
            string result = Properties.Resources.ResourceManager.GetString(resource);

            return !string.IsNullOrEmpty(result) 
                ? Regex.Split(result, "\\s+") 
                : new string[1] { "" };
        }
    }
}
