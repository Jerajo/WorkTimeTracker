using TimeTracker.Application.Contracts;
using TimeTracker.Xamarin.Contracts;
using ICommandFactory = TimeTracker.Xamarin.Contracts.ICommandFactory;

namespace TimeTracker.Xamarin.Domains.Task
{
    public class TasksRegionModel : RegionModelBase
    {
        public TasksRegionModel(IRegionNavigationService navigationService,
            ICommandFactory commandFactory,
            IQueryFactory queryFactory)
            : base(navigationService, commandFactory, queryFactory)
        {
        }
    }
}
