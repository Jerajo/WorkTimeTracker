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
        /// <summary>
        /// Indicates weather or not the group is deleted.
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
