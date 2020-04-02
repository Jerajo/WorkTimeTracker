namespace TimeTracker.Core.Contracts
{
    /// <summary>
    /// Represent an object relational mapper (ORM) for the application.
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// Save all uncommitted changes.
        /// </summary>
        /// <returns>Count of affected rows.</returns>
        int SaveChanges();
    }
}
