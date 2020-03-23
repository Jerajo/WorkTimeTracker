using Ninject.Modules;
using System.Collections.Generic;
using TimeTracker.Application.Contracts;
using TimeTracker.Application.Factories;
using TimeTracker.Application.Queries;

namespace TimeTracker.Tests.Common.Helpers
{
    public class TestModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IQuery<Domain.Group, List<Domain.Group>>>().To<GetGroupsQuery>();
            Bind<IQuery<Domain.Task, List<Domain.Task>>>().To<GetTasksQuery>();

            Bind<IQueryFactory>().To<QueryFactory>();
            Bind<ICommandFactory>().To<CommandFactory>();
        }
    }
}