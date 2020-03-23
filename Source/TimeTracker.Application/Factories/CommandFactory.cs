using System;
using TimeTracker.Application.Contracts;

namespace TimeTracker.Application.Factories
{
    /// <summary>
    /// Create an instance of an async command..
    /// </summary>
    /// <typeparam name="T">Query options.</typeparam>
    public class CommandFactory<T> : ICommandFactory<T>
        where T : class
    {
        /// <inheritdoc/>
        public ICommand<T> GetInstance()
        {
            throw new NotImplementedException();
        }
    }
}
