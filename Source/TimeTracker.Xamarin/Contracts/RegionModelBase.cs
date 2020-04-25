using DryIoc;
using Prism.Mvvm;
using Prism.Navigation;
using TimeTracker.Application.Contracts;

namespace TimeTracker.Xamarin.Contracts
{
    public class RegionModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        public RegionModelBase(IContainer container)
        {
            NavigationService = container.Resolve<IRegionNavigationService>();
            CommandFactory = container.Resolve<ICommandFactory>(); ;
            QueryFactory = container.Resolve<IQueryFactory>(); ;
        }

        #region Properties

        protected IRegionNavigationService NavigationService { get; }
        protected ICommandFactory CommandFactory { get; }
        public IQueryFactory QueryFactory { get; }
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
