using ScriptGen.Common;

namespace ScriptGen.Model
{
    /// <summary>
    /// Модель элемента
    /// </summary>
    public class ElementM
    {
        public string Name;
        public string LogicalName;
        public string ProgrammingName;

        public bool IsError;
        public string Message = null;
        
        public string GetError()
        {
            string message = Verification.ConcatString(GetNameError(), GetLogicalNameError());

            return Verification.ConcatString(message, GetProgrammingNameError());
        }

        private string GetNameError() 
            => string.IsNullOrWhiteSpace(Name)
                ? "Название не может быть пустым!"
                : !Verification.IsOnlyEnLetterOrDigitOrAllowedChar(Name, '_') || Verification.IsFalseWord(Name)
                    ? "Название содержит недопустимое слово или символ!"
                    : null;

        private string GetLogicalNameError()
            => !string.IsNullOrWhiteSpace(LogicalName) && !Verification.IsOnlyLetterOrAllowedChar(LogicalName, ' ')
                ? "Логическое название содержит недопустимый символ!"
                : null;

        private string GetProgrammingNameError()
            => !string.IsNullOrWhiteSpace(ProgrammingName) && !Verification.IsOnlyEnLetter(ProgrammingName)
                ? "Название для языков программирования содержит недопустимый символ!"
                : null;
    }
}
