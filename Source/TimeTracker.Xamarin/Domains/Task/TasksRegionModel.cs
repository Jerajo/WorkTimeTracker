using DryIoc;
using TimeTracker.Xamarin.Contracts;

namespace TimeTracker.Xamarin.Domains.Task
{
    public class TasksRegionModel : RegionModelBase
    {
        public TasksRegionModel(IContainer container) : base(container) { }
    }
}
