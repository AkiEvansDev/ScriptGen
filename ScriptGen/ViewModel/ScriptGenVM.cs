using System.Collections.ObjectModel;

using BaseMVVM.Abstraction;
using BaseMVVM.Command;

using Explorer;

using ScriptGen.API;
using ScriptGen.Common;
using ScriptGen.Common.SaveModel;
using ScriptGen.Faker.ViewModel;
using ScriptGen.Generator;
using ScriptGen.Interface;
using ScriptGen.Model;
using ScriptGen.Settings;

namespace ScriptGen.ViewModel
{
    /// <summary>
    /// Работа с приложение 
    /// </summary>
    public class ScriptGenVM : ViewModelBase
    {
        private bool havePages;
        private bool havePlugins;

        private ScriptGenM scriptGen = new ScriptGenM();

        private readonly FileDialog explorer = new FileDialog();
        private readonly SettingsDialog settings = new SettingsDialog();

        public ScriptGenVM()
        {
            OpenSettings = new SimpleCommand(settings.Open);
            OpenPage = new SimpleCommand(LoadPage);
            SavePage = new SimpleCommand(SaveSelectPage, () => SelectPage != null);

            AddPage = new SimpleCommand(NewPage);
            AddTable = new SimpleCommand(NewTable, () => SelectPage != null);

            CheckError = new SimpleCommand(() => IsError(), () => SelectPage != null && SelectPage.Tables.Count > 0);
            GenerateScript = new SimpleCommand(Generate, () => SelectPage != null && SelectPage.Tables.Count > 0);

            FakerContext = new FakerVM();

            PluginManager.CommonAPI.GetError = () => SelectPage != null && SelectPage.Tables.Count > 0
                ? IsError()
                : false;

            PluginManager.CommonAPI.GetTables = () => SelectPage != null && SelectPage.Tables.Count > 0
                ? DataConverter.IPageToInfo(SelectPage) 
                : null;

            PluginManager.OnPluginsUpdate = () => HavePlugins = Plugins.Count > 0;
            HavePlugins = Plugins.Count > 0;

            FileDialog.OnInfo = StatusBarManagerVM.ExplorerInfo;
            FileDialog.OnError = StatusBarManagerVM.ExplorerError;

            StatusBarManagerVM.Message("Всё хорошо (наверное)!");
        }

        public SimpleCommand OpenSettings { get; }
        public SimpleCommand OpenPage { get; }
        public SimpleCommand SavePage { get; }

        public SimpleCommand AddPage { get; }
        public SimpleCommand AddTable { get; }

        public SimpleCommand CheckError { get; }
        public SimpleCommand GenerateScript { get; }

        public FakerVM FakerContext { get; }

        public ObservableCollection<IPage> Pages
            => scriptGen.Pages;
        
        public ObservableCollection<PluginData> Plugins
            => PluginManager.Plugins;

        public StatusBarManagerVM StatusBarManager { get; }
            = new StatusBarManagerVM();
        
        public double ActualWidth { get; set; }
        public double ActualHeight { get; set; }

        public bool HavePages
        {
            get => havePages;
            set
            {
                havePages = value;

                OnPropertyChanged();
            }
        }

        public bool HavePlugins
        {
            get => havePlugins;
            set
            {
                havePlugins = value;

                OnPropertyChanged();
            }
        }

        public IPage SelectPage
        {
            get => scriptGen.SelectPage;
            set
            {
                if (scriptGen.SelectPage == null || !scriptGen.SelectPage.Equals(value))
                {
                    scriptGen.SelectPage = value;

                    if (value != null)
                    {
                        SelectPage.ActualWidth = ActualWidth;
                        SelectPage.ActualHeight = ActualHeight;
                    }

                    OnPropertyChanged();
                }
            }
        }

        public ModelType TypeModel
        {
            get => scriptGen.ModelType;
            set
            {
               scriptGen.ModelType = value;

                for (int i = 0; i < Pages.Count; i++)
                    Pages[i].TypeModel = value;
            }
        }

        private void NewTable() 
            => SelectPage?.AddTable(new TableVM(new PrimaryFieldVM()));

        private void NewPage()
        {
            IPage page = new PageVM()
            {
                Name = "Новая страница",
                TypeModel = TypeModel,
                ActualWidth = ActualWidth,
                ActualHeight = ActualHeight
            };

            page.OnRemovePage += RemovePage;
            page.OnSelectPage += (p) => SelectPage = p;

            Pages.Add(page);
            HavePages = true;

            SelectLast();
        }

        private void RemovePage(IPage page)
        {
            if (SelectPage != null && SelectPage.Equals(page))
                SelectPage = null;

            Pages.Remove(page);
            HavePages = Pages.Count > 0;

            if (SelectPage == null && HavePages)
                SelectLast();
        }

        private void SelectLast()
        {
            int i;
            for (i = 0; i < Pages.Count - 1; i++)
                Pages[i].IsSelect = false;

            SelectPage = Pages[i];
            SelectPage.IsSelect = true;
        }

        private void Select(IPage page)
        {
            if (page == null)
                return;

            for (int i = 0; i < Pages.Count; i++)
                Pages[i].IsSelect = false;

            SelectPage = page;
            SelectPage.IsSelect = true;
        }

        private bool IsError()
        {
            SelectPage.SetError();

            if (SelectPage.IsError)
            {
                IPage page = SelectPage;
                SelectPage.OnRemovePage += (t) => page = null;

                StatusBarManagerVM.Error($"На схеме \"{SelectPage.Name}\" найдены ошибки(-а)!", "перейти",
                    new SimpleCommand(() => Select(page), () => page != null));

                return true;
            }
            
            StatusBarManagerVM.Message("Ошибок не обнаружено!");
            return false;
        }

        private void SaveSelectPage()
        {
            DataBaseSave dataBaseSave = DataConverter.IPageToSave(SelectPage);

            if (explorer.Open(ExplorerSelectType.All, ExplorerType.Save, ".xml", ".txt", ""))
                DataBaseFileWork.Save(dataBaseSave, explorer.SelectPath);
        }

        private void LoadPage()
        {
            if (explorer.Open(ExplorerSelectType.File, ExplorerType.Open, ".xml") 
                && DataBaseFileWork.Load(explorer.SelectPath, out DataBaseSave dataBase))
            {
                NewPage();
                SelectPage.Load(dataBase);
            }
        }

        private void Generate()
        {
            if (IsError())
                return;

            if (explorer.Open(ExplorerSelectType.Folder, ExplorerType.Save))
                ScriptGenerator.Generate(DataConverter.IPageToInfo(SelectPage), explorer.SelectPath);
        }
    }
}
