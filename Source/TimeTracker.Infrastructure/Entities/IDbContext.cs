namespace TimeTracker.Infrastructure.Entities
{
    internal interface IDbContext
    {
        int SaveChanges();
    }
}
