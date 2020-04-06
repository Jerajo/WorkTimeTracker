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
    public class GetTasksScheduleQuery : DisposableBase,
        IQuery<Func<Core.TasksSchedule, bool>, List<Domain.TasksSchedule>>
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="repositoryFactory">Handles data base operations.</param>
        /// <param name="mapper">Map objects.</param>
        public GetTasksScheduleQuery(IRepositoryFactory repositoryFactory,
            IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="query">Search query options.</param>
        /// <returns>List of available tasksSchedules.</returns>
        public Task<List<Domain.TasksSchedule>> Run(Func<Core.TasksSchedule, bool> query)
        {
            Guard.Against.Null(query, nameof(query));

            var repository = _repositoryFactory.GetRepository<Core.TasksSchedule>();
            var taskEntities = repository.Query(query).ToList();

            var result = _mapper.Map<List<Domain.TasksSchedule>>(taskEntities);

            return Task.FromResult(result);
        }
    }
}
