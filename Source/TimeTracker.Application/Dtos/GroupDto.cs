using System.Collections.Generic;
using TimeTracker.Core.BaseClasses;

namespace TimeTracker.Application.Dtos
{
    /// <summary>
    /// Represents a group of task or a user story.
    /// </summary>
    public class GroupDto : EntityBase
    {
        /// <summary>
        /// Code use in to identify the user story.
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Name of the group.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Related tasks of the group.
        /// </summary>
        public List<TaskDto> Tasks { get; } = new List<TaskDto>();
    }
}
