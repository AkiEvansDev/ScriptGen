namespace ScriptGen.Interface
{
    /// <summary>
    /// Управление ошибкой элемента
    /// </summary>
    public interface IError
    {
        /// <summary>
        /// true/false существует ли ошибка у элемента
        /// </summary>
        bool IsError { get; set; }
        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// Устанавливает ошибку с переданным сообщением
        /// </summary>
        /// <param name="message">Сообщение</param>
        void SetError(string message = null);
    }
}
