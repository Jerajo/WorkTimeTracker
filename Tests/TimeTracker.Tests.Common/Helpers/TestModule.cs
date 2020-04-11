using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Ninject.Modules;
using System.Windows.Input;
using TimeTracker.Application.Commands;
using TimeTracker.Application.Contracts;
using TimeTracker.Application.Factories;
using TimeTracker.Application.Queries;
using TimeTracker.Application.Validators;
using TimeTracker.Core.Contracts;
using TimeTracker.Core.ValueObjects;
using TimeTracker.Infrastructure.Services;
using IValidatorFactory = TimeTracker.Application.Contracts.IValidatorFactory;

namespace TimeTracker.Tests.Common.Helpers
{
    /// <summary>
    /// Represents the application dependencies configuration.
    /// </summary>
    public class TestModule : NinjectModule
    {
        private readonly string _databaseName;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="databaseName">The name of the assembly test database.</param>
        public TestModule(string databaseName = null)
        {
            _databaseName = databaseName;
        }

        #region Congiguration

        /// <summary>
        /// Load all dependencies.
        /// </summary>
        public override void Load()
        {
            // --------------- // MODELS // --------------- //
            Bind<ITask>().To<Domain.Task>();
            Bind<ISchedule>().To<Domain.Schedule>();
            Bind<IDescription>().To<Description>();
            Bind<ITasksSchedule>().To<Domain.TasksSchedule>();
            Bind<IGroup>().To<Domain.Group>();

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

            // --------------- // VALIDATORS // --------------- //
            Bind<IValidator>().To<GroupValidator>();
            Bind<IValidator>().To<TaskValidator>();
            Bind<IValidator>().To<ScheduleValidator>();
            Bind<IValidator>().To<TasksScheduleValidator>();

            // --------------- // FACTORIES // --------------- //
            Bind<IValidatorFactory>().To<ValidatorFactory>();
            Bind<IQueryFactory>().To<QueryFactory>();
            Bind<ICommandFactory>().To<CommandFactory>();

            // --------------- // DB CONTEXT // --------------- //
            if (!string.IsNullOrEmpty(_databaseName))
            {
                var options = new DbContextOptionsBuilder<WorkTimeTracker>()
                    .UseInMemoryDatabase(_databaseName)
                    .UseLazyLoadingProxies()
                    .Options;
                Bind<IDbContext>().To<WorkTimeTracker>().WithConstructorArgument(options);
                Bind<WorkTimeTracker>().ToConstructor(_ => new WorkTimeTracker(options));
            }
            else
            {
                Bind<IDbContext>().To<WorkTimeTracker>();
                Bind<WorkTimeTracker>().ToConstructor(_ => new WorkTimeTracker());
            }

            // --------------- // REPOSITORY AND UNIT OF WORK // --------------- //
            Bind<IRepositoryFactory>().To<RepositoryFactory>();
            Bind(typeof(IDataRepository<>)).To(typeof(DataRepository<>));
            Bind<IUnitOfWork>().To<UnitOfWork>();

            // --------------- // MAPPER // --------------- //
            var mapperConfiguration = CreateConfiguration();
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();

            //Bind<IMapper>().ToMethod(ctx =>
            //    new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
            Bind<IMapper>().ToMethod(ctx => new Mapper(mapperConfiguration));
        }

        #endregion

        #region Auxiliary Methods

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

        #endregion
    }
}
