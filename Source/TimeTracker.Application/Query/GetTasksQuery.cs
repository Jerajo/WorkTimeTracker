using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTracker.Application.Contracts;

namespace TimeTracker.Application.Query
{
    /// <summary>
    /// Get the list of available tasks.
    /// </summary>
    public class GetTasksQuery : IQuery<Domain.Task, List<Domain.Task>>
    {
        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="queryOptions">Search query options.</param>
        /// <returns>List of available tasks.</returns>
        public Task<List<Domain.Task>> Run(Domain.Task queryOptions)
        {
            throw new NotImplementedException();
            //return Task.FromResult(new List<Domain.Task>());
        }
    }
}
