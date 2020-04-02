using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTracker.Application.Contracts;
using TimeTracker.Core.BaseClasses;

namespace TimeTracker.Application.Queries
{
    /// <summary>
    /// Get the list of available groups.
    /// </summary>
    public class GetGroupsQuery : DisposableBase, IQuery<Domain.Group, List<Domain.Group>>
    {
        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="queryOptions">Search query options.</param>
        /// <returns>List of available groups.</returns>
        public Task<List<Domain.Group>> Run(Domain.Group queryOptions)
        {
            throw new NotImplementedException();
            //return Task.FromResult(new List<Domain.Group>());
        }
    }
}
