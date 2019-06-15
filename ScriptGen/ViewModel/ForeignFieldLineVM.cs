using System.Windows;

using ScriptGen.Abstraction;
using ScriptGen.Interface;

namespace ScriptGen.ViewModel
{
    /// <summary>
    /// Работа с соединительной линией
    /// </summary>
    public class ForeignFieldLineVM : LineBase
    {
        /// <summary>
        /// Таблица, являющаяся источником
        /// </summary>
        private ITable source;
        /// <summary>
        /// Таблица, являющаяся целью
        /// </summary>
        private ITable target;

        /// <summary>
        /// Поле FK, с которым связанна линия
        /// </summary>
        private readonly IField field;

        /// <summary>
        /// Создаёт новую линию связи
        /// </summary>
        /// <param name="source">Таблица, являющаяся источником</param>
        /// <param name="target">Таблица, являющаяся целью</param>
        /// <param name="field">Поле FK, с которым связанна линия</param>
        public ForeignFieldLineVM(ITable source, ITable target, IField field) :
            base(source, target)
        {
            this.source = source;
            this.target = target;

            this.field = field;
            
            source.OnMarginChanged += OnSourceMove;
            target.OnMarginChanged += OnTargetMove;

            source.OnRemoveTable += OnRemoveSource;
            target.OnRemoveTable += OnRemoveTarget;
            field.OnRemoveField += OnRemoveField;

            SourceX = source.Margin.Left + 150;
            SourceY = source.Margin.Top + 10;

            TargetX = target.Margin.Left + 150;
            TargetY = target.Margin.Top + 10;

            ConnectionX = source.Margin.Left + source.Width + (target.Margin.Left - source.Margin.Left - source.Width) / 2; 
        }

        // <summary>
        /// Поле FK, с которым связанна линия
        /// </summary>
        public override IField Field
            => field;

        /// <summary>
        /// Координата Y для начальной точки линии
        /// </summary>
        public override double SourceY
        {
            get => base.SourceY;
            set
            {
                if (value > Source.Margin.Top + 5 && value < Source.Margin.Top + Source.Height - 5)
                    base.SourceY = value;
            }
        }

        /// <summary>
        /// Координата Y для конечной точки линии
        /// </summary>
        public override double TargetY
        {
            get => base.TargetY;
            set
            {
                if (value > Target.Margin.Top + 5 && value < Target.Margin.Top + Target.Height - 5)
                    base.TargetY = value;
            }
        }

        /// <summary>
        /// Происходит при удалении поля
        /// </summary>
        /// <param name="obj">Поле</param>
        private void OnRemoveField(IField obj)
        {
            target.OnRemoveTable -= OnRemoveTarget;
            source.OnRemoveTable -= OnRemoveSource;

            RemoveLine(this);
        }

        /// <summary>
        /// Происходит при удаления цели
        /// </summary>
        /// <param name="obj">Цель</param>
        private void OnRemoveTarget(ITable obj)
        {
            source.OnRemoveTable -= OnRemoveSource;

            RemoveLine(this);
        }

        /// <summary>
        /// Происходит при удаления источника
        /// </summary>
        /// <param name="obj">Источник</param>
        private void OnRemoveSource(ITable obj)
        {
            target.OnRemoveTable -= OnRemoveTarget;
            target.RemoveField(field);

            RemoveLine(this);
        }
        
        /// <summary>
        /// Происходит при перемещении источника
        /// </summary>
        /// <param name="margin">Отступы источника</param>
        /// <param name="obj">Источник</param>
        private void OnSourceMove(Thickness margin, IMove obj)
        {
            SourceX = margin.Left + obj.Width / 2;
            SourceY += margin.Top - obj.Margin.Top;
        }

        /// <summary>
        /// Происходит при перемещении цели
        /// </summary>
        /// <param name="margin">Отступы цели</param>
        /// <param name="obj">Цель</param>
        private void OnTargetMove(Thickness margin, IMove obj)
        {
            TargetX = margin.Left + obj.Width / 2;
            TargetY += margin.Top - obj.Margin.Top;
        }
    }
}
