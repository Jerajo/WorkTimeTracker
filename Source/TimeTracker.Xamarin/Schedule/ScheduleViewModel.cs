using Prism.Navigation;
using TimeTracker.Application.Contracts;
using TimeTracker.Xamarin.Contracts;
using ICommandFactory = TimeTracker.Xamarin.Contracts.ICommandFactory;

namespace TimeTracker.Xamarin.Schedule
{
    public class ScheduleViewModel : ViewModelBase
    {
        public ScheduleViewModel(INavigationService navigationService,
            ICommandFactory commandFactory,
            IQueryFactory queryFactory) : base(navigationService, commandFactory, queryFactory)
        {
        }
    }
}
