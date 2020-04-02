using TimeTracker.Core.BaseClasses;
using TimeTracker.Core.Contracts;

namespace TimeTracker.Core.ValueObjects
{
    /// <summary>
    /// Represents the template to be use when the schedule is exported.
    /// </summary>
    public partial class Description : EntityBase, IDescription
    {
        /// <inheritdoc/>
        public string Template { get; set; }
    }
}