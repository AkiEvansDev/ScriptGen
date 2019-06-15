using System.Collections.ObjectModel;

using ScriptGen.Common;
using ScriptGen.Interface;

namespace ScriptGen.Model
{
    /// <summary>
    /// Модель приложения
    /// </summary>
    public class ScriptGenM
    {
        public ModelType ModelType;

        public IPage SelectPage;

        public ObservableCollection<IPage> Pages
            = new ObservableCollection<IPage>();
    }
}