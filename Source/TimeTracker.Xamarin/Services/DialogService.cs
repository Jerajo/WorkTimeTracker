using Prism.Services;
using System.Threading.Tasks;
using TimeTracker.Core.Resources;
using TimeTracker.Xamarin.Contracts;

namespace TimeTracker.Xamarin.Services
{
    public class DialogService : IDialogService
    {
        private readonly IPageDialogService _pageDialogService;

        public DialogService(IPageDialogService pageDialogService) =>
            _pageDialogService = pageDialogService;

        public Task DisplayErroMessage(string errorMessage)
        {
            return _pageDialogService.DisplayAlertAsync(Messages.DialogErrorTitle,
                errorMessage,
                Messages.DialogAcceptButton);
        }

        public Task<bool> DisplayDialogMessage(string message)
        {
            return _pageDialogService.DisplayAlertAsync(Messages.DialogErrorTitle,
                message,
                Messages.DialogAcceptButton,
                Messages.DialogCancelButton);
        }
    }
}
