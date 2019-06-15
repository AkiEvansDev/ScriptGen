using System.Collections.ObjectModel;
using System.Text;

using BaseMVVM.Abstraction;
using BaseMVVM.Command;

using Explorer;

using ScriptGen.Common;
using ScriptGen.Faker.Common;
using ScriptGen.Faker.Model;
using ScriptGen.ViewModel;

namespace ScriptGen.Faker.ViewModel
{
    /// <summary>
    /// Осуществляет генерации записей по шаблону с случайными значениями
    /// </summary>
    public class FakerVM : ViewModelBase
    {
        private int count = 1;
        private string template = "";

        private FileDialog fileDialog
            = new FileDialog();

        public FakerVM()
        {
            if (FileWork.BinaryLoad(@"Data\faker.bin", out string errorMessage, out object loadTemplates))
                Templates = (ObservableCollection<string>)loadTemplates;

            FakeWord.Load();
            FakeName.Load();
            FakeSurname.Load();

            AddFaker = new SimpleCommand(NewFaker);

            SaveTemplate = new SimpleCommand(() =>
            {
                if (!Templates.Contains(Template))
                {
                    Templates.Add(Template);
                    StatusBarManagerVM.Message("Шаблон сохранён!", Template);
                }
            });
            RemoveTemplate = new SimpleCommand(() =>
            {
                if (Templates.Contains(Template))
                {
                    StatusBarManagerVM.Message("Шаблон удалён!", Template);
                    Templates.Remove(Template);
                }
            });

            Randomize = new SimpleCommand(Generate, () => Fakers.Count > 0 && Template.Length > 0);
        }

        public SimpleCommand AddFaker { get; }

        public SimpleCommand SaveTemplate { get; }
        public SimpleCommand RemoveTemplate { get; }

        public SimpleCommand Randomize { get; }

        public static ObservableCollection<string> Templates { get; private set; }

        public static void SaveDefaultTemplates()
            => FileWork.BinarySave(Templates, @"Data\faker.bin", out string errorMessage);

        public ObservableCollection<FakerM> Fakers { get; }
            = new ObservableCollection<FakerM>();

        public string Count
        {
            get => count.ToString();
            set
            {
                if (int.TryParse(value, out int v)  && v > 0 && v < 101)
                    count = v;

                OnPropertyChanged();
            }
        }

        public string Template
        {
            get => template;
            set
            {
                template = value;

                OnPropertyChanged();
            }
        }

        private void NewFaker()
        {
            FakerM faker = new FakerM();
            faker.OnRemoveFaker += (f) => Fakers.Remove(f);

            Fakers.Add(faker);
        }

        private void Generate()
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < Fakers.Count; i++)
                Fakers[i].Reset();

            for (int i = 0; i < count; i++)
            {
                string templateResult = GetTemplateResult();

                if (!string.IsNullOrEmpty(templateResult))
                    result.AppendLine(templateResult);
                else
                {
                    StatusBarManagerVM.Info("Не удалось получить уникальное значение!", $"Завершено итераций {i} из {count}!",
                        "сохранить", new SimpleCommand(() => Save(result)));
                    return;
                }
            }

            Save(result);
        }

        private string GetTemplateResult()
        {
            string result = Template;

            for (int i = 0; i < Fakers.Count; i++)
                if (Fakers[i].Randomize(out string value))
                    result = result.Replace($"@v{i + 1}", value);
                else
                    return null;

            return result;
        }

        private void Save(StringBuilder result)
        {
            if (fileDialog.Open(ExplorerSelectType.All, ExplorerType.Save, ".txt", ".sql"))
                FileWork.TextSave(result, fileDialog.SelectPath, "faker", ".txt");
        }
    }
}
