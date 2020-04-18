using Prism.Navigation;
using TimeTracker.Application.Contracts;
using TimeTracker.Xamarin.Contracts;
using ICommandFactory = TimeTracker.Xamarin.Contracts.ICommandFactory;

namespace TimeTracker.Xamarin.Group
{
    public class GroupViewModel : ViewModelBase
    {
        public GroupViewModel(INavigationService navigationService,
            ICommandFactory commandFactory,
            IQueryFactory queryFactory) : base(navigationService, commandFactory, queryFactory)
        {
        }
    }
}
