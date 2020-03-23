using System;
using System.Threading.Tasks;

namespace TimeTracker.Application.Contracts
{
    /// <summary>
    /// Represents an async query.
    /// </summary>
    /// <typeparam name="TOptions">Query options.</typeparam>
    /// <typeparam name="TResult">Query result.</typeparam>
    public interface IQuery<in TOptions, TResult> : IQuery
        where TOptions : class
        where TResult : class
    {
        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="param">Nullable parameter.</param>
        /// <returns><typeparamref name="TResult"/></returns>
        Task<TResult> Run(TOptions param);
    }

    /// <summary>
    /// Represent a query object
    /// </summary>
    public interface IQuery : IDisposable { }
}
