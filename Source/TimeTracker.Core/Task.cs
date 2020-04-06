using System.Collections.Generic;
using TimeTracker.Core.BaseClasses;
using TimeTracker.Core.Contracts;
using TimeTracker.Core.ValueObjects;

namespace TimeTracker.Core
{
    /// <summary>
    /// Represents a task of work.
    /// </summary>
    public class Task : EntityBase, ITask
    {
        /// <inheritdoc/>
        public string Code { get; set; }
        /// <inheritdoc/>
        public string Name { get; set; }
        /// <inheritdoc/>
        public int? DescriptionId { get; set; }
        /// <inheritdoc/>
        public virtual Description Description { get; set; }
        /// <inheritdoc/>
        public int? GroupId { get; set; }
        /// <inheritdoc/>
        public virtual Group Group { get; set; }
        /// <inheritdoc/>
        public virtual List<TasksSchedule> TasksSchedules { get; } = new List<TasksSchedule>();
    }
}
