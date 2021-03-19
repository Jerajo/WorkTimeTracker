using System.Threading.Tasks;

namespace TimeTracker.Xamarin.Contracts
{
    public interface IDialogService
    {
        Task<bool> DisplayDialogMessage(string message);
        Task DisplayErroMessage(string errorMessage);
    }
}
