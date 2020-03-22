using System;
using System.ComponentModel.DataAnnotations.Schema;
using TimeTracker.Infrastructure.Entities;

namespace Migration.Entities
{

    [Table("TimeSchedule")]
    public partial class TimeSchedule
    {
        public int Id { get; set; }

        public TimeSpan TimeStart { get; set; }

        public TimeSpan? TimeEnd { get; set; }

        public int TaskId { get; set; }

        public int ScheduleId { get; set; }

        public virtual Schedule Schedule { get; set; }

        public virtual Task Task { get; set; }
    }
}
