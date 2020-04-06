using System;
using System.Collections.Generic;
using System.Linq;
using Async = System.Threading.Tasks;

namespace TimeTracker.Core.Contracts
{
    public interface IDataRepository<T> : IDisposable
        where T : class, IEntity
    {
        IUnitOfWork GetTransaction();

        #region GET

        T Get(Func<T, bool> query);
        Async.Task<T> GetAllAsync(Func<T, bool> query);
        IQueryable<T> GetAll();
        Async.Task<IQueryable<T>> GetAllAsync();
        List<T> Query(Func<T, bool> query);
        Async.Task<List<T>> QueryAsync(Func<T, bool> query);

        bool Any();
        Async.Task<bool> AnyAsync();

        #endregion

        #region POST

        void Add(T entity);
        Async.Task AddAsync(T entity);
        void AddGroup(IEnumerable<T> entities);
        Async.Task AddGroupAsync(IEnumerable<T> entities);

        #endregion

        #region PUT/PATCH

        void Update(T entity);
        Async.Task UpdateAsync(T entity);
        void UpdateGroup(T entity, Func<T, bool> query);
        Async.Task UpdateGroupAsync(T entity, Func<T, bool> query);

        #endregion

        #region DELETE

        void Delete(T entity);
        Async.Task DeleteAsync(T entity);
        void DeleteGroup(Func<T, bool> query);
        Async.Task DeleteGroupAsync(Func<T, bool> query);

        #endregion
    }
}
