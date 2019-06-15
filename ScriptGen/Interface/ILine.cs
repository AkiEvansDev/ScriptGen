using System;

namespace ScriptGen.Interface
{
    /// <summary>
    /// Линия связи между таблицами
    /// </summary>
    public interface ILine
    {
        /// <summary>
        /// Вызывается при удалении линии
        /// </summary>
        event Action<ILine> OnRemoveLine;

        /// <summary>
        /// Координата X для начальной точки линии
        /// </summary>
        double SourceX { get; set; }
        /// <summary>
        /// Координата X для конечной точки линии
        /// </summary>
        double TargetX { get; set; }
        /// <summary>
        /// Координата Y для начальной точки линии
        /// </summary>
        double SourceY { get; set; }
        /// <summary>
        /// Координата Y для конечной точки линии
        /// </summary>
        double TargetY { get; set; }

        /// <summary>
        /// Координата X для разделительной линии
        /// </summary>
        double ConnectionX { get; set; }

        /// <summary>
        /// Таблица, являющаяся источником
        /// </summary>
        ITable Source { get; }
        /// <summary>
        /// Таблица, являющаяся целью
        /// </summary>
        ITable Target { get; }

        /// <summary>
        /// Поле FK, с которым связанна линия
        /// </summary>
        IField Field { get; }
    }
}
