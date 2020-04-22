using Ninject.Modules;
using TimeTracker.Core.Contracts;
using TimeTracker.Core.ValueObjects;

namespace TimeTracker.Xamarin.Configuration
{
    /// <summary>
    /// Represents the domain layer dependency injection configuration.
    /// </summary>
    public class DomainModule : NinjectModule
    {
        /// <summary>
        /// Load all dependencies for the domain layer.
        /// </summary>
        public override void Load()
        {
            // --------------- // MODELS // --------------- //
            Bind<ITask>().To<Domain.Task>();
            Bind<ISchedule>().To<Domain.Schedule>();
            Bind<IDescription>().To<Description>();
            Bind<ITasksSchedule>().To<Domain.TasksSchedule>();
            Bind<IGroup>().To<Domain.Group>();
        }
    }
}
