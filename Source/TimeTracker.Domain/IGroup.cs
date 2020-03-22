using System.Collections.Generic;

namespace TimeTracker.Domain
{
    /// <summary>
    /// Represents a group of task or a user story.
    /// </summary>
    public interface IGroup
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// Code use in to identify the user story.
        /// </summary>
        string Code { get; set; }
        /// <summary>
        /// Name of the group.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Related tasks of the group.
        /// </summary>
        ICollection<Task> Tasks { get; }
    }
}