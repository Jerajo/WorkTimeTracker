using System.Collections.Generic;
using TimeTracker.Core.Resources;
using Xamarin.Forms.PowerControls.Buttons;
using Xamarin.Forms.PowerControls.Resources.Fonts;

namespace TimeTracker.Xamarin.Resources
{
    public static class UiComponents
    {
        #region Header



        #endregion

        #region Navigation Menu

        public static List<NavigationButtonModel> GetNavigationButtonModelss() =>
            new List<NavigationButtonModel>
            {
                new NavigationButtonModel(Messages.NavMenuTask,
                    Regions.Tasks,
                    Fonts.MaterialRegular,
                    FontAwesomeRegular.CheckSquare),
                new NavigationButtonModel(Messages.NavMenuTrack,
                    Regions.Schedules,
                    Fonts.MaterialRegular,
                    FontAwesomeRegular.Clock),
                new NavigationButtonModel(Messages.NavMenuOptions,
                    Regions.Options,
                    Fonts.MaterialSolid,
                    FontAwesomeSolid.Cog),
            };

        #endregion

        #region Tracker



        #endregion
    }
}
