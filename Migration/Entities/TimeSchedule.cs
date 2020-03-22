namespace Migration.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
