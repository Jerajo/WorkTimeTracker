using Prism.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTracker.Xamarin.Contracts;
using TimeTracker.Xamarin.Resources;
using TimeTracker.Xamarin.Services;
using Xamarin.Forms.PowerControls.Buttons;
using Xamarin.Forms.PowerControls.Menus;
using Xamarin.Forms.PowerControls.Resources.Fonts;

namespace TimeTracker.Xamarin.Layout.NavigationMenu
{
    public class DeskTopNavigationMenuModel : NavigationMenuModel
    {
        private readonly IRegionNavigationService _navigationService;
        private readonly IPageDialogService _pageDialogService;

        public DeskTopNavigationMenuModel(IRegionNavigationService navigationService,
            IPageDialogService pageDialogService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;

            //TODO: implement load menu from persistence.
            Tabs = new List<NavigationButtonModel>
            {
                new NavigationButtonModel(Regions.Tasks,
                    Regions.Tasks,
                    Fonts.MaterialRegular,
                    FontAwesomeRegular.CheckSquare),
                new NavigationButtonModel(Regions.Schedules,
                    Regions.Schedules,
                    Fonts.MaterialRegular,
                    FontAwesomeRegular.Clock),
                new NavigationButtonModel(Regions.Options,
                    Regions.Options,
                    Fonts.MaterialSolid,
                    FontAwesomeSolid.Cog),
            };
        }

        public List<NavigationButtonModel> Tabs { get; }

        #region Auxiliary Methods

        protected override void OnTabSelected(object selectedTab)
        {
            if (!(selectedTab is NavigationButtonModel model))
                return;
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
                    await ShowNavigationErroMessage(navigateBackResult.Exception.Message);
                return;
            }

            var navigateToResult = await _navigationService.NavigateToAsync(path);
            if (!navigateToResult.Success)
                await ShowNavigationErroMessage(navigateToResult.Exception.Message);
        }

        private async Task ShowNavigationErroMessage(string exception)
        {
            await _pageDialogService.DisplayAlertAsync("Error",
                "No se pudo navegar a la region seleccionada. \n" + exception,
                "Aceptar");
        }

        #endregion
    }
}
