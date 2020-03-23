namespace TimeTracker.Application.Contracts
{
    /// <summary>
    /// Create an instance of an async query.
    /// </summary>
    public interface IQueryFactory
    {
        /// <summary>
        /// Get an instance of an async query.
        /// </summary>
        /// <typeparam name="T">Query type.</typeparam>
        T GetInstance<T>() where T : IQuery;
    }
}
