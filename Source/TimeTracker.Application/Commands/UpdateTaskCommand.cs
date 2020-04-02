using System;
using TimeTracker.Application.Contracts;
using TimeTracker.Domain.BaseClasses;
using AsyncOperation = System.Threading.Tasks.Task;

namespace TimeTracker.Application.Commands
{
    /// <summary>
    /// Update the selected task.
    /// </summary>
    public class UpdateTaskCommand : DisposableBase, ICommand<Domain.Task>
    {
        /// <inheritdoc/>
        /// <param name="task">Nullable parameter.</param>
        public AsyncOperation Run(Domain.Task task)
        {
            throw new NotImplementedException();
        }
    }
}
