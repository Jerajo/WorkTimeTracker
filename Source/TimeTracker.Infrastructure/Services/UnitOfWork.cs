using TimeTracker.DataAccess.Contracts;
using TimeTracker.Domain.BaseClasses;
using TimeTracker.Infrastructure.Entities;
using AsyncOperation = System.Threading.Tasks.Task;

namespace TimeTracker.Infrastructure.Services
{
    public class UnitOfWork : DisposableBase, IUnitOfWork
    {
        private readonly WorkTimeTracker _WorkTimeTracker;

        public UnitOfWork(WorkTimeTracker workTimeTracker)
        {
            _WorkTimeTracker = workTimeTracker;
        }

        #region Interface Methods

        public void Commit()
        {
            _WorkTimeTracker.SaveChanges();
        }

        public AsyncOperation CommitAsync()
        {
            return _WorkTimeTracker.SaveChangesAsync();
        }

        IDataRepository<T> IUnitOfWork.GetRepository<T>()
        {
            return new DataRepository<T>(_WorkTimeTracker);
        }

        #endregion
    }
}
