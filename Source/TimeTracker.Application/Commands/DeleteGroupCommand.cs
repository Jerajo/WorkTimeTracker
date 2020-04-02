using System;
using TimeTracker.Application.Contracts;
using TimeTracker.Core.BaseClasses;
using AsyncOperation = System.Threading.Tasks.Task;

namespace TimeTracker.Application.Commands
{
    /// <summary>
    /// Delete the selected group.
    /// </summary>
    public class DeleteGroupCommand : DisposableBase, ICommand<Domain.Group>
    {
        /// <inheritdoc/>
        /// <param name="group">Nullable parameter.</param>
        public AsyncOperation Run(Domain.Group group)
        {
            throw new NotImplementedException();
        }
    }
}
