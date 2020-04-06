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
    /// Get the list of available groups.
    /// </summary>
    public class GetGroupsQuery : DisposableBase,
        IQuery<Func<Core.Group, bool>, List<Domain.Group>>
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="repositoryFactory">Handles data base operations.</param>
        /// <param name="mapper">Map objects.</param>
        public GetGroupsQuery(IRepositoryFactory repositoryFactory,
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
        public Task<List<Domain.Group>> Run(Func<Core.Group, bool> query)
        {
            Guard.Against.Null(query, nameof(query));

            var repository = _repositoryFactory.GetRepository<Core.Group>();
            var taskEntities = repository.Query(query).ToList();

            var result = _mapper.Map<List<Domain.Group>>(taskEntities);

            return Task.FromResult(result);
        }
    }
}
