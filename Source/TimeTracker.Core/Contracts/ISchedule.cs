﻿using System;
using System.Collections.Generic;

namespace TimeTracker.Core.Contracts
{
    /// <summary>
    /// Represents a day of work.
    /// </summary>
    public interface ISchedule : IEntity
    {
        /// <summary>
        /// Day of work.
        /// </summary>
        DateTimeOffset ScheduleDate { get; set; }
        /// <summary>
        /// Related tasks of the schedule.
        /// </summary>
        List<TasksSchedule> TasksSchedules { get; }
    }
}
