using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using ScriptGen.Abstraction;
using ScriptGen.Interface;

namespace ScriptGen.Model
{
    /// <summary>
    /// Модель базы данных
    /// </summary>
    public class DataBaseM : UniqueErrorBase
    {
        public string Name;

        public bool IsError;

        public ObservableCollection<ITable> Tables
            = new ObservableCollection<ITable>();
        public ObservableCollection<ILine> Lines
            = new ObservableCollection<ILine>();

        public void SetTableError()
        {
            string message = null;

            IEnumerable<string> names = Tables.Select(x => x.Name);
            IEnumerable<string> logicalNames = Tables.Select(x => x.LogicalName);
            IEnumerable<string> linqNames = Tables.Select(x => x.ProgrammingName);

            for (int i = 0; i < Tables.Count; i++)
            {

                SetError(ref message, GetUniqueError(names, i, "Название должно быть уникальным!"));
                SetError(ref message, GetUniqueError(logicalNames, i, "Логическое название должно быть уникальным!"));
                SetError(ref message, GetUniqueError(linqNames, i, "Название для языков программирования должно быть уникальным!"));

                Tables[i].SetError(message);
                message = null;
            }
        }
    }
}
