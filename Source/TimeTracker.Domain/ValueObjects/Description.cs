using TimeTracker.Domain.Contracts;

namespace TimeTracker.Domain.ValueObjects
{
    /// <summary>
    /// Represents the template to be use when the schedule is exported.
    /// </summary>
    public class Description : IDescription
    {
        /// <inheritdoc/>
        public int Id { get; set; }
        /// <inheritdoc/>
        public string Template { get; set; }
    }
}