using System;
using TimeTracker.Application.Contracts;

namespace TimeTracker.Application.Factories
{
    /// <summary>
    /// Create an instance of an async query.
    /// </summary>
    /// <typeparam name="T">Query type.</typeparam>
    public class QueryFactory<T> : IQueryFactory<T> where T : class
    {
        /// <inheritdoc/>
        public T GetInstance()
        {
            throw new NotImplementedException();
        }
    }
}
