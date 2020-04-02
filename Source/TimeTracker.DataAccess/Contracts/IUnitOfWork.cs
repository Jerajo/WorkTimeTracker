using System;
using System.Threading.Tasks;
using TimeTracker.Domain.Contracts;

namespace TimeTracker.DataAccess.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IDataRepository<T> GetRepository<T>() where T : class, IEntity;
        void Commit();
        Task CommitAsync();
    }
}
