using System;
using Xamarin.Forms.PowerControls.Contracts;

namespace TimeTracker.Xamarin.Domains.Group
{
    /// <summary>
    /// Represents a group of task or a user story.
    /// </summary>
    public class GroupCellModel : BindableBase
    {
        public GroupCellModel() {}

        public GroupCellModel(string name, string code = null)
        {
            Name = name;
            Code = code;
        }

        public int Index { get; set; }

        /// <summary>
        /// The group id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Code used to identify the user story.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Name of the group.
        /// </summary>
        public string Name { get; set; }

        public TimeSpan ProjectedTime { get; set; }

        public TimeSpan ActualTime { get; set; }

        /// <summary>
        /// Indicates weather or not this group of task are billable.
        /// </summary>
        public bool Billable { get; set; }

        public bool ActionCanceled { get; set; }
    }
}
