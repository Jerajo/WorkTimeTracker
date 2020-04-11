using Prism.Navigation;
using TimeTracker.Xamarin.Contract;

namespace TimeTracker.Xamarin.Schedule
{
    public class ScheduleViewModel : ViewModelBase
    {
        public ScheduleViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
