using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTracker.Infrastructure.Entities
{

    [Table("TimeSchedule")]
    [AutoMap(typeof(Domain.TasksSchedule))]
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
