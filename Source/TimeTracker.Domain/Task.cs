using System.Collections.Generic;
using TimeTracker.Domain.ValueObjects;

namespace TimeTracker.Domain
{
    /// <summary>
    /// Represents a task of work.
    /// </summary>
    public class Task : ITask
    {
        /// <inheritdoc/>
        public int Id { get; set; }
        /// <inheritdoc/>
        public string Code { get; set; }
        /// <inheritdoc/>
        public string Name { get; set; }
        /// <inheritdoc/>
        public int DescriptionId { get; set; }
        /// <inheritdoc/>
        public Description Description { get; set; }
        /// <inheritdoc/>
        public int GroupId { get; set; }
        /// <inheritdoc/>
        public Group Group { get; set; }
        /// <inheritdoc/>
        public ICollection<Schedule> Schedules { get; } = new List<Schedule>();
    }
}
