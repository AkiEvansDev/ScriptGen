using BaseMVVM.Command;

using MaterialDesignThemes.Wpf;

namespace ScriptGen.Model
{
    /// <summary>
    /// Модель сообщения
    /// </summary>
    public struct MessageM
    {
        public bool IsVisible { get; }

        public string Data { get; }
        public PackIconKind Kind { get; }

        public string ActionTitle { get; }
        public SimpleCommand ActionCommand { get; }

        public MessageM(string message, PackIconKind kind, string actionTitle = null, SimpleCommand action = null)
        {
            IsVisible = action != null;

            Data = message;
            Kind = kind;

            ActionTitle = actionTitle;
            ActionCommand = action;
        }
    }
}
