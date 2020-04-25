using DryIoc;
using TimeTracker.Xamarin.Contracts;

namespace TimeTracker.Xamarin.Domains.Schedule
{
    public class SchedulesRegionModel : RegionModelBase
    {
        public SchedulesRegionModel(IContainer container) : base(container) { }
    }
}
