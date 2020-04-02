using System;
using AsyncOperation = System.Threading.Tasks.Task;

namespace TimeTracker.Core.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IDataRepository<T> GetRepository<T>() where T : class, IEntity;
        void Commit();
        AsyncOperation CommitAsync();
    }
}
