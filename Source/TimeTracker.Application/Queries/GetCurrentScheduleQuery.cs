using AutoMapper;
using System;
using System.Threading.Tasks;
using TimeTracker.Application.Contracts;
using TimeTracker.Core.BaseClasses;
using TimeTracker.Core.Contracts;

namespace TimeTracker.Application.Queries
{
    /// <summary>
    /// Get the current day schedule.
    /// </summary>
    public class GetCurrentScheduleQuery : DisposableBase,
        IQuery<object, Domain.Schedule>
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="repositoryFactory">Handles data base operations.</param>
        /// <param name="mapper">Map objects.</param>
        public GetCurrentScheduleQuery(IRepositoryFactory repositoryFactory,
            IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="param">Search query options.</param>
        /// <returns>Selected Schedule.</returns>
        public Task<Domain.Schedule> Run(object param = null)
        {
            var repository = _repositoryFactory.GetRepository<Core.Schedule>();
            var taskEntities = repository.Get(x => x.ScheduleDate.Date == DateTimeOffset.Now.Date);

            var result = _mapper.Map<Domain.Schedule>(taskEntities);

            return Task.FromResult(result);
        }
    }
}
