using System;

using BaseMVVM.Abstraction;

using ScriptGen.Interface;

namespace ScriptGen.Abstraction
{
    /// <summary>
    /// Базовый класс линия связи между таблицами
    /// </summary>
    public abstract class LineBase : ViewModelBase, ILine
    {
        /// <summary>
        /// Вызывается при удалении линии
        /// </summary>
        public event Action<ILine> OnRemoveLine;

        /// <summary>
        /// Координата X для начальной точки линии
        /// </summary>
        private double sourceX;
        /// <summary>
        /// Координата X для конечной точки линии
        /// </summary>
        private double targetX;
        /// <summary>
        /// Координата Y для начальной точки линии
        /// </summary>
        private double sourceY;
        /// <summary>
        /// Координата Y для конечной точки линии
        /// </summary>
        private double targetY;

        /// <summary>
        /// Координата X для разделительной линии
        /// </summary>
        private double connectionX;

        /// <summary>
        /// Создаёт объект базовой линии связи
        /// </summary>
        /// <param name="source">Исходная таблица</param>
        /// <param name="target">Конечная таблица</param>
        public LineBase(ITable source, ITable target)
        {
            Source = source;
            Target = target;
        }

        /// <summary>
        /// Координата X для начальной точки линии
        /// </summary>
        public virtual double SourceX
        {
            get => sourceX;
            set
            {
                sourceX = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Координата X для конечной точки линии
        /// </summary>
        public virtual double TargetX
        {
            get => targetX;
            set
            {
                targetX = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Координата Y для начальной точки линии
        /// </summary>
        public virtual double SourceY
        {
            get => sourceY;
            set
            {
                sourceY = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Координата Y для конечной точки линии
        /// </summary>
        public virtual double TargetY
        {
            get => targetY;
            set
            {
                targetY = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Координата X для разделительной линии
        /// </summary>
        public virtual double ConnectionX
        {
            get => connectionX;
            set
            {
                connectionX = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Таблица, являющаяся источником
        /// </summary>
        public ITable Source { get; }
        /// <summary>
        /// Таблица, являющаяся целью
        /// </summary>
        public ITable Target { get; }

        /// <summary>
        /// Поле FK, с которым связанна линия
        /// </summary>
        public virtual IField Field { get; }

        /// <summary>
        /// Вызывается для удаления линии связи
        /// </summary>
        /// <param name="line">Линия связи</param>
        protected void RemoveLine(ILine line)
            => OnRemoveLine?.Invoke(line);
    }
}
