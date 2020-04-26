using Xamarin.Forms.PowerControls.Contracts;

namespace Xamarin.Forms.PowerControls.Menus
{
    public class NavigationMenuModel : BindableBase
    {
        private object _selectedTab;
        public object SelectedTab
        {
            get => _selectedTab;
            set
            {
                _selectedTab = value;
                OnTabSelected(_selectedTab);
            }
        }

        protected virtual void OnTabSelected(object selectedTab) {}
    }
}
