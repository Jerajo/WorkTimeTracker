using System;
using TimeTracker.Application.Contracts;

namespace TimeTracker.Application.Commands
{
    /// <summary>
    /// Delete the selected task.
    /// </summary>
    public class DeleteTaskCommand : ICommand<Domain.Task>
    {
        /// <inheritdoc/>
        /// <param name="task">Nullable parameter.</param>
        public IAsyncResult Run(Domain.Task task)
        {
            throw new NotImplementedException();
        }
    }
}
