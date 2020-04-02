using TimeTracker.DataAccess.Contracts;
using TimeTracker.Domain.Contracts;

namespace TimeTracker.DataAccess
{
    public interface IRepositoryFactory
    {
        IDataRepository<T> GetRepository<T>() where T : class, IEntity;
    }
}