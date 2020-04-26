using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Xamarin.Forms.PowerControls.Contracts
{
    public class BindableBase : INotifyPropertyChanged, INotifyPropertyChanging
    {
        #region Property Changing

        public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;

        protected virtual void OnPropertyChanging(object oldValue,
            object newValue,
            [CallerMemberName] string propertyName = null)
        {
            if (oldValue.Equals(newValue))
                return;

            PropertyChanging?.Invoke(this, new System.ComponentModel.PropertyChangingEventArgs(propertyName));
        }

        public void RaisePropertyChanging(object oldValue,
            object newValue,
            string propertyName)
            => OnPropertyChanging(oldValue, newValue, propertyName);

        #endregion

        #region Property Changed

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void RaisePropertyChanged(string propertyName) => OnPropertyChanged(propertyName);

        #endregion
    }
}
