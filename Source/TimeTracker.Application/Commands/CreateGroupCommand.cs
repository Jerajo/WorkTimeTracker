using System;
using TimeTracker.Application.Contracts;
using TimeTracker.Domain.BaseClasses;

namespace TimeTracker.Application.Commands
{
    /// <summary>
    /// Create a new task.
    /// </summary>
    public class CreateGroupCommand : DisposableBase, ICommand<Domain.Group>
    {
        /// <inheritdoc/>
        /// <param name="group">Nullable parameter.</param>
        public IAsyncResult Run(Domain.Group group)
        {
            throw new NotImplementedException();
        }
    }
}
