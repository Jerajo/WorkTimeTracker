using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ninject;
using Ninject.Modules;
using TimeTracker.Application.Commands;
using TimeTracker.Application.Contracts;
using TimeTracker.Application.Factories;
using TimeTracker.Application.Queries;
using TimeTracker.DataAccess;
using TimeTracker.DataAccess.Contracts;
using TimeTracker.Domain;
using TimeTracker.Domain.Contracts;
using TimeTracker.Domain.ValueObjects;
using TimeTracker.Infrastructure.Services;

namespace TimeTracker.Tests.Common.Helpers
{
    /// <summary>
    /// Represents the application dependencies configuration.
    /// </summary>
    public class TestModule : NinjectModule
    {
        /// <summary>
        /// Load all dependencies.
        /// </summary>
        public override void Load()
        {
            // --------------- // MODELS // --------------- //
            Bind<ITask>().To<Task>();
            Bind<ISchedule>().To<Schedule>();
            Bind<IDescription>().To<Description>();
            Bind<ITasksSchedule>().To<TasksSchedule>();
            Bind<IGroup>().To<Group>();

            // --------------- // QUERIES // --------------- //
            Bind<IQuery>().To<GetTasksQuery>();
            Bind<IQuery>().To<GetGroupsQuery>();

            // --------------- // COMMANDS // --------------- //
            Bind<ICommand>().To<CreateTaskCommand>();
            Bind<ICommand>().To<UpdateTaskCommand>();
            Bind<ICommand>().To<DeleteTaskCommand>();
            Bind<ICommand>().To<TrackWorkTimeCommand>();
            Bind<ICommand>().To<CreateGroupCommand>();
            Bind<ICommand>().To<UpdateGroupCommand>();
            Bind<ICommand>().To<DeleteGroupCommand>();

            // --------------- // FACTORIES // --------------- //
            Bind<IQueryFactory>().To<QueryFactory>();
            Bind<ICommandFactory>().To<CommandFactory>();

            // --------------- // DB CONTEXT // --------------- //
            var options = new DbContextOptionsBuilder<WorkTimeTracker>()
                .UseInMemoryDatabase(databaseName: "WorkTimeTracker_TestDB")
                .Options;
            Bind<IDbContext>().To<WorkTimeTracker>().WithConstructorArgument(options);

            // --------------- // REPOSITORY AND UNIT OF WORK // --------------- //
            Bind<IRepositoryFactory>().To<RepositoryFactory>();
            Bind(typeof(IDataRepository<>)).To(typeof(DataRepository<>));
            Bind<IUnitOfWork>().To<UnitOfWork>();

            // --------------- // MAPPER // --------------- //
            var mapperConfiguration = CreateConfiguration();
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();

            Bind<IMapper>().ToMethod(ctx =>
                new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }

        /// <summary>
        /// Add all profiles in current assembly.
        /// </summary>
        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(GetType().Assembly);
            });

            return config;
        }
    }
}