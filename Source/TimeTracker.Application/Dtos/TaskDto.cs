using System.Collections.Generic;
using TimeTracker.Core.BaseClasses;

namespace TimeTracker.Application.Dtos
{
    /// <summary>
    /// Represents a task of work.
    /// </summary>
    public class TaskDto : EntityBase
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
        /// Id for the task description.
        /// </summary>
        public int? DescriptionId { get; set; }
        /// <summary>
        /// Template to be use in the Excel document.
        /// </summary>
        public TaskExportTemplateDto Description { get; set; }
        /// <summary>
        /// Id for the task group.
        /// </summary>
        public int? GroupId { get; set; }
        /// <summary>
        /// Related group for the task.
        /// </summary>
        public GroupDto Group { get; set; }
        /// <summary>
        /// Relates schedules of the task.
        /// </summary>
        public List<TasksScheduleDto> TasksSchedules { get; } = new List<TasksScheduleDto>();
    }
}
