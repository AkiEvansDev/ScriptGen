using BaseMVVM.Command;

using ScriptGen.Abstraction;
using ScriptGen.Common;
using ScriptGen.Common.SaveModel;

using ScriptGenPlugin.Model;

namespace ScriptGen.ViewModel
{
    /// <summary>
    /// Работа с полями таблицы
    /// </summary>
    public class FieldVM : FieldBase
    {
        /// <summary>
        /// Создаёт новое поле таблицы
        /// </summary>
        public FieldVM()
        {
            Type = FieldType.Any;

            Loaded = new SimpleCommand(SetFocus);

            Info.OnSettingsChange += () =>
            {
                OnPropertyChanged("DataTypeSource");
                OnPropertyChanged("ProgrammingSource");
            };
        }

        /// <summary>
        /// Загружает поле таблицы
        /// </summary>
        /// <param name="fieldSave">Сохранённые данные</param>
        public FieldVM(FieldSave fieldSave) : this()
        {
            Load(fieldSave);

            DataType = fieldSave.DataType;
            ProgrammingType = fieldSave.ProgrammingType;
        }

        /// <summary>
        /// Список SQL типов данных
        /// </summary>
        public override string[] DataTypeSource
            => Info.SQLTypes;

        /// <summary>
        /// Список типов данных для языков программирования
        /// </summary>
        public override string[] ProgrammingTypeSource
            => Info.ProgrammingTypes;
    }
}

