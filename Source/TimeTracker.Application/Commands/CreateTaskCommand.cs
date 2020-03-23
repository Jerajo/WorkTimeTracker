using System;
using TimeTracker.Application.Contracts;

namespace TimeTracker.Application.Commands
{
    /// <summary>
    /// Create a new task.
    /// </summary>
    public class CreateTaskCommand : ICommand<Domain.Task>
    {
        /// <inheritdoc/>
        /// <param name="task">Nullable parameter.</param>
        public IAsyncResult Run(Domain.Task task)
        {
            throw new NotImplementedException();
        }
    }
}
