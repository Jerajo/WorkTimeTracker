using Prism.Mvvm;
using Prism.Navigation;

namespace TimeTracker.Xamarin.Contract
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        #region Properties

        protected INavigationService NavigationService { get; }

        public string Title { get; set; }

        #endregion

        #region Events

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }

        #endregion
    }
}
