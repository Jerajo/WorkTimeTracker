using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Application.Contracts;
using TimeTracker.DataAccess.Contracts;
using TimeTracker.Domain.BaseClasses;

namespace TimeTracker.Application.Queries
{
    /// <summary>
    /// Get the list of available tasks.
    /// </summary>
    public class GetTasksQuery : DisposableBase, IQuery<Domain.Task, List<Domain.Task>>
    {
        private readonly IDataRepository<ITask> _DataRepository;
        private readonly IMapper _Mapper;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="dataRepository">Handles data base operations.</param>
        /// <param name="mapper">Map objects.</param>
        public GetTasksQuery(IDataRepository<ITask> dataRepository,
            IMapper mapper)
        {
            _DataRepository = dataRepository;
            _Mapper = mapper;
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="queryOptions">Search query options.</param>
        /// <returns>List of available tasks.</returns>
        public Task<List<Domain.Task>> Run(Domain.Task queryOptions)
        {
            var taskEntities = _DataRepository.GetAll().ToList();

            var result = _Mapper.Map<List<Domain.Task>>(taskEntities);

            return Task.FromResult(result);
        }
    }
}
