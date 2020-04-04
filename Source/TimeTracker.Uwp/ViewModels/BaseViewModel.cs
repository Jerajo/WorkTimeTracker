using System.ComponentModel;
using TimeTracker.Core.BaseClasses;
using TimeTracker.Core.Contracts;

namespace TimeTracker.Uwp.ViewModels
{
    public class BaseViewModel : DisposableBase, INotifyPropertyChanged, IEntity
    {
        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <inheritdoc/>
        public int Id { get; set; }
    }
}
