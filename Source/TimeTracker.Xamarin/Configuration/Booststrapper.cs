using Ninject;
using Prism.Ioc;
using TimeTracker.Application.Contracts;
using TimeTracker.Application.Factories;
using TimeTracker.Xamarin.Contracts;
using TimeTracker.Xamarin.Domains.Group;
using TimeTracker.Xamarin.Domains.Schedule;
using TimeTracker.Xamarin.Domains.Task;
using TimeTracker.Xamarin.Layout;
using TimeTracker.Xamarin.Layout.Options;
using TimeTracker.Xamarin.Resources;
using TimeTracker.Xamarin.Services;

namespace TimeTracker.Xamarin.Configuration
{
    public static class Booststrapper
    {
        public static IContainerRegistry RegisterNavigation(this IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IRegionNavigationService, RegionNavigationService>();
            containerRegistry.RegisterForNavigation<MainLayout, MainLayoutModel>();

            containerRegistry.RegisterRegion<GroupCell, GroupCellModel>(Regions.GroupCell);
            containerRegistry.RegisterRegion<TaskCell, TaskCellModel>(Regions.TaskCell);
            containerRegistry.RegisterRegion<ScheduleCell, ScheduleCellModel>(Regions.ScheduleCell);

            containerRegistry.RegisterRegion<TasksRegion, TasksRegionModel>(Regions.Tasks);
            containerRegistry.RegisterRegion<SchedulesRegion, SchedulesRegionModel>(Regions.Schedules);
            containerRegistry.RegisterRegion<OptionsRegion, OptionsRegionModel>(Regions.Options);

            return containerRegistry;
        }

        public static IContainerRegistry RegisterTypes(this IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<Contracts.ICommandFactory, Services.CommandFactory>();
            containerRegistry.Register<IQueryFactory, QueryFactory>();
            containerRegistry.Register<IRegionNavigationService, RegionNavigationService>();
            containerRegistry.Register<IDialogService, DialogService>();
            return containerRegistry;
        }

        public static IContainerRegistry RegisterContainer(this IContainerRegistry containerRegistry,
            IKernel kernel)
        {
            containerRegistry.RegisterInstance<IKernel>(kernel);
            return containerRegistry;
        }
    }
}
