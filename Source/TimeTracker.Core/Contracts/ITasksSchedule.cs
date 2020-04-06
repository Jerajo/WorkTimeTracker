using System;

namespace TimeTracker.Core.Contracts
{
    /// <summary>
    /// Represent a schedule for a task.
    /// </summary>
    public interface ITasksSchedule : IEntity
    {
        /// <summary>
        /// Id for the task schedule.
        /// </summary>
        int? ScheduleId { get; set; }
        /// <summary>
        /// Related schedule for the task.
        /// </summary>
        Schedule Schedule { get; set; }
        /// <summary>
        /// Id for the task.
        /// </summary>
        int? TaskId { get; set; }
        /// <summary>
        /// Related task for the schedule.
        /// </summary>
        Task Task { get; set; }
        /// <summary>
        /// Completed task time.
        /// </summary>
        TimeSpan? TimeEnd { get; set; }
        /// <summary>
        /// Started work time.
        /// </summary>
        TimeSpan TimeStart { get; set; }
    }
}
