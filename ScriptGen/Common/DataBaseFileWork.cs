using System.IO;

using BaseMVVM.Command;

using ScriptGen.Common.SaveModel;
using ScriptGen.ViewModel;

namespace ScriptGen.Common
{
    /// <summary>
    /// Осуществляет работу с моделями базы данных
    /// </summary>
    public static class DataBaseFileWork
    {
        public static void Save(DataBaseSave dataBaseSave, string path)
        {
            if (!File.Exists(path) && Directory.Exists(path))
                path = string.IsNullOrWhiteSpace(dataBaseSave.Name)
                    ? FileWork.GetUniqueFileName(path, "dbSave", ".xml")
                    : FileWork.GetUniqueFileName(path, dataBaseSave.Name, ".xml");

            if (FileWork.XmlSave(dataBaseSave, path, typeof(DataBaseSave), out string message))
            {
                StatusBarManagerVM.Message($"Модель \"{dataBaseSave.Name}\" сохранена!", "перейти",
                    new SimpleCommand(() => FileWork.OpenInExplorer(path)));
            }
            else
                StatusBarManagerVM.Error($"Не удалось сохранить модель!", message);
        }

        public static bool Load(string path, out DataBaseSave dataBase)
        {
            dataBase = new DataBaseSave();

            if (FileWork.XmlLoad(path, typeof(DataBaseSave), out string message, out object db))
            {
                dataBase = (DataBaseSave)db;
                return true;
            }
            else
            {
                StatusBarManagerVM.Error($"Не удалось загрузить модель!", message, "перейти",
                    new SimpleCommand(() => FileWork.OpenInExplorer(path)));
            }

            return false;
        }
    }
}
