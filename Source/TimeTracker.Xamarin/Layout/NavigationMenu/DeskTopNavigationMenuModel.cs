using System.Collections.Generic;
using System.Diagnostics;
using TimeTracker.Core.Resources;
using TimeTracker.Xamarin.Contracts;
using TimeTracker.Xamarin.Resources;
using TimeTracker.Xamarin.Services;
using Xamarin.Forms.PowerControls.Buttons;
using Xamarin.Forms.PowerControls.Menus;

namespace TimeTracker.Xamarin.Layout.NavigationMenu
{
    public class DeskTopNavigationMenuModel : NavigationMenuModel
    {
        private readonly IRegionNavigationService _navigationService;
        private readonly IDialogService _dialogService;

        public DeskTopNavigationMenuModel(IRegionNavigationService navigationService,
            IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            Tabs = UiComponents.GetNavigationButtonModelss();
        }

        public List<NavigationButtonModel> Tabs { get; }

        #region Auxiliary Methods

        protected override async void OnTabSelected(object selectedTab)
        {
            if (!(selectedTab is NavigationButtonModel model))
            {
                await _dialogService.DisplayErroMessage(Messages.DialogNavigationErrorMessage);
                return;
            }
            NavigateTo(model.Path);
        }

        private async void NavigateTo(string path)
        {
            if (RegionNavigationService.CurrentRegionName == path)
                return;

            if (RegionNavigationService.NavigationHistory.Contains(path))
            {
                var navigateBackResult =  await _navigationService.NavigateBackAsync();
                if (!navigateBackResult.Success)
                {
                    await _dialogService.DisplayErroMessage(Messages.DialogNavigationErrorMessage);
                    Debug.WriteLine(navigateBackResult.Exception.Message);
                }
                return;
            }

            var navigateToResult = await _navigationService.NavigateToAsync(path);
            if (!navigateToResult.Success)
            {
                await _dialogService.DisplayErroMessage(Messages.DialogNavigationErrorMessage);
                Debug.WriteLine(navigateToResult.Exception.Message);
            }
        }

        #endregion
    }
}
