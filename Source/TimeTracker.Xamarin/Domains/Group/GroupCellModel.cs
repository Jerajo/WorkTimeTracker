using System;
using Xamarin.Forms.PowerControls.Contracts;

namespace TimeTracker.Xamarin.Domains.Group
{
    /// <summary>
    /// Represents a group of task or a user story.
    /// </summary>
    public class GroupCellModel : BindableBase
    {
        /// <summary>
        /// Code use in to identify the user story.
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Name of the group.
        /// </summary>
        public string Name { get; set; }
        public TimeSpan ProjectedTime { get; set; }
        public TimeSpan ActualTime { get; set; }
        public bool Billable { get; set; }
    }
}
