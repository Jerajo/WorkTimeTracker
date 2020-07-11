using Ardalis.GuardClauses;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Application.Contracts;
using TimeTracker.Application.Dtos;
using TimeTracker.Core.BaseClasses;
using TimeTracker.Core.Contracts;
using TimeTracker.Core.ValueObjects;

namespace TimeTracker.Application.Queries
{
    /// <summary>
    /// Get the list of available templates.
    /// </summary>
    public class GetTemplatesQuery : DisposableBase,
        IQuery<Func<Description, bool>, List<TaskExportTemplateDto>>
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="repositoryFactory">Handles data base operations.</param>
        /// <param name="mapper">Map objects.</param>
        public GetTemplatesQuery(IRepositoryFactory repositoryFactory,
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
        public Task<List<TaskExportTemplateDto>> ExecuteAsync(Func<Description, bool> query)
        {
            Guard.Against.Null(query, nameof(query));

            var repository = _repositoryFactory.GetRepository<Description>();
            var taskEntities = repository.Query(query).ToList();

            var result = _mapper.Map<List<TaskExportTemplateDto>>(taskEntities);

            return Task.FromResult(result);
        }
    }
}
