using System;
using System.Windows;

namespace ScriptGen.Interface
{
    /// <summary>
    /// Движение элемента
    /// </summary>
    public interface IMove
    {
        /// <summary>
        /// Событие изменения ширины элемента
        /// </summary>
        event Action<double, IMove> OnWidthChanged;
        /// <summary>
        /// Событие изменения высоты элемента
        /// </summary>
        event Action<double, IMove> OnHeightChanged;
        /// <summary>
        /// Событие изменения положения элемента
        /// </summary>
        event Action<Thickness, IMove> OnMarginChanged;

        /// <summary>
        /// true/false можно ли изменить ширину
        /// </summary>
        Func<double, IMove, bool> CanWidthChange { get; set; }
        /// <summary>
        /// true/false можно ли изменить высоту
        /// </summary>
        Func<double, IMove, bool> CanHeightChange { get; set; }

        /// <summary>
        /// Функция, определяющая можно ли изменить положение элемента по горизонтали
        /// </summary>
        Func<double, IMove, bool> CanHorizontalChange { get; set; }
        /// <summary>
        /// Функция, определяющая можно ли изменить положение элемента по вертикали
        /// </summary>
        Func<double, IMove, bool> CanVerticalChange { get; set; }

        /// <summary>
        /// Ширина элемента
        /// </summary>
        double Width { get; set; }
        /// <summary>
        /// Высота элемента
        /// </summary>
        double Height { get; set; }
        /// <summary>
        /// Положение элемента
        /// </summary>
        Thickness Margin { get; set; }
    }
}
