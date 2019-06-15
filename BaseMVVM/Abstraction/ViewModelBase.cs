using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BaseMVVM.Abstraction
{
    /// <summary>
    /// Абстрактный класс для VM, реализует интерфейс INotifyPropertyChanged
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Событие изменения свойства
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Вызывает событие изменения свойства
        /// </summary>
        /// <param name="name">Название свойства</param>
        public void OnPropertyChanged([CallerMemberName]string name = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
