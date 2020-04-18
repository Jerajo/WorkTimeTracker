using Ninject;
using TimeTracker.Core.Contracts;

namespace TimeTracker.Application.Factories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IKernel _kernel;

        public RepositoryFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IDataRepository<T> GetRepository<T>() where T : class, IEntity
        {
            return _kernel.Get<IDataRepository<T>>();
        }
    }
}
