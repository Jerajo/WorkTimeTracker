namespace TimeTracker.Core.Contracts
{
    /// <summary>
    /// Represents a entity of the data base.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        int Id { get; set; }
    }
}
