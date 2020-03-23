using System;

namespace TimeTracker.Application.Contracts
{
    /// <summary>
    /// Represents a async command.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface ICommand<in T> where T : class
    {
        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="param">Nullable parameter.</param>
        /// <returns><code>System.IAsyncResult</code></returns>
        IAsyncResult Run(T param);
    }
}
