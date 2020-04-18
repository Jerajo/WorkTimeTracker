using Prism.Navigation;
using TimeTracker.Application.Contracts;
using TimeTracker.Xamarin.Contracts;
using ICommandFactory = TimeTracker.Xamarin.Contracts.ICommandFactory;

namespace TimeTracker.Xamarin.Task
{
    public class TaskViewModel : ViewModelBase
    {
        public TaskViewModel(INavigationService navigationService,
            ICommandFactory commandFactory,
            IQueryFactory queryFactory) : base(navigationService, commandFactory, queryFactory)
        {
        }
    }
}
