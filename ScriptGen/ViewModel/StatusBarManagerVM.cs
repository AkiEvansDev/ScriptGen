using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using BaseMVVM.Command;

using Explorer;

using MaterialDesignThemes.Wpf;

using ScriptGen.Common;
using ScriptGen.Model;

namespace ScriptGen.ViewModel
{
    /// <summary>
    /// Осуществляет вывод сообщений
    /// </summary>
    public class StatusBarManagerVM
    {
        public static PackIconKind ErrorIcon
            = PackIconKind.CloseBox;

        public static PackIconKind InfoIcon
            = PackIconKind.WarningBox;

        public static PackIconKind MessageIcon
            = PackIconKind.CheckboxMarked;

        private static string data;

        private static PackIconKind kind;

        public static SimpleCommand ClearHistory { get; }
            = new SimpleCommand(Clear, () => History.Count > 0);

        public static ObservableCollection<MessageM> History { get; }
            = new ObservableCollection<MessageM>();

        public static string Data
        {
            get => data;
            private set
            {
                data = value;

                OnStaticPropertyChanged();
            }
        }

        public static PackIconKind Kind
        {
            get => kind;
            private set
            {
                kind = value;

                OnStaticPropertyChanged();
            }
        }
        
        private static void SetOption(string message, string option, PackIconKind kind)
        {
            Data = message;
            Kind = kind;

            message = Verification.ConcatString(message, option);

            History.Add(new MessageM(message, kind));
        }

        private static void SetOption(string message, string option, PackIconKind kind, string actionTitle, SimpleCommand action)
        {
            Data = message;
            Kind = kind;

            message = Verification.ConcatString(message, option);

            History.Add(new MessageM(message, kind, actionTitle, action));
        }

        private static void SetOption(string message, PackIconKind kind)
            => SetOption(message, null, kind);

        private static void SetOption(string message, PackIconKind kind, string actionTitle, SimpleCommand action)
            => SetOption(message, null, kind, actionTitle, action);

        public static void Error(string message)
            => SetOption(message, ErrorIcon);

        public static void Error(string message, string option)
            => SetOption(message, option, ErrorIcon);

        public static void Error(string message, string actionTitle, SimpleCommand action)
            => SetOption(message, ErrorIcon, actionTitle, action);

        public static void Error(string message, string option, string actionTitle, SimpleCommand action)
            => SetOption(message, option, ErrorIcon, actionTitle, action);

        public static void Info(string message)
            => SetOption(message, InfoIcon);

        public static void Info(string message, string option)
            => SetOption(message, option, InfoIcon);

        public static void Info(string message, string actionTitle, SimpleCommand action)
            => SetOption(message, InfoIcon, actionTitle, action);

        public static void Info(string message, string option, string actionTitle, SimpleCommand action)
            => SetOption(message, option, InfoIcon, actionTitle, action);

        public static void Message(string message)
            => SetOption(message, MessageIcon);

        public static void Message(string message, string option)
            => SetOption(message, option, MessageIcon);

        public static void Message(string message, string actionTitle, SimpleCommand action)
            => SetOption(message, MessageIcon, actionTitle, action);

        public static void Message(string message, string option, string actionTitle, SimpleCommand action)
            => SetOption(message, option, MessageIcon, actionTitle, action);

        public static void ExplorerError(ExplorerException e)
        {
            if (!string.IsNullOrEmpty(e.Message) && !string.IsNullOrEmpty(e.Option) && e.Action != null)
                Error(e.Message, e.Option, e.ActionTitle, new SimpleCommand(e.Action));
            else if (!string.IsNullOrEmpty(e.Message) && !string.IsNullOrEmpty(e.Option))
                Error(e.Message, e.Option);
        }

        public static void ExplorerInfo(string message, string option)
        {
            if (!string.IsNullOrEmpty(message) && !string.IsNullOrEmpty(option))
                Info(message, option);
            else if (!string.IsNullOrEmpty(message))
                Info(message);
        }

        private static void Clear()
        {
            History.Clear();

            Message("История очищена!");
        }

        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;

        private static void OnStaticPropertyChanged([CallerMemberName] string name = "")
            => StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
    }
}
