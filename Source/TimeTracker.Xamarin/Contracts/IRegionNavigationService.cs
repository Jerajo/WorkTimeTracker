using Prism.Navigation;
using System.Threading.Tasks;

namespace TimeTracker.Xamarin.Contracts
{
    public interface IRegionNavigationService
    {
        NavigationResult NavigateTo(string path, INavigationParameters parameters = null);
        Task<NavigationResult> NavigateToAsync(string path, INavigationParameters parameters = null);
        NavigationResult NavigateBack(INavigationParameters parameters = null);
        Task<NavigationResult> NavigateBackAsync(INavigationParameters parameters = null);
    }
}
