using System;
using System.Collections.Generic;
using TimeTracker.Core.BaseClasses;

namespace TimeTracker.Application.Dtos
{
    /// <summary>
    /// Represents a day of work.
    /// </summary>
    public class ScheduleDto : EntityBase
    {
        /// <summary>
        /// Day of work.
        /// </summary>
        public DateTimeOffset ScheduleDate { get; set; }
        /// <summary>
        /// Related tasks of the schedule.
        /// </summary>
        public List<TasksScheduleDto> TasksSchedules { get; } = new List<TasksScheduleDto>();
    }
}
