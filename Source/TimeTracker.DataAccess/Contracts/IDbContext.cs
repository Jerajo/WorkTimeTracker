namespace TimeTracker.DataAccess.Contracts
{
    public interface IDbContext
    {
        int SaveChanges();
    }
}
