using System;
using System.Collections.ObjectModel;

namespace Explorer.Interface
{
    /// <summary>
    /// Элементов проводника
    /// </summary>
    internal interface INodeTree
    {
        /// <summary>
        /// Событие выделения элемента проводника
        /// </summary>
        event Action OnSelected;
        /// <summary>
        /// Событие открытия/закрытия элемента проводника
        /// </summary>
        event Action<bool> OnExpanded;

        /// <summary>
        /// true/false выделен ли элемент проводника
        /// </summary>
        bool IsSelected { get; set; }
        /// <summary>
        /// true/false открыт ли элемент проводника
        /// </summary>
        bool IsExpanded { get; set; }

        /// <summary>
        /// Дочерние элементы элемента проводника
        /// </summary>
        ObservableCollection<INodeTree> ChildNode { get; set; }
    }
}
