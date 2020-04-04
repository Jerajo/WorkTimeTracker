using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TimeTracker.Core.BaseClasses;
using TimeTracker.Core.Contracts;
using AsyncOperation = System.Threading.Tasks.Task;

namespace TimeTracker.Infrastructure.Services
{
    public class DataRepository<T> : DisposableBase, IDataRepository<T> where T : class, IEntity
    {
        private readonly WorkTimeTracker _workTimeTracker;

        public DataRepository(WorkTimeTracker workTimeTracker)
        {
            _workTimeTracker = workTimeTracker;
        }

        public IUnitOfWork GetTransaction()
        {
            return new UnitOfWork(_workTimeTracker);
        }

        #region Queries

        #region GET

        public T Get(Func<T, bool> query)
        {
            return _workTimeTracker.Set<T>().Find(query);
        }

        public Task<T> GetAsync(Func<T, bool> query)
        {
            return AsyncOperation.FromResult(Get(query));
        }

        public IQueryable<T> GetAll()
        {
            return _workTimeTracker.Set<T>();
        }

        public Task<IQueryable<T>> GetAllAsync()
        {
            return AsyncOperation.FromResult(GetAll());
        }

        public List<T> Query(Func<T, bool> query)
        {
            return _workTimeTracker.Set<T>().Where(query).ToList();
        }

        public Task<List<T>> QueryAsync(Func<T, bool> query)
        {
            return AsyncOperation.FromResult(Query(query));
        }

        public List<TB> QuerySelect<TB>(Expression<Func<T, TB>> selector,
            Func<TB, bool> query)
        {
            return _workTimeTracker.Set<T>()
                .Select(selector)
                .Where(query)
                .ToList();
        }

        public Task<List<TB>> QuerySelectAsync<TB>(Expression<Func<T, TB>> selector,
            Func<TB, bool> query)
        {
            return AsyncOperation.FromResult(QuerySelect(selector, query));
        }

        #endregion

        #region OPTIONS

        public bool Any()
        {
            return _workTimeTracker.Set<T>().Any();
        }

        public Task<bool> AnyAsync()
        {
            return AsyncOperation.FromResult(Any());
        }

        #endregion

        #endregion

        #region Commands

        #region POST

        public void Add(T entity)
        {
            _workTimeTracker.Set<T>().Add(entity);
        }

        public AsyncOperation AddAsync(T entity)
        {
            return AsyncOperation.Run(() => Add(entity));
        }

        public void AddGroup(IEnumerable<T> entities)
        {
            _workTimeTracker.Set<T>().AddRange(entities);
        }

        public AsyncOperation AddGroupAsync(IEnumerable<T> entities)
        {
            return AsyncOperation.Run(() => AddGroup(entities));
        }

        #endregion


        #region DELETE

        public void Delete(T entity)
        {
            _workTimeTracker.Set<T>().Remove(entity);
        }

        public AsyncOperation DeleteAsync(T entity)
        {
            return AsyncOperation.Run(() => Delete(entity));
        }

        public void DeleteGroup(Func<T, bool> query)
        {
            var entitiesToDelete = Query(query);
            _workTimeTracker.Set<T>().RemoveRange(entitiesToDelete);
        }

        public AsyncOperation DeleteGroupAsync(Func<T, bool> query)
        {
            return AsyncOperation.Run(() => DeleteGroup(query));
        }

        #endregion


        #region PUT/PACH

        public void Update(T entity)
        {
            _workTimeTracker.Set<T>().Update(entity);
        }

        public AsyncOperation UpdateAsync(T entity)
        {
            return AsyncOperation.Run(() => Update(entity));
        }

        public void UpdateGroup(T entity, Func<T, bool> query)
        {
            var entitiesToUpdate = Query(query);
            var updatedEntities = entitiesToUpdate.Select(x => UpdateGenericObject(x, entity));
            _workTimeTracker.Set<T>().UpdateRange(updatedEntities);
        }

        public AsyncOperation UpdateGroupAsync(T entity, Func<T, bool> query)
        {
            return AsyncOperation.Run(() => UpdateGroup(entity, query));
        }

        #endregion

        #endregion

        #region Auxiliary Methods

        private T UpdateGenericObject(T entityToUpdate, T entityChanges)
        {
            var toUpdateProperties = entityToUpdate.GetType().GetProperties();
            var changedPropertyInfos = entityChanges.GetType().GetProperties();

            for(var i = 0; i < toUpdateProperties.Length; i++)
            {
                if(toUpdateProperties[i].Name == nameof(entityChanges.Id))
                    continue;
                toUpdateProperties[i].SetValue(entityToUpdate,
                    changedPropertyInfos[i].GetValue(changedPropertyInfos));
            }

            return entityToUpdate;
        }

        #endregion
    }
}
