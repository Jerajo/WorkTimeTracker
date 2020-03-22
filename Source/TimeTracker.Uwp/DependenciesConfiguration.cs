using Ninject.Modules;
using System.Collections.Generic;
using TimeTracker.Application.Contracts;
using TimeTracker.Application.Query;

namespace TimeTracker.Uwp
{
    /// <summary>
    /// Represents the aplication dependencies configuration.
    /// </summary>
    public class DependenciesConfiguration : NinjectModule
    {
        /// <summary>
        /// Load all dependencies.
        /// </summary>
        public override void Load()
        {
            Bind<IQuery<Domain.Group, List<Domain.Group>>>().To<GetGroupsQuery>();
            Bind<IQuery<Domain.Task, List<Domain.Task>>>().To<GetTasksQuery>();
        }
    }
}
