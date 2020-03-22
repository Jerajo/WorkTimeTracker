using System.Threading.Tasks;

namespace TimeTracker.DataAccess
{
    public interface IDataBaseHandler
    {
        bool DataBaseExist { get; }

        Task CreateDataBase();
    }
}