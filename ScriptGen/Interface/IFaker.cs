namespace ScriptGen.Interface
{
    /// <summary>
    /// Интерфейс для генератора случайных значений
    /// </summary>
    public interface IFaker
    {
        /// <summary>
        /// Генерирует count случайных значений
        /// </summary>
        /// <param name="count">Количество</param>
        /// <param name="language">Язык</param>
        /// <returns>Случайные значения</returns>
        string Next(int count, char language);
        /// <summary>
        /// Генерирует от minCount до maxCount случайных значений
        /// </summary>
        /// <param name="minCount">Минимальное количество слов</param>
        /// <param name="maxCount">Максимальное количество слов</param>
        /// <param name="language">Язык</param>
        /// <returns>Случайные слов</returns>
        string Next(int minCount, int maxCount, char language);
        /// <summary>
        /// Возвращает случайное значение из представленных
        /// </summary>
        /// <param name="values">Возможные значения</param>
        /// <returns>Случайное слово</returns>
        string Next(string[] values);
    }
}
