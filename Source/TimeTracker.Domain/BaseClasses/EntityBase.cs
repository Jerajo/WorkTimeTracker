using TimeTracker.Domain.Contracts;

namespace TimeTracker.Domain.BaseClasses
{
    /// <summary>
    /// Represents a entity from the data base.
    /// </summary>
    public class EntityBase : IEntity
    {
        /// <inheritdoc/>
        public int Id { get; set; }
    }
}
