using System;
using System.Collections.Generic;
using System.Linq;

using BaseMVVM.Abstraction;
using BaseMVVM.Command;

using ScriptGen.Faker.Common;
using ScriptGen.Interface;

namespace ScriptGen.Faker.Model
{
    /// <summary>
    /// Модель генерации случайного значения
    /// </summary>
    public class FakerM : ViewModelBase
    {
        public event Action<FakerM> OnRemoveFaker;

        private static readonly Dictionary<char, IFaker> keywords
            = new Dictionary<char, IFaker>()
            {
                { 'd', new FakeNumber() },
                { 'l', new FakeChar() },
                { 'w', new FakeWord() },
                { 'n', new FakeName() },
                { 's', new FakeSurname() }
            };

        private string templateText = "Пользовательский";

        private bool isUnique = false;
        private string template = "";

        private int reRandomCount;

        private List<string> wasValues = new List<string>();

        public FakerM() 
            => RemoveFaker = new SimpleCommand(() => OnRemoveFaker?.Invoke(this));

        public SimpleCommand RemoveFaker { get; }

        public static Dictionary<string, string> DefaultTemplates { get; } 
            = new Dictionary<string, string>()
            {
                { "Пользовательский", "" },
                { "Имя", "n{r1}" },
                { "Фамилия", "s{r1}" },
                { "Текст", "w{r10-100}" },
                { "Телефон", "+d{7,8} (d{3}) d{3}-d{2}-d{2}" },
                { "Почта", "l{e4-8}d{2-4}@gmail.com" },
                { "Пароль", "l{e4-8}d{2-4}l{e3-5}d{0-2}" },
                { "Пол", "l{м,ж}" }
            };

        public string TemplateText
        {
            get => templateText;
            set
            {
                templateText = value;
                Template = DefaultTemplates[templateText];

                OnPropertyChanged();
            }
        }

        public bool IsUnique
        {
            get => isUnique;
            set
            {
                isUnique = value;

                OnPropertyChanged();
            }
        }

        public string Template
        {
            get => template;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    template = value;

                    if (TemplateText != "Пользовательский" && !DefaultTemplates.ContainsValue(value))
                        TemplateText = "Пользовательский";
                }

                OnPropertyChanged();
            }
        }

        public bool Randomize(out string result)
        {
            result = "";
            string template = Template;

            while (!string.IsNullOrEmpty(template))
            {
                char command = template[0];
                template = template.Substring(1);

                if (keywords.ContainsKey(command) && template.Length > 2 && template[0] == '{' && template.Contains('}'))
                {
                    string parameter = template.Substring(1, template.IndexOf('}') - 1);
                    template = template.Substring(parameter.Length + 2);

                    result += KeywordInvoke(command, parameter);
                }
                else
                    result += command;
            }

            if (IsUnique && wasValues.Contains(result))
                return reRandomCount++ < 100 
                    ? Randomize(out result) 
                    : false;

            wasValues.Add(result);
            reRandomCount = 0;

            return true;
        }

        public void Reset()
            => wasValues.Clear();

        private string KeywordInvoke(char command, string parameter)
        {
            char language = FakeHelper.GetLanguageChar(ref parameter);

            if (int.TryParse(parameter, out int count) && count > 0)
                return keywords[command].Next(count, language);
            else if (parameter.Contains('-'))
            {
                int[] parameters = FakeHelper.GetRange(parameter);

                if (parameters.Length == 2 && parameters[0] < parameters[1] && parameters[0] >= 0)
                    return keywords[command].Next(parameters[0], parameters[1], language);
            }
            else if (parameter.Contains(','))
            {
                string[] parameters = FakeHelper.GetParameters(parameter);

                if (parameters.Length > 0)
                    return keywords[command].Next(parameters);
            }

            return "";
        }
    }
}
