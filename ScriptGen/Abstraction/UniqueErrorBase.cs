using System.Collections.Generic;

using ScriptGen.Common;

namespace ScriptGen.Abstraction
{
    /// <summary>
    /// Базовый класс для проверки на уникальность
    /// </summary>
    public abstract class UniqueErrorBase
    {
        /// <summary>
        /// Добавляет ошибку к сообщению
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="error">Ошибка</param>
        public virtual void SetError(ref string message, string error)
            => message = Verification.ConcatString(message, error);

        /// <summary>
        /// Проверяет, является ли указанный элемент уникальным для коллекции
        /// </summary>
        /// <param name="data">Коллекция</param>
        /// <param name="index">Индекс элемента для проверки</param>
        /// <param name="message">Сообщение в случае ошибки</param>
        /// <returns>null или сообщение об ошибке</returns>
        public virtual string GetUniqueError(IEnumerable<string> data, int index, string message)
            => Verification.IsUniqueElement(data, index) == false
                ? message
                : null;
    }
}
