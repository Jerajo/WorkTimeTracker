using Prism.Navigation;
using TimeTracker.Xamarin.Contract;

namespace TimeTracker.Xamarin.Task
{
    public class TaskViewModel : ViewModelBase
    {
        public TaskViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
