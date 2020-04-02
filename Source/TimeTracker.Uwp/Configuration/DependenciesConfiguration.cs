﻿using AutoMapper;
using Ninject;
using Ninject.Modules;
using System.Collections.Generic;
using TimeTracker.Application.Commands;
using TimeTracker.Application.Contracts;
using TimeTracker.Application.Factories;
using TimeTracker.Application.Queries;
using TimeTracker.DataAccess.Contracts;
using TimeTracker.Infrastructure.Services;

namespace TimeTracker.Uwp.Configuration
{
    /// <summary>
    /// Represents the application dependencies configuration.
    /// </summary>
    public class DependenciesConfiguration : NinjectModule
    {
        /// <summary>
        /// Load all dependencies.
        /// </summary>
        public override void Load()
        {
            // --------------- // MODELS // --------------- //
            Bind<IQuery<Domain.Group, List<Domain.Group>>>().To<GetGroupsQuery>();
            Bind<IQuery<Domain.Task, List<Domain.Task>>>().To<GetTasksQuery>();

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
            Bind<IDbContext>().To<WorkTimeTracker>();

            // --------------- // REPOSITORY AND UNIT OF WORK // --------------- //
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
