using Ardalis.GuardClauses;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Application.Contracts;
using TimeTracker.Core.BaseClasses;
using TimeTracker.Core.Contracts;

namespace TimeTracker.Application.Queries
{
    /// <summary>
    /// Get the list of available schedules.
    /// </summary>
    public class GetSchedulesQuery : DisposableBase,
        IQuery<Func<Core.Schedule, bool>, List<Domain.Schedule>>
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="repositoryFactory">Handles data base operations.</param>
        /// <param name="mapper">Map objects.</param>
        public GetSchedulesQuery(IRepositoryFactory repositoryFactory,
            IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="query">Search query options.</param>
        /// <returns>List of available groups.</returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<List<Domain.Schedule>> ExecuteAsync(Func<Core.Schedule, bool> query)
        {
            Guard.Against.Null(query, nameof(query));

            var repository = _repositoryFactory.GetRepository<Core.Schedule>();
            var taskEntities = repository.Query(query).ToList();

            var result = _mapper.Map<List<Domain.Schedule>>(taskEntities);

            return Task.FromResult(result);
        }
    }
}
