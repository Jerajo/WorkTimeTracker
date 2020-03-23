using System.Collections.Generic;
using TimeTracker.Domain.Contracts;

namespace TimeTracker.Domain
{
    /// <summary>
    /// Represents a group of task or a user story.
    /// </summary>
    public class Group : IGroup
    {
        /// <inheritdoc/>
        public int Id { get; set; }
        /// <inheritdoc/>
        public string Code { get; set; }
        /// <inheritdoc/>
        public string Name { get; set; }
        /// <inheritdoc/>
        public ICollection<Task> Tasks { get; } = new List<Task>();
    }
}
