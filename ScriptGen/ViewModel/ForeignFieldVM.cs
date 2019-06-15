using System.ComponentModel;

using BaseMVVM.Command;

using ScriptGen.Abstraction;
using ScriptGen.Common.SaveModel;
using ScriptGen.Interface;

using ScriptGenPlugin.Model;

namespace ScriptGen.ViewModel
{
    /// <summary>
    /// Работа с внешним ключом
    /// </summary>
    public class ForeignFieldVM : FieldBase
    {
        /// <summary>
        /// Создаёт новый внешний ключ таблицы
        /// </summary>
        public ForeignFieldVM()
        {
            Type = FieldType.FK;

            NameEnter = new SimpleCommand(() => NextField?.Invoke());

            Loaded = new SimpleCommand(SetFocus);
        }

        /// <summary>
        /// Загружает внешний ключ таблицы
        /// </summary>
        /// <param name="fieldSave">Сохранённые данные</param>
        public ForeignFieldVM(FieldSave fieldSave) : this() 
            => Load(fieldSave);

        /// <summary>
        /// Связывает поле с таблице (возвращает объект линии связи)
        /// </summary>
        /// <param name="source">Источник</param>
        /// <param name="target">Цель</param>
        /// <returns>Линия связи</returns>
        public ILine AddRefTable(ITable source, ITable target)
        {
            RefTable = source;
            DataType = $"FK({RefTable.Name})";
            ProgrammingType = $"FK({RefTable.Name})";

            RefTable.PropertyChanged += RefTablePropertyChanged;

            return new ForeignFieldLineVM(source, target, this);
        }

        /// <summary>
        /// Загружает связь поля с таблице (возвращает объект линии связи)
        /// </summary>
        /// <param name="source">Источник</param>
        /// <param name="target">Цель</param>
        /// <param name="lineSave">Сохраненные данные</param>
        /// <returns>Линия связи</returns>
        public ILine AddRefTable(ITable source, ITable target, LineSave lineSave)
        {
            ILine line = AddRefTable(source, target);

            line.SourceX = lineSave.SourceX1;
            line.SourceY = lineSave.SourceY1;
            line.TargetX = lineSave.TargetX1;
            line.TargetY = lineSave.TargetY1;

            line.ConnectionX = lineSave.ConnectionX1;

            return line;
        }

        /// <summary>
        /// Отслеживание изменения имени цели
        /// </summary>
        private void RefTablePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name")
            {
                DataType = $"FK({RefTable.Name})";
                ProgrammingType = $"FK({RefTable.Name})";
            }
        }
    }
}
