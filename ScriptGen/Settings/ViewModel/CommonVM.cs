using System.Collections.ObjectModel;

using BaseMVVM.Abstraction;
using BaseMVVM.Command;

using Explorer;

using ScriptGen.API;
using ScriptGen.Common;
using ScriptGen.Settings.Model;

namespace ScriptGen.Settings.ViewModel
{
    /// <summary>
    /// Управляет основными настройками приложения
    /// </summary>
    public class CommonVM : ViewModelBase
    {
        private CommonM common;
        private CommonM oldCommon;

        private FileDialog fileDialog
            = new FileDialog();

        public CommonVM()
        {
            common.ActiveOptionIndex = Info.ActiveOptionIndex;
            common.ActiveTemplateSQLIndex = Info.ActiveTemplateSQLIndex;
            common.ActiveTemplateProgrammingIndex = Info.ActiveTemplateProgrammingIndex;

            AddOption = new SimpleCommand(LoadOption);
            AddTemplate = new SimpleCommand(LoadTemplate);
            OpenInExplorer = new SimpleCommand(() => FileWork.OpenInExplorer("Data"));

            AddPlugin = new SimpleCommand(LoadPlugin);

            Info.OnSettingsAdd += () =>
            {
                OnPropertyChanged("OptionNames");
                OnPropertyChanged("SqlNames");
                OnPropertyChanged("ProgrammingNames");
            };
        }

        public SimpleCommand AddOption { get; }
        public SimpleCommand AddTemplate { get; }
        public SimpleCommand OpenInExplorer { get; }

        public SimpleCommand AddPlugin { get; }

        public string[] OptionNames
            => Info.OptionNames;

        public string[] SqlNames
            => Info.SqlNames;

        public string[] ProgrammingNames
            => Info.ProgrammingNames;

        public ObservableCollection<PluginData> Plugins
            => PluginManager.Plugins;

        public int ActiveOptionIndex
        {
            get => common.ActiveOptionIndex;
            set
            {
                common.ActiveOptionIndex = value;

                OnPropertyChanged();
            }
        }

        public int ActiveTemplateSQLIndex
        {
            get => common.ActiveTemplateSQLIndex;
            set
            {
                common.ActiveTemplateSQLIndex = value;

                OnPropertyChanged();
            }
        }

        public int ActiveTemplateProgrammingIndex
        {
            get => common.ActiveTemplateProgrammingIndex;
            set
            {
                common.ActiveTemplateProgrammingIndex = value;

                OnPropertyChanged();
            }
        }
        
        private void LoadOption()
        {
            if (fileDialog.Open(ExplorerSelectType.File, ExplorerType.Open, ".xml"))
                SettingsManager.LoadOption(fileDialog.SelectPath);
        }

        private void LoadTemplate()
        {
            if (fileDialog.Open(ExplorerSelectType.File, ExplorerType.Open, ".xml"))
                SettingsManager.LoadTemplate(fileDialog.SelectPath);
        }

        private void LoadPlugin()
        {
            if (fileDialog.Open(ExplorerSelectType.File, ExplorerType.Open, ".txt", ".xml"))
                PluginManager.LoadPlugin(fileDialog.SelectPath);
        }

        public void Save()
            => oldCommon = common;

        public void Accept()
        {
            Info.ActiveOptionIndex = ActiveOptionIndex;
            Info.ActiveTemplateSQLIndex = ActiveTemplateSQLIndex;
            Info.ActiveTemplateProgrammingIndex = ActiveTemplateProgrammingIndex;
        }

        public void Cancel()
        {
            ActiveOptionIndex = oldCommon.ActiveOptionIndex;
            ActiveTemplateSQLIndex = oldCommon.ActiveTemplateSQLIndex;
            ActiveTemplateSQLIndex = oldCommon.ActiveTemplateSQLIndex;
        }

        public CommonM GetCommon()
            => common;
    }
}
