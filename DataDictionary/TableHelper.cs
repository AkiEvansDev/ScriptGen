using System.Drawing;

using Xceed.Words.NET;

namespace DataDictionary
{
    /// <summary>
    /// Класс содержащий методы для работы с таблицей word
    /// </summary>
    public static class TableHelper
    {
        /// <summary>
        /// Серый цвет для нечётных строк таблицы
        /// </summary>
        private static readonly Color grayColor = Color.FromArgb(242, 242, 242);
        /// <summary>
        /// Цвет границ таблицы
        /// </summary>
        private static readonly Color borderColor = Color.FromArgb(191, 191, 191);

        /// <summary>
        /// Граница таблицы
        /// </summary>
        private static readonly Border defaultBorder = new Border(BorderStyle.Tcbs_single, BorderSize.two, 0, borderColor);

        /// <summary>
        /// Применяет базовые настройки стиля к таблице
        /// </summary>
        /// <param name="wordTable">Таблица word</param>
        public static void SetTableStyle(ref Table wordTable)
        {
            wordTable.Alignment = Alignment.left;
            wordTable.Design = TableDesign.TableGrid;

            for (int i = 0; i < 6; i++)
                wordTable.SetBorder((TableBorderType)i,  defaultBorder);
        }

        /// <summary>
        /// Заполняет ячейку таблицы
        /// </summary>
        /// <param name="wordTable">Таблица word</param>
        /// <param name="rowIndex">Номер строки</param>
        /// <param name="cellIndex">Номер столбца</param>
        /// <param name="value">Значение</param>
        public static void SetCell(ref Table wordTable, int rowIndex, int cellIndex, string value)
            => SetCell(ref wordTable, rowIndex, cellIndex, value, 12, Alignment.left, false);

        /// <summary>
        /// Заполняет ячейку таблицы и применяет к ней стиль
        /// </summary>
        /// <param name="wordTable">Таблица word</param>
        /// <param name="rowIndex">Номер строки</param>
        /// <param name="cellIndex">Номер столбца</param>
        /// <param name="value">Значение</param>
        /// <param name="fontSize">Размер шрифта</param>
        public static void SetCell(ref Table wordTable, int rowIndex, int cellIndex, string value, int fontSize)
            => SetCell(ref wordTable, rowIndex, cellIndex, value, fontSize, Alignment.left, false);

        /// <summary>
        /// Заполняет ячейку таблицы и применяет к ней стиль
        /// </summary>
        /// <param name="wordTable">Таблица word</param>
        /// <param name="rowIndex">Номер строки</param>
        /// <param name="cellIndex">Номер столбца</param>
        /// <param name="value">Значение</param>
        /// <param name="alignment">Выравнивание</param>
        public static void SetCell(ref Table wordTable, int rowIndex, int cellIndex, string value, Alignment alignment)
            => SetCell(ref wordTable, rowIndex, cellIndex, value, 12, alignment, false);

        /// <summary>
        /// Заполняет ячейку таблицы и применяет к ней стиль
        /// </summary>
        /// <param name="wordTable">Таблица word</param>
        /// <param name="rowIndex">Номер строки</param>
        /// <param name="cellIndex">Номер столбца</param>
        /// <param name="value">Значение</param>
        /// <param name="isBold">Выделить ли значение жирным</param>
        public static void SetCell(ref Table wordTable, int rowIndex, int cellIndex, string value, bool isBold)
            => SetCell(ref wordTable, rowIndex, cellIndex, value, 12, Alignment.left, isBold);

        /// <summary>
        /// Заполняет ячейку таблицы и применяет к ней стиль
        /// </summary>
        /// <param name="wordTable">Таблица word</param>
        /// <param name="rowIndex">Номер строки</param>
        /// <param name="cellIndex">Номер столбца</param>
        /// <param name="value">Значение</param>
        /// <param name="fontSize">Размер шрифта</param>
        /// <param name="alignment">Выравнивание</param>
        public static void SetCell(ref Table wordTable, int rowIndex, int cellIndex, string value, int fontSize, Alignment alignment)
            => SetCell(ref wordTable, rowIndex, cellIndex, value, fontSize, alignment, false);

        /// <summary>
        /// Заполняет ячейку таблицы и применяет к ней стиль
        /// </summary>
        /// <param name="wordTable">Таблица word</param>
        /// <param name="rowIndex">Номер строки</param>
        /// <param name="cellIndex">Номер столбца</param>
        /// <param name="value">Значение</param>
        /// <param name="fontSize">Размер шрифта</param>
        /// <param name="isBold">Выделить ли значение жирным</param>
        public static void SetCell(ref Table wordTable, int rowIndex, int cellIndex, string value, int fontSize, bool isBold)
            => SetCell(ref wordTable, rowIndex, cellIndex, value, fontSize, Alignment.left, isBold);

        /// <summary>
        /// Заполняет ячейку таблицы и применяет к ней стиль
        /// </summary>
        /// <param name="wordTable">Таблица word</param>
        /// <param name="rowIndex">Номер строки</param>
        /// <param name="cellIndex">Номер столбца</param>
        /// <param name="value">Значение</param>
        /// <param name="fontSize">Размер шрифта</param>
        /// <param name="alignment">Выравнивание</param>
        /// <param name="isBold">Выделить ли значение жирным</param>
        public static void SetCell(ref Table wordTable, int rowIndex, int cellIndex, string value,
            int fontSize, Alignment alignment, bool isBold)
        {
            wordTable.Rows[rowIndex].Cells[cellIndex].VerticalAlignment = VerticalAlignment.Top;

            if (rowIndex % 2 != 0)
                wordTable.Rows[rowIndex].Cells[cellIndex].FillColor = grayColor;

            wordTable.Rows[rowIndex].Cells[cellIndex].Paragraphs[0].Append(value)
                .FontSize(fontSize)
                .Alignment = alignment;

            if (isBold)
                wordTable.Rows[rowIndex].Cells[cellIndex].Paragraphs[0].Bold();
        }
    }
}
