using System;
using System.Collections.Generic;

namespace TimeTracker.Domain
{
    /// <summary>
    /// Represents a day of work.
    /// </summary>
    public interface ISchedule
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// Day of work.
        /// </summary>
        DateTimeOffset ScheduleDate { get; set; }
        /// <summary>
        /// Related tasks of the schedule.
        /// </summary>
        ICollection<Task> Tasks { get; }
    }
}