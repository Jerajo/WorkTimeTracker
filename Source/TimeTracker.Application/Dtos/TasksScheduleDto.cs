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
        /// Id for the task.
        /// </summary>
        public int TaskId { get; set; }
    }
}
