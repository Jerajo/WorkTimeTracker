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

        public int Id { get; set; }
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
