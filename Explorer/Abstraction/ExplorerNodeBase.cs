using System;
using System.Collections.ObjectModel;

using BaseMVVM.Abstraction;

using Explorer.Interface;

using MaterialDesignThemes.Wpf;

namespace Explorer.Abstraction
{
    /// <summary>
    /// Абстрактный класс для элементов проводника
    /// </summary>
    internal abstract class ExplorerNodeBase : ViewModelBase, INodeTree
    {
        /// <summary>
        /// Событие выделения элемента проводника
        /// </summary>
        public event Action OnSelected;
        /// <summary>
        /// Событие открытия/закрытия элемента проводника
        /// </summary>
        public event Action<bool> OnExpanded;

        /// <summary>
        /// true/false выбран ли элемент проводника
        /// </summary>
        private bool isSelected;
        /// <summary>
        /// true/false открыт ли элемент проводника
        /// </summary>
        private bool isExpanded;

        /// <summary>
        /// true/false можно ли переименовать элемент проводника
        /// </summary>
        private bool isEditable;

        /// <summary>
        /// Дочерние элементы элемента проводника
        /// </summary>
        private ObservableCollection<INodeTree> childNode;

        /// <summary>
        /// Создаёт новый объект с информацией об элементе проводника
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="fullName">Путь</param>
        public ExplorerNodeBase(string name, string fullName)
        {
            FullPatch = fullName;
            Name = name;
        }

        /// <summary>
        /// Полный путь
        /// </summary>
        public virtual string FullPatch { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// Иконка
        /// </summary>
        public virtual PackIconKind Kind { get; set; }

        /// <summary>
        /// true/false выделен ли элемент проводника
        /// </summary>
        public virtual bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;

                if (value)
                    OnSelected?.Invoke();

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// true/false открыт ли элемент проводника
        /// </summary>
        public virtual bool IsExpanded
        {
            get => isExpanded;
            set
            {
                isExpanded = value;

                OnExpanded?.Invoke(value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// true/false можно ли переименовывать элемент проводника
        /// </summary>
        public virtual bool IsEditable
        {
            get => isEditable;
            set
            {
                isEditable = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Дочерние элементы элемента проводника
        /// </summary>
        public virtual ObservableCollection<INodeTree> ChildNode
        {
            get => childNode;
            set
            {
                childNode = value;

                OnPropertyChanged();
            }
        }
    }
}
