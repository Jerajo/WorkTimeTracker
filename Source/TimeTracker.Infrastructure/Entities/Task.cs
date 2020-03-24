using AutoMapper;
using Migration.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeTracker.DataAccess.Contracts;

namespace TimeTracker.Infrastructure.Entities
{

    [Table("Task")]
    [AutoMap(typeof(Domain.Task))]
    public partial class Task : ITask
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

        public int? DescriptionId { get; set; }

        public Description Description { get; set; }

        public int? UserStoryId { get; set; }

        public virtual UserStory UserStory { get; set; }

        public virtual ICollection<TimeSchedule> TimeSchedules { get; }
    }
}
