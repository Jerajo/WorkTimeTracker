using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TimeTracker.Domain.Contracts;

namespace TimeTracker.DataAccess.Contracts
{
    public interface IDataRepository<T> : IDisposable
        where T : class, IEntity
    {
        IUnitOfWork GetTransaction();

        #region GET

        T Get(Func<T, bool> query);
        Task<T> GetAsync(Func<T, bool> query);
        IQueryable<T> GetAll();
        Task<IQueryable<T>> GetAllAsync();
        List<T> Query(Func<T, bool> query);
        Task<List<T>> QueryAsync(Func<T, bool> query);
        List<TB> QuerySelect<TB>(Expression<Func<T, TB>> selector, Func<TB, bool> query);
        Task<List<TB>> QuerySelectAsync<TB>(Expression<Func<T, TB>> selector, Func<TB, bool> query);

        bool Any();
        Task<bool> AnyAsync();

        #endregion

        #region POST

        void Add(T entity);
        Task AddAsync(T entity);
        void AddGroup(IEnumerable<T> entities);
        Task AddGroupAsync(IEnumerable<T> entities);

        #endregion

        #region PUT/PATCH

        void Update(T entity);
        Task UpdateAsync(T entity);
        void UpdateGroup(T entity, Func<T, bool> query);
        Task UpdateGroupAsync(T entity, Func<T, bool> query);

        #endregion

        #region DELETE

        void Delete(T entity);
        Task DeleteAsync(T entity);
        void DeleteGroup(Func<T, bool> query);
        Task DeleteGroupAsync(Func<T, bool> query);

        #endregion
    }
}
