using TimeTracker.Core.BaseClasses;
using TimeTracker.Core.Contracts;
using AsyncOperation = System.Threading.Tasks.Task;

namespace TimeTracker.Infrastructure.Services
{
    public class UnitOfWork : DisposableBase, IUnitOfWork
    {
        private readonly WorkTimeTracker _workTimeTracker;

        public UnitOfWork(WorkTimeTracker workTimeTracker)
        {
            _workTimeTracker = workTimeTracker;
        }

        #region Interface Methods

        public void Commit()
        {
            _workTimeTracker.SaveChanges();
        }

        public AsyncOperation CommitAsync()
        {
            return _workTimeTracker.SaveChangesAsync();
        }

        public IDataRepository<T> GetRepository<T>() where T : class, IEntity
        {
            return new DataRepository<T>(_workTimeTracker);
        }

        #endregion
    }
}
