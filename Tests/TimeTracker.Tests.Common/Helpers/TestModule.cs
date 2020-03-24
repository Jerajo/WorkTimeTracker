using Microsoft.EntityFrameworkCore;
using Ninject.Modules;
using System.Collections.Generic;
using TimeTracker.Application.Contracts;
using TimeTracker.Application.Factories;
using TimeTracker.Application.Queries;
using TimeTracker.Infrastructure.Entities;

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

            var options = new DbContextOptionsBuilder<WorkTimeTracker>()
                .UseInMemoryDatabase(databaseName: "WorkTimeTracker_TestDB")
                .Options;
            Bind<IDbContext>().To<WorkTimeTracker>().WithConstructorArgument(options);
        }
    }
}