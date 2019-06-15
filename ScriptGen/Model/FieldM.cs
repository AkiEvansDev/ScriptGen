using ScriptGen.Common;
using ScriptGen.Interface;

using ScriptGenPlugin.Model;

namespace ScriptGen.Model
{
    /// <summary>
    /// Модель поля таблицы
    /// </summary>
    public struct FieldM
    {
        public ElementM Element;

        public FieldType Type;

        public string DataType;
        public string ProgrammingType;

        public bool IsNull;
        public bool IsUnique;

        public ITable RefTable;

        public string GetError()
        {
            string message = Verification.ConcatString(Element.GetError(), GetDataTypeError());
            return Verification.ConcatString(message, GetProgrammingTypeError());
        }

        private string GetDataTypeError()
            => Type == FieldType.FK
            ? null
            : string.IsNullOrWhiteSpace(DataType)
                ? "Тип данных не может быть пустым!"
                : !Verification.IsOnlyEnLetterOrDigitOrAllowedChar(DataType, '(', ')') || Verification.IsFalseWord(DataType)
                    ? "Тип данных содержит недопустимое слово или символ!"
                    : null;

        private string GetProgrammingTypeError()
            => Type == FieldType.FK
            ? null
            : !string.IsNullOrWhiteSpace(ProgrammingType) && (!Verification.IsOnlyEnLetter(ProgrammingType) || Verification.IsFalseWord(ProgrammingType))
                ? "Тип данных для языков программирования содержит недопустимое слово или символ!"
                : null;
    }
}
