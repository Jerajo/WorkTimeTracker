using System;
using TimeTracker.Application.Contracts;
using TimeTracker.Core.BaseClasses;
using AsyncOperation = System.Threading.Tasks.Task;

namespace TimeTracker.Application.Commands
{
    /// <summary>
    /// Track work time for selected task.
    /// </summary>
    public class TrackWorkTimeCommand : DisposableBase, ICommand<Domain.Task>
    {
        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="selectedTask">Selected task.</param>
        public AsyncOperation Run(Domain.Task selectedTask)
        {
            throw new NotImplementedException();
        }
    }
}
