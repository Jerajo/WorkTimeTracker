using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Migration.Entities
{

    [Table("Task")]
    public partial class Task
    {
        public Task()
        {
            TimeSchedules = new HashSet<TimeSchedule>();
        }

        public int Id { get; set; }

        public int Code { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public TimeSpan? ProjectedEffort { get; set; }

        public int? UserStoryId { get; set; }

        public virtual UserStory UserStory { get; set; }

        public virtual ICollection<TimeSchedule> TimeSchedules { get; }
    }
}
