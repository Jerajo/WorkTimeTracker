using Prism.Navigation;
using TimeTracker.Xamarin.Contract;

namespace TimeTracker.Xamarin.Layout
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
        }
    }
}
