using System.Linq;

using BaseMVVM.Command;

using ScriptGen.Abstraction;
using ScriptGen.Common;
using ScriptGen.Common.SaveModel;
using ScriptGen.Interface;

using ScriptGenPlugin.Model;

namespace ScriptGen.ViewModel
{
    /// <summary>
    /// Работа с таблицей
    /// </summary>
    public class TableVM : TableBase
    {
        /// <summary>
        /// Создаёт новую таблицу
        /// </summary>
        /// <param name="startField">Начальное поле</param>
        public TableVM(IField startField)
        {
            startField.PrevField = SetFocus;
            AddField(startField);

            NameDown = NameEnter = new SimpleCommand(Fields[0].SetFocus);
        }

        // <summary>
        /// Загружает таблицу
        /// </summary>
        /// <param name="startField">Сохранённые данные</param>
        public TableVM(TableSave tableSave) : this(new PrimaryFieldVM(tableSave.Fields[0]))
        {
            Load(tableSave);

            FieldSave[] fields = tableSave.Fields.Where((f) => f.Type == FieldType.Any).ToArray();

            for (int i = 0; i < fields.Length; i++)
                AddField(new FieldVM(fields[i]));
        }

        /// <summary>
        /// Тип модели (физическая/логическая/программирование)
        /// </summary>
        public override ModelType TypeModel
        {
            get => base.TypeModel;
            set
            {
                base.TypeModel = value;
                
                for (int i = 0; i < Fields.Count; i++)
                    Fields[i].TypeModel = value;
            }
        }
    
        /// <summary>
        /// Обновляет команды полей
        /// </summary>
        private void RefreshFieldCommand()
        {
            int i;
            for (i = 0; i < Fields.Count - 1; i++)
            {
                Fields[i].NextField = Fields[i + 1].SetFocus;
                Fields[i + 1].PrevField = Fields[i].SetFocus;
            }
            Fields[i].NextField = () => AddField(GetNewField?.Invoke());

            Height = 50 + 42 * Fields.Count;
        }

        /// <summary>
        /// Сортирует поля таблицы
        /// </summary>
        private void LastFieldSort()
        {
            int index = Fields.Count(f => f.Type != FieldType.Any) - 1;

            if (index < Fields.Count && index > 0)
                Fields.Move(Fields.Count - 1, index);
        }

        /// <summary>
        /// Добавляет поле
        /// </summary>
        /// <param name="field">Поле</param>
        public override void AddField(IField field)
        {
            if (field != null)
            {
                base.AddField(field);

                if (field.Type != FieldType.Any)
                    LastFieldSort();
                RefreshFieldCommand();
            }
        }

        /// <summary>
        /// Удаляет поле
        /// </summary>
        /// <param name="field">Поле</param>
        public override void RemoveField(IField field)
        {
            if (field != null)
            {
                base.RemoveField(field);

                RefreshFieldCommand();
            }
        }
    }
}
