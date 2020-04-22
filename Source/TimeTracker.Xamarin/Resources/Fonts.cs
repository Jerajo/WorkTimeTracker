using Xamarin.Forms;

namespace TimeTracker.Xamarin.Resources
{
    public static class Fonts
    {
        public static string MaterialRegular =>
            (OnPlatform<string>)App.Current.Resources["FontAwesomeRegularIcons"];
        public static string MaterialSolid =>
            (OnPlatform<string>)App.Current.Resources["FontAwesomeSolidIcons"];
        public static string MaterialBrand =>
            (OnPlatform<string>)App.Current.Resources["FontAwesomeBrandIcons"];
    }
}
