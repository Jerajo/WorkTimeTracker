using Migration.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTracker.Infrastructure.Entities
{

    [Table("Schedule")]
    public partial class Schedule
    {
        public Schedule()
        {
            TimeSchedules = new HashSet<TimeSchedule>();
        }

        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public virtual ICollection<TimeSchedule> TimeSchedules { get; }
    }
}
