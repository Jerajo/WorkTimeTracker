using Ninject;
using TimeTracker.Application.Contracts;

namespace TimeTracker.Application.Factories
{
    /// <summary>
    /// Create an instance of an async query.
    /// </summary>
    public class QueryFactory : IQueryFactory
    {
        private readonly IKernel _kernel;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="kernel">DI container.</param>
        public QueryFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        /// <inheritdoc/>
        public T GetInstance<T>() where T : IQuery
        {
            return _kernel.Get<T>();
        }
    }
}
