using TimeTracker.Core.Contracts;

namespace TimeTracker.Core.BaseClasses
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
