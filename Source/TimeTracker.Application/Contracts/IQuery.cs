using System.Threading.Tasks;

namespace TimeTracker.Application.Contracts
{
    /// <summary>
    /// Represents a async query.
    /// </summary>
    /// <typeparam name="TA">Query options.</typeparam>
    /// <typeparam name="TB">Query result.</typeparam>
    public interface IQuery<in TA, TB>
        where TA : class
        where TB : class
    {
        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="param">Nullable parameter.</param>
        /// <returns><typeparamref name="TB"/></returns>
        Task<TB> Run(TA param);
    }
}
