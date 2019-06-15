using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

using ScriptGen.Abstraction;
using ScriptGen.Interface;

namespace ScriptGen.Model
{
    /// <summary>
    /// Модель таблицы
    /// </summary>
    public class TableM : UniqueErrorBase
    {
        public int Id;

        public ElementM Element;

        public ObservableCollection<IField> Fields
            = new ObservableCollection<IField>();

        public double Width;
        public double Height;
        public Thickness Margin;

        public void SetFieldError()
        {
            string message = null;

            IEnumerable<string> names = Fields.Select(x => x.Name);
            IEnumerable<string> logicalNames = Fields.Select(x => x.LogicalName);
            IEnumerable<string> programmingNames = Fields.Select(x => x.ProgrammingName);

            for (int i = 0; i < Fields.Count; i++)
            {
                SetError(ref message, GetUniqueError(names, i, "Название должно быть уникальным!"));
                SetError(ref message, GetUniqueError(logicalNames, i, "Логическое название должно быть уникальным!"));
                SetError(ref message, GetUniqueError(programmingNames, i, "Название для языков программирования должно быть уникальным!"));

                Fields[i].SetError(message);
                message = null;
            }
        }
    }
}
