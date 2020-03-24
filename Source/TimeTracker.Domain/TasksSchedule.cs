using System;
using TimeTracker.Domain.Contracts;

namespace TimeTracker.Domain
{
    /// <summary>
    /// Represent a schedule for a task.
    /// </summary>
    public class TasksSchedule : ITasksSchedule
    {
        /// <inheritdoc/>
        public int Id { get; set; }
        /// <inheritdoc/>
        public TimeSpan TimeStart { get; set; }
        /// <inheritdoc/>
        public TimeSpan? TimeEnd { get; set; }
        /// <inheritdoc/>
        public int TaskId { get; set; }
        /// <inheritdoc/>
        public int ScheduleId { get; set; }
        /// <inheritdoc/>
        public virtual Schedule Schedule { get; set; }
        /// <inheritdoc/>
        public virtual Task Task { get; set; }
    }
}
