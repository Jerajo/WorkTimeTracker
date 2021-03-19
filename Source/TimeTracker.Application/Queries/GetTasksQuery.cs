using Ardalis.GuardClauses;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeTracker.Application.Contracts;
using TimeTracker.Core.BaseClasses;
using TimeTracker.Core.Contracts;
using Async = System.Threading.Tasks;

namespace TimeTracker.Application.Queries
{
    /// <summary>
    /// Get the list of available tasks.
    /// </summary>
    public class GetTasksQuery : DisposableBase,
        IQuery<Func<Core.Task, bool>, List<Domain.Task>>
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="repositoryFactory">Handles data base operations.</param>
        /// <param name="mapper">Map objects.</param>
        public GetTasksQuery(IRepositoryFactory repositoryFactory,
            IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="query">Search query options.</param>
        /// <returns>List of available tasks.</returns>
        /// <exception cref="ArgumentException"/>
        public Async.Task<List<Domain.Task>> ExecuteAsync(Func<Core.Task, bool> query)
        {
            Guard.Against.Null(query, nameof(query));

            var repository = _repositoryFactory.GetRepository<Core.Task>();
            var taskEntities = repository.Query(query).ToList();

            var result = _mapper.Map<List<Domain.Task>>(taskEntities);

            return Async.Task.FromResult(result);
        }
    }
}
