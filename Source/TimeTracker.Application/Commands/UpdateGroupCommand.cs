using System;
using System.Threading.Tasks;
using TimeTracker.Application.Contracts;
using TimeTracker.Domain.BaseClasses;

namespace TimeTracker.Application.Commands
{
    /// <summary>
    /// Update the selected group.
    /// </summary>
    public class UpdateGroupCommand : DisposableBase, ICommand<Domain.Group>
    {
        /// <inheritdoc/>
        /// <param name="group">Nullable parameter.</param>
        public Task Run(Domain.Group group)
        {
            throw new NotImplementedException();
        }
    }
}
