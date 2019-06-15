using ScriptGen.Abstraction;
using ScriptGen.Common;
using ScriptGen.Common.SaveModel;

using ScriptGenPlugin.Model;

namespace ScriptGen.ViewModel
{
    /// <summary>
    /// Работа с первичным ключом
    /// </summary>
    public class PrimaryFieldVM : FieldBase
    {
        /// <summary>
        /// Создаёт первичный ключ таблицы
        /// </summary>
        public PrimaryFieldVM()
        {
            Type = FieldType.PK;
            IsUnique = true;
        }

        /// <summary>
        /// Загружает первичный ключ таблицы
        /// </summary>
        /// <param name="fieldSave">Сохранённые данные</param>
        public PrimaryFieldVM(FieldSave fieldSave) : this()
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
