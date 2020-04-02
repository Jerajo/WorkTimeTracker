using Ninject;
using TimeTracker.Core.Contracts;

namespace TimeTracker.DataAccess
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IKernel _Kernel;

        public RepositoryFactory(IKernel kernel)
        {
            _Kernel = kernel;
        }

        public IDataRepository<T> GetRepository<T>() where T : class, IEntity
        {
            return _Kernel.Get<IDataRepository<T>>();
        }
    }
}
