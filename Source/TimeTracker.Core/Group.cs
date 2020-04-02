using System.Collections.Generic;
using TimeTracker.Core.BaseClasses;
using TimeTracker.Core.Contracts;

namespace TimeTracker.Core
{
    /// <summary>
    /// Represents a group of task or a user story.
    /// </summary>
    public class Group : EntityBase, IGroup
    {
        /// <inheritdoc/>
        public string Code { get; set; }
        /// <inheritdoc/>
        public string Name { get; set; }
        /// <inheritdoc/>
        public ICollection<Task> Tasks { get; } = new List<Task>();
    }
}
