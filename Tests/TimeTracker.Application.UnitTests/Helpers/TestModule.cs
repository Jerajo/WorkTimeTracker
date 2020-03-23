using Ninject.Modules;
using System.Collections.Generic;
using TimeTracker.Application.Contracts;
using TimeTracker.Application.Queries;

namespace TimeTracker.Application.UnitTests.Helpers
{
    internal class TestModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IQuery<Domain.Group, List<Domain.Group>>>().To<GetGroupsQuery>();
            Bind<IQuery<Domain.Task, List<Domain.Task>>>().To<GetTasksQuery>();
        }
    }
}