using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TimeTracker.DataAccess.Contracts
{
    public interface IDataRepository<T> : IDisposable where T : class, IEntity
    {
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

        void Add(T entity);
        Task AddAsync(T entity);
        void AddGroup(IEnumerable<T> entities);
        Task AddGroupAsync(IEnumerable<T> entities);

        void Update(T entity);
        Task UpdateAsync(T entity);
        void UpdateGroup(T entity, Func<T, bool> query);
        Task UpdateGroupAsync(T entity, Func<T, bool> query);

        void Delete(T entity);
        Task DeleteAsync(T entity);
        void DeleteGroup(Func<T, bool> query);
        Task DeleteGroupAsync(Func<T, bool> query);
    }

    public interface IUnitOfWork : IDisposable
    {
        IDataRepository<T> GetRepository<T>() where T : class, IEntity;
        void Commit();
        Task CommitAsync();
    }

    public interface IEntity
    {
        int Id { get; set; }
    }
}
