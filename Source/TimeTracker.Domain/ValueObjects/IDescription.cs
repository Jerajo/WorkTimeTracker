namespace TimeTracker.Domain.ValueObjects
{
    /// <summary>
    /// Represents the template to be use when the schedule is exported.
    /// </summary>
    public interface IDescription
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// Template to be use in the Excel document.
        /// </summary>
        string Template { get; set; }
    }
}