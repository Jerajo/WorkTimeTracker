using AutoMapper;
using DryIoc;
using Ninject;
using Prism.Common;
using Prism.Mvvm;
using Prism.Navigation;
using System.Windows.Input;
using TimeTracker.Application.Contracts;
using TimeTracker.Core.Resources;
using TimeTracker.Xamarin.Layout;
using TimeTracker.Xamarin.Layout.Notifications;

namespace TimeTracker.Xamarin.Contracts
{
    public class RegionModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        public RegionModelBase(IContainer container)
        {
            Kernel = container.Resolve<IKernel>();
            NavigationService = container.Resolve<IRegionNavigationService>();
            CommandFactory = container.Resolve<ICommandFactory>();

            ApplicationProvider = container.Resolve<IApplicationProvider>();
            MainPage = (MainLayout)ApplicationProvider.MainPage;

            QueryFactory = Kernel.Get<IQueryFactory>();
            Mapper = Kernel.Get<IMapper>();
        }


        #region Properties

        protected IKernel Kernel { get; }
        protected IRegionNavigationService NavigationService { get; }
        protected ICommandFactory CommandFactory { get; }
        protected IQueryFactory QueryFactory { get; }
        protected IMapper Mapper { get; }
        protected IApplicationProvider ApplicationProvider { get; }
        protected MainLayout MainPage { get; }
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

        #region Auxiliary Methods

        protected virtual void PushNotification(string message,
            NotificationType type,
            int? duration = null)
        {
            var notification = new Notification(message, type);
            MainPage.PushNotification(notification, duration);
        }

        protected virtual void PushRevertNotification(ICommand revertCommand,
            object commandParameter)
        {
            var notification = new Notification(Messages.NotificationMessageRevert,
                NotificationType.Revert)
            {
                RevertCommand = revertCommand,
                CommandParameter = commandParameter
            };
            MainPage.PushNotification(notification, 5000);
        }

        #endregion
    }
}
