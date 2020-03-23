namespace TimeTracker.Application.Contracts
{
    /// <summary>
    /// Create an instance of an async command..
    /// </summary>
    public interface ICommandFactory
    {
        /// <summary>
        /// Get an instance of an async command.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        T GetInstance<T>() where T : ICommand;
    }
}
