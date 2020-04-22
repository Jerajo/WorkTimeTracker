using TimeTracker.Application.Contracts;
using TimeTracker.Xamarin.Contracts;
using ICommandFactory = TimeTracker.Xamarin.Contracts.ICommandFactory;

namespace TimeTracker.Xamarin.Domains.Group
{
    public class GroupCellModel : RegionModelBase
    {
        public GroupCellModel(IRegionNavigationService navigationService,
            ICommandFactory commandFactory,
            IQueryFactory queryFactory)
            : base(navigationService, commandFactory, queryFactory)
        {
        }
    }
}
