﻿using System;
using System.Collections.Generic;
using TimeTracker.Domain.Contracts;

namespace TimeTracker.Domain
{
    /// <summary>
    /// Represents a day of work.
    /// </summary>
    public class Schedule : ISchedule
    {
        /// <inheritdoc/>
        public int Id { get; set; }
        /// <inheritdoc/>
        public DateTimeOffset ScheduleDate { get; set; }
        /// <inheritdoc/>
        public ICollection<TasksSchedule> TasksSchedules { get; } = new List<TasksSchedule>();
    }
}
