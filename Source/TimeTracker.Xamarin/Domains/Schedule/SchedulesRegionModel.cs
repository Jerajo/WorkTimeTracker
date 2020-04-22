using TimeTracker.Application.Contracts;
using TimeTracker.Xamarin.Contracts;
using ICommandFactory = TimeTracker.Xamarin.Contracts.ICommandFactory;

namespace TimeTracker.Xamarin.Domains.Schedule
{
    public class SchedulesRegionModel : RegionModelBase
    {
        public SchedulesRegionModel(IRegionNavigationService navigationService,
            ICommandFactory commandFactory,
            IQueryFactory queryFactory)
            : base(navigationService, commandFactory, queryFactory)
        {
        }
    }
}
