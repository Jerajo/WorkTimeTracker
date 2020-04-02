using System;
using System.Collections.Generic;
using TimeTracker.Core.BaseClasses;
using TimeTracker.Core.Contracts;

namespace TimeTracker.Core
{
    /// <summary>
    /// Represents a day of work.
    /// </summary>
    public class Schedule : EntityBase, ISchedule
    {
        /// <inheritdoc/>
        public DateTimeOffset ScheduleDate { get; set; }
        /// <inheritdoc/>
        public ICollection<TasksSchedule> TasksSchedules { get; } = new List<TasksSchedule>();
    }
}
