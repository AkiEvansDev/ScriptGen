using System;
using System.IO;
using System.Linq;

using ScriptGenPlugin.Interface;
using ScriptGenPlugin.Model;

using Xceed.Words.NET;

namespace DataDictionary
{
    /// <summary>
    /// Логика создания словаря данных
    /// </summary>
    public class App : IPlugin
    {
        /// <summary>
        /// API приложения для получения данных
        /// </summary>
        public static IAPI MainAPI;

        /// <summary>
        /// Содержимое кнопки плагина
        /// </summary>
        public object ActionContent => "Создать словарь данных";
        /// <summary>
        /// Подсказка кнопки плагина
        /// </summary>
        public string ActionToolTip => null;

        /// <summary>
        /// Действие кнопки плагина
        /// </summary>
        public Action PluginAction => StartGenerate;
        /// <summary>
        /// Функция, определяющая возможность вызова PluginAction
        /// </summary>
        public Func<bool> CanPluginAction => null;

        /// <summary>
        /// Вызывается при загрузке плагина
        /// </summary>
        /// <param name="api">API приложения</param>
        public void Start(IAPI api)
            => MainAPI = api;

        /// <summary>
        /// Вызывается при закрытии приложения
        /// </summary>
        public void Close() { }

        /// <summary>
        /// Начинает создание словаря данных
        /// </summary>
        private void StartGenerate()
        {
            string path = MainAPI.SaveFile(true, ".docx", ".doc");

            if (path != null)
            {
                try
                {
                    if (Directory.Exists(path) && !File.Exists(path))
                        path = FileWork.GetUniqueFileName(path, "DataDictionary", ".docx");

                    GenerateDataDictionary(MainAPI.GetTableInfo(), path);
                    MainAPI.Message("Словарь данных сохранён!", "перейти", () => FileWork.OpenInExplorer(path));
                }
                catch (Exception e)
                {
                    MainAPI.Error("При создании словаря данных возникла ошибка!", e.Message);
                }
            }
        }

        /// <summary>
        /// Создаёт словарь данных и сохраняет его по указанному пути
        /// </summary>
        /// <param name="tablesInfo">Данные для построения словаря данных</param>
        /// <param name="path">Путь для сохранения</param>
        public void GenerateDataDictionary(TableInfo[] tablesInfo, string path)
        {
            if (tablesInfo.Length != 0)
            {
                DocX document = DocX.Create(path);
                Table wordTable = document.AddTable(tablesInfo.Sum(t => t.Fields.Length + 1) + 1, 6);

                TableHelper.SetTableStyle(ref wordTable);

                AddTableHeader(ref wordTable);

                int rowIndex = 0;
                for (int i = 0; i < tablesInfo.Length; i++)
                {
                    wordTable.Rows[++rowIndex].MergeCells(0, 5);
                    TableHelper.SetCell(ref wordTable, rowIndex, 0, tablesInfo[i].Name, 14, Alignment.center, true);

                    for (int j = 0; j < tablesInfo[i].Fields.Length; j++)
                        AddFieldInfoRow(ref wordTable, tablesInfo[i].Fields[j], ++rowIndex);
                }

                document.InsertParagraph().InsertTableAfterSelf(wordTable);
                
                document.Save();
            }
        }

        /// <summary>
        /// Заполняет заголовок таблицы словаря данных
        /// </summary>
        /// <param name="wordTable">Таблица word</param>
        private void AddTableHeader(ref Table wordTable)
        {
            if (wordTable.RowCount > 0 && wordTable.ColumnCount > 5)
            {
                TableHelper.SetCell(ref wordTable, 0, 0, "Поле", 14, true);
                TableHelper.SetCell(ref wordTable, 0, 1, "Тип данных", 14, Alignment.center, true);
                TableHelper.SetCell(ref wordTable, 0, 2, "Пустое", 14, Alignment.center, true);
                TableHelper.SetCell(ref wordTable, 0, 3, "Уникальное", 14, Alignment.center, true);
                TableHelper.SetCell(ref wordTable, 0, 4, "Ключ", 14, true);
                TableHelper.SetCell(ref wordTable, 0, 5, "Комментарий", 14, true);
            }
        }

        /// <summary>
        /// Заполняет строку таблицы информацией о поле
        /// </summary>
        /// <param name="wordTable">Таблица word</param>
        /// <param name="field">Информация о поле таблицы</param>
        /// <param name="rowIndex">Номер строки для заполнения</param>
        private void AddFieldInfoRow(ref Table wordTable, FieldInfo field, int rowIndex)
        {
            if (wordTable.RowCount >= rowIndex && wordTable.ColumnCount > 5)
            {
                TableHelper.SetCell(ref wordTable, rowIndex, 0, field.Name, true);
                TableHelper.SetCell(ref wordTable, rowIndex, 1, field.DataType, Alignment.center);
                TableHelper.SetCell(ref wordTable, rowIndex, 2, field.IsNull ? "Да" : "Нет", Alignment.center);
                TableHelper.SetCell(ref wordTable, rowIndex, 3, field.IsUnique ? "Да" : "Нет", Alignment.center);
                TableHelper.SetCell(ref wordTable, rowIndex, 4, GetFieldKeyString(field));
                TableHelper.SetCell(ref wordTable, rowIndex, 5, "");
            }
        }

        /// <summary>
        /// Получает строковую информацию о том, является ли поле внешнем или первичным ключом
        /// </summary>
        /// <param name="field">Информация о поле таблицы</param>
        /// <returns>Информация</returns>
        private string GetFieldKeyString(FieldInfo field)
        {
            if (field.Type == FieldType.PK)
                return "Первичный ключ (PK)";
            else if (field.Type == FieldType.FK)
                return $"Внешний ключ на таблицу {field.RefTable.Name} (FK)";

            return "Нет";
        }
    }
}
