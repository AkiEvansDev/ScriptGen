using System.Windows;

using BaseMVVM.Command;

using Explorer;

using ScriptGen.API;
using ScriptGen.Faker.ViewModel;
using ScriptGen.View;
using ScriptGen.ViewModel;

namespace ScriptGen
{
    public partial class App : Application
    {
        private void ScriptGenStart(object sender, StartupEventArgs e)
        {
            Load();

             ScriptGenV window = new ScriptGenV
             {
                 DataContext = new ScriptGenVM()
             };

             window.Closing += (o, arg) => OnClose();

             window.Show();
        }
        
        private void Load()
        {
            FileDialog.OnInfo += (m, o) => StatusBarManagerVM.Info(m, o);
            FileDialog.OnError += (e) =>
                StatusBarManagerVM.Error(e.Message, e.Option, e.ActionTitle, new SimpleCommand(e.Action));

            PluginManager.LoadPlugins();
        }

        private void OnClose()
        {
            PluginManager.ClosePlugins();
            FakerVM.SaveDefaultTemplates();

            Current.Shutdown();
        }
    }
}
