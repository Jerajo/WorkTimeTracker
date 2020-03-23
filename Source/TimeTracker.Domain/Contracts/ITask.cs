using System.Collections.Generic;
using TimeTracker.Domain.ValueObjects;

namespace TimeTracker.Domain.Contracts
{
    /// <summary>
    /// Represents a task of work.
    /// </summary>
    public interface ITask
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
        /// Id for the task description.
        /// </summary>
        int DescriptionId { get; set; }
        /// <summary>
        /// Related description for the task.
        /// </summary>
        Description Description { get; set; }
        /// <summary>
        /// Id for the task group.
        /// </summary>
        int GroupId { get; set; }
        /// <summary>
        /// Related group for the task.
        /// </summary>
        Group Group { get; set; }
        /// <summary>
        /// Relates schedules of the task.
        /// </summary>
        ICollection<Schedule> Schedules { get; }
    }
}