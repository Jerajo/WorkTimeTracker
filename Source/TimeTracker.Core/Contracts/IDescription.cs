namespace TimeTracker.Core.Contracts
{
    /// <summary>
    /// Represents the template to be use when the schedule is exported.
    /// </summary>
    public interface IDescription : IEntity
    {
        /// <summary>
        /// Template to be use in the Excel document.
        /// </summary>
        string Template { get; set; }
    }
}