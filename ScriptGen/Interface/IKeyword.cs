namespace ScriptGen.Interface
{
    /// <summary>
    /// Ключевого слово для выстроенного языка шаблонизации
    /// </summary>
    public interface IKeyword
    {
        /// <summary>
        /// Выполняет заданное действие для ключевого слова
        /// </summary>
        /// <param name="code">Код</param>
        void Do(string code);
    }
}
