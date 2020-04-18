namespace TimeTracker.Core.Contracts
{
    public interface IRepositoryFactory
    {
        IDataRepository<T> GetRepository<T>() where T : class, IEntity;
    }
}