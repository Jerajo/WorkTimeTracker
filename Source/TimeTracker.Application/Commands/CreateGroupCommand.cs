using System;
using TimeTracker.Application.Contracts;

namespace TimeTracker.Application.Commands
{
    /// <summary>
    /// Create a new task.
    /// </summary>
    public class CreateGroupCommand : ICommand<Domain.Group>
    {
        /// <inheritdoc/>
        /// <param name="group">Nullable parameter.</param>
        public IAsyncResult Run(Domain.Group group)
        {
            throw new NotImplementedException();
        }
    }
}
