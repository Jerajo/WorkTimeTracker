using System;
using TimeTracker.Core.BaseClasses;

namespace TimeTracker.Application.Dtos
{
    /// <summary>
    /// Represent a schedule for a task.
    /// </summary>
    public class TasksScheduleDto : EntityBase
    {
        /// <summary>
        /// Working duration time.
        /// </summary>
        public TimeSpan Duration { get; set; }
        /// <summary>
        /// Id for the task schedule.
        /// </summary>
        public int ScheduleId { get; set; }
        /// <summary>
        /// Related schedule for the task.
        /// </summary>
        public ScheduleDto Schedule { get; set; }
        /// <summary>
        /// Id for the task.
        /// </summary>
        public int TaskId { get; set; }
        /// <summary>
        /// Related task for the schedule.
        /// </summary>
        public TaskDto Task { get; set; }
    }
}
