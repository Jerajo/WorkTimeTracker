using System;
using TimeTracker.Application.Contracts;

namespace TimeTracker.Application.Commands
{
    /// <summary>
    /// Update the selected group.
    /// </summary>
    public class UpdateGroupCommand : ICommand<Domain.Group>
    {
        /// <inheritdoc/>
        /// <param name="group">Nullable parameter.</param>
        public IAsyncResult Run(Domain.Group group)
        {
            throw new NotImplementedException();
        }
    }
}
