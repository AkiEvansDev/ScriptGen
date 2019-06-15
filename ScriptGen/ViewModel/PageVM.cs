using System.Linq;
using System.Windows;

using ScriptGen.Abstraction;
using ScriptGen.Common;
using ScriptGen.Common.SaveModel;
using ScriptGen.Interface;

namespace ScriptGen.ViewModel
{
    /// <summary>
    /// Работа с страницей приложения
    /// </summary>
    public class PageVM : PageBase
    {
        /// <summary>
        /// Тип модели
        /// </summary>
        private ModelType modelType;

        /// <summary>
        /// Создаёт новую страницу 
        /// </summary>
        public PageVM() 
            => Info.OnSettingsChange += () =>
                {
                    OnPropertyChanged("Width");
                    OnPropertyChanged("Height");
                };

        /// <summary>
        /// Выделенная таблица
        /// </summary>
        public override ITable SelectTable
        {
            get => base.SelectTable;
            set
            {
                if (SelectTable != null && SelectTable.Equals(value))
                    value = null;

                if (SelectTable == null || value == null)
                {
                    if (value != null)
                        value.IsSelect = true;
                    else if (SelectTable != null)
                        SelectTable.IsSelect = false;

                    base.SelectTable = value;
                }
                else if (value != null)
                    AddReference(value);
            }
        }

        /// <summary>
        /// Тип модели
        /// </summary>
        public override ModelType TypeModel
        {
            get => modelType;
            set
            {
                modelType = value;

                for (int i = 0; i < Tables.Count; i++)
                    Tables[i].TypeModel = value;
            }
        }

        /// <summary>
        /// Добавляет таблицу
        /// </summary>
        /// <param name="table">Таблица</param>
        public override void AddTable(ITable table)
        {
            if (table != null)
            {
                table.Margin = new Thickness(ScrollHorizontal + 50, ScrollVertical + 50, 0, 0);
                table.TypeModel = TypeModel;

                table.GetNewField = () => new FieldVM() { TypeModel = TypeModel };

                SetTableMove(table);

                base.AddTable(table);
            }
        }

        /// <summary>
        /// Загружает таблицу
        /// </summary>
        /// <param name="table">Таблица</param>
        private void LoadTable(ITable table)
        {
            table.TypeModel = TypeModel;

            table.GetNewField = () => new FieldVM() { TypeModel = TypeModel };
            SetTableMove(table);

            base.AddTable(table);
        }

        /// <summary>
        /// Загружает данные страницы
        /// </summary>
        /// <param name="dataBaseSave">Сохранённая информация</param>
        public override void Load(DataBaseSave dataBaseSave)
        {
            base.Load(dataBaseSave);

            for (int i = 0; i < dataBaseSave.Tables.Length; i++)
                LoadTable(new TableVM(dataBaseSave.Tables[i]));

            for (int i = 0; i < dataBaseSave.Lines.Length; i++)
                AddReference(dataBaseSave.Lines[i]);
        }

        /// <summary>
        /// Устанавливает ограничения перемещения таблицы
        /// </summary>
        /// <param name="table">Таблица</param>
        private void SetTableMove(ITable table)
        {
            table.CanWidthChange = (w, _) => w >= 300 && w <= 500;

            table.CanHorizontalChange = (l, o) => l >= 10 && l + o.Width <= 2790;
            table.CanVerticalChange = (t, o) => t >= 10 && t + o.Height <= 1565;

            table.OnMarginChanged += OnTableMove;
        }

        /// <summary>
        /// Происходит при движении таблицы
        /// </summary>
        /// <param name="margin">Отступы таблицы</param>
        /// <param name="obj">Таблица</param>
        private void OnTableMove(Thickness margin, IMove obj)
        {
            if (margin.Left + obj.Width > ScrollHorizontal + ActualWidth)
                ScrollHorizontal += 5;
            else if (margin.Left < ScrollHorizontal)
                ScrollHorizontal -= 5;
            
            if (margin.Top + obj.Height > ScrollVertical + ActualHeight)
                ScrollVertical += 5;
            else if (margin.Top < ScrollVertical)
                ScrollVertical -= 5;
        }

        /// <summary>
        /// Связывает таблицу с другой
        /// </summary>
        /// <param name="target">Цель</param>
        private void AddReference(ITable target)
        {
            ForeignFieldVM field = new ForeignFieldVM()
            {
                TypeModel = TypeModel
            };

            AddLine(field.AddRefTable(SelectTable, target));
            target.AddField(field);
            
            SelectTable = null;
        }

        /// <summary>
        /// Загружает связь
        /// </summary>
        /// <param name="lineSave">Сохранённые данные</param>
        private void AddReference(LineSave lineSave)
        {
            ForeignFieldVM field = new ForeignFieldVM(lineSave.Field);

            ITable source = Tables.FirstOrDefault(t => t.Id == lineSave.SourceId);
            ITable target = Tables.FirstOrDefault(t => t.Id == lineSave.TargetId);

            AddLine(field.AddRefTable(source, target, lineSave));
            target.AddField(field);
        }

        /// <summary>
        /// Добавляет линию
        /// </summary>
        /// <param name="line">Линия</param>
        private void AddLine(ILine line)
        {
            line.OnRemoveLine += (l) => Lines.Remove(l);

            Lines.Add(line);
        }
    }
}
