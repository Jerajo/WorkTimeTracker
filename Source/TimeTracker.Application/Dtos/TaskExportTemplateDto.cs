using TimeTracker.Core.BaseClasses;

namespace TimeTracker.Application.Dtos
{
    /// <summary>
    /// Represents the template to be use when the schedule is exported.
    /// </summary>
    public class TaskExportTemplateDto : EntityBase
    {
        /// <summary>
        /// Template to be use in the Excel document.
        /// </summary>
        public string Template { get; set; }
    }
}
