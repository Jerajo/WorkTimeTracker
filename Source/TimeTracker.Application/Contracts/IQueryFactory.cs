namespace TimeTracker.Application.Contracts
{
    /// <summary>
    /// Create an instance of an async query.
    /// </summary>
    /// <typeparam name="T">Query type.</typeparam>
    public interface IQueryFactory<out T> where T : class
    {
        /// <summary>
        /// Get an instance of an async query.
        /// </summary>
        T GetInstance();
    }
}
