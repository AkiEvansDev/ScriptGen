using System.Linq;
using System.Text.RegularExpressions;

namespace ScriptGen.Settings.Model
{
    /// <summary>
    /// Модель экземпляра настроек
    /// </summary>
    public class Option
    {
        public string Name;

        public string[] FalseWords;

        public string[] SQLTypes;
        public string[] ProgrammingTypes;

        public Option(OptionData optionData)
        {
            Name = optionData.Name;

            FalseWords = GetWords(optionData.FalseWords).Select(s => s.ToLower()).ToArray();

            SQLTypes = GetWords(optionData.SQLTypes);
            ProgrammingTypes = GetWords(optionData.ProgrammingTypes);
        }

        private string[] GetWords(string data)
            => Regex.Split(data, "\\s+").Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
    }
}
