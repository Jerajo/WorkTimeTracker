using Microsoft.EntityFrameworkCore;
using Ninject.Modules;
using System;
using TimeTracker.Application.Factories;
using TimeTracker.Core.Contracts;
using TimeTracker.EF6.Services;

namespace TimeTracker.Xamarin.Configuration
{
    /// <summary>
    /// Represents the persistence layer dependency injection configuration.
    /// </summary>
    public class PersistenceModule : NinjectModule
    {
        private readonly DbContextOptions<WorkTimeTracker> _options;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="options">The assembly configuration for the database.</param>
        public PersistenceModule(Func<DbContextOptionsBuilder<WorkTimeTracker>,
            DbContextOptions<WorkTimeTracker>> options)
        {
            _options = options?.Invoke(new DbContextOptionsBuilder<WorkTimeTracker>());
        }

        #region Congiguration

        /// <summary>
        /// Load all dependencies for the persistence layer.
        /// </summary>
        public override void Load()
        {
            // --------------- // DB CONTEXT // --------------- //
            Bind<IDbContext>().To<WorkTimeTracker>().WithConstructorArgument(_options);
            Bind<WorkTimeTracker>().ToConstructor(_ => new WorkTimeTracker(_options));

                // --------------- // REPOSITORY AND UNIT OF WORK // --------------- //
            Bind(typeof(IDataRepository<>)).To(typeof(DataRepository<>));
            Bind<IRepositoryFactory>().To<RepositoryFactory>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }

        #endregion
    }
}
