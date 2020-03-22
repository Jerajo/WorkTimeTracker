using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TimeTracker.DataAccess;
using TimeTracker.Infrastructure.Entities;

namespace TimeTracker.Infrastructure
{
    /// <summary>
    /// Data base handler.
    /// </summary>
    public class DataBaseHandler : IDataBaseHandler
    {
        private readonly WorkTimeTracker _WorkTimeTracker;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DataBaseHandler(WorkTimeTracker workTimerTracker)
        {
            Guard.Against.Null(workTimerTracker, nameof(workTimerTracker));

            _WorkTimeTracker = workTimerTracker;
        }

        /// <summary>
        /// Indicates whether the data base exist or not.
        /// </summary>
        public bool DataBaseExist => _WorkTimeTracker.Database.CanConnect();

        /// <summary>
        /// Create a new data base if not exist.
        /// </summary>
        public Task CreateDataBase()
        {
            return Task.Run(() =>
            {
                if(!DataBaseExist)
                    _WorkTimeTracker.Database.Migrate();
                else
                    throw new InvalidOperationException("Error: código 504.");
            });
        }
    }
}
