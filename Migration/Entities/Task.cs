namespace Migration.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Task")]
    public partial class Task
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeSchedule> TimeSchedules { get; set; }
    }
}
