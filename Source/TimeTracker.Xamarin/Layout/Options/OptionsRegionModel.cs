using DryIoc;
using TimeTracker.Core.Resources;
using TimeTracker.Xamarin.Contracts;

namespace TimeTracker.Xamarin.Layout.Options
{
    public class OptionsRegionModel : RegionModelBase
    {
        public OptionsRegionModel(IContainer container) : base(container)
        {
            Title = Messages.RegionTitleOptions;
        }
    }
}
