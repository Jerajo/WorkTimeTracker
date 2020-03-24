using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeTracker.Infrastructure.Entities;

namespace Migration.Entities
{

    [Table("UserStory")]
    public partial class UserStory
    {
        public UserStory()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }

        public int Code { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public int StoryPoints { get; set; }

        public virtual ICollection<Task> Tasks { get; }
    }
}
