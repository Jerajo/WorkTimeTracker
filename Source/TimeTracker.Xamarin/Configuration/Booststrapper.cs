using Ninject;
using Prism.Ioc;
using TimeTracker.Application.Contracts;
using TimeTracker.Application.Factories;
using TimeTracker.Xamarin.Shell;

namespace TimeTracker.Xamarin.Configuration
{
    public static class Booststrapper
    {
        public static IContainerRegistry RegisterNavigation(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainShell, MainShellModel>();
            return containerRegistry;
        }

        public static IContainerRegistry RegisterTypes(this IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<Contracts.ICommandFactory, Services.CommandFactory>();
            containerRegistry.Register<IQueryFactory, QueryFactory>();
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
