namespace TimeTracker.Application.Contracts
{
    /// <summary>
    /// Create an instance of an async command..
    /// </summary>
    /// <typeparam name="T">Query options.</typeparam>
    public interface ICommandFactory<in T> where T : class
    {
        /// <summary>
        /// Get an instance of an async command.
        /// </summary>
        ICommand<T> GetInstance();
    }
}
