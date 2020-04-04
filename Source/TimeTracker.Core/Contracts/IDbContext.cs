using System;
using System.Threading;
using System.Threading.Tasks;

namespace TimeTracker.Core.Contracts
{
    /// <summary>
    /// Represent an object relational mapper (ORM) for the application.
    /// </summary>
    public interface IDbContext : IDisposable
    {
        /// <summary>
        /// Save all uncommitted changes.
        /// </summary>
        /// <returns>Count of affected rows.</returns>
        int SaveChanges();
        /// <summary>
        /// Save all uncommitted changes asynchronously.
        /// </summary>
        /// <returns>Count of affected rows.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// If the data base does not exits is created. If exits does nothing and returns false.
        /// </summary>
        /// <returns><see langword="true"/>/<see langword="false"/></returns>
        Task<bool> EnsureCreated();
        /// <summary>
        /// If the data base does exits is deleted. If not exits does nothing and returns false.
        /// </summary>
        /// <returns><see langword="true"/>/<see langword="false"/></returns>
        Task<bool> EnsureDeleted();
        /// <summary>
        /// Fetch default data to the database.
        /// </summary>
        /// <returns>Count of affected rows.</returns>
        Task<int> FetchInitialData();
    }
}
