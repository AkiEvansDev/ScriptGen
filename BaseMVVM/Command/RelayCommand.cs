using System;
using System.Windows.Input;

namespace BaseMVVM.Command
{
    /// <summary>
    /// Команда с параметром object
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Действие команды
        /// </summary>
        private Action<object> action;
        /// <summary>
        /// Функция, определяющая можно ли выполнить действие команды
        /// </summary>
        private Func<object, bool> canExecute;

        /// <summary>
        /// Отслеживает изменения состояния функции canExecute
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Создаёт новую команду
        /// </summary>
        /// <param name="action">Действие</param>
        /// <param name="canExecute">Функция, определяющая можно ли выполнить действие</param>
        public RelayCommand(Action<object> action, Func<object, bool> canExecute = null)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Изменяет действие команды
        /// </summary>
        /// <param name="newAction">Новое действие</param>
        public void NewAction(Action<object> newAction)
            => action = newAction;

        /// <summary>
        /// Изменяет функцию, которая определяет можно ли выполнить действие
        /// </summary>
        /// <param name="newCanExecute">Новая функция, определяющая можно ли выполнить действие</param>
        public void NewCanExecute(Func<object, bool> newCanExecute)
            => canExecute = newCanExecute;

        /// <summary>
        /// Возвращает результат функции canAction
        /// </summary>
        /// <param name="parameter">Параметры</param>
        /// <returns>Можно ли выполнить действие</returns>
        public bool CanExecute(object parameter)
            => canExecute == null || canExecute(parameter);

        /// <summary>
        /// Вызывает действие команды
        /// </summary>
        /// <param name="parameter">Параметры</param>
        public void Execute(object parameter)
            => action(parameter);
    }
}
