using System;
using System.Threading.Tasks;

namespace TimeTracker.Application.Contracts
{
    /// <summary>
    /// Represents a async command.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface ICommand<in T> : ICommand where T : class
    {
        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="param">Nullable parameter.</param>
        /// <returns><code>System.Threading.Tasks.Task</code></returns>
        Task Run(T param);
    }

    /// <summary>
    /// Represents a command object.
    /// </summary>
    public interface ICommand : IDisposable { }

}
