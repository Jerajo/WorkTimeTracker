using Ardalis.GuardClauses;
using AutoMapper;
using FluentValidation;
using System;
using System.Threading.Tasks;
using TimeTracker.Application.Contracts;
using TimeTracker.Application.Dtos;
using TimeTracker.Application.Validators;
using TimeTracker.Core.BaseClasses;
using TimeTracker.Core.Contracts;

namespace TimeTracker.Application.Commands
{
    /// <summary>
    /// Delete the selected schedule.
    /// </summary>
    public class DeleteScheduleCommand : DisposableBase, ICommand<ScheduleDto>
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly Contracts.IValidatorFactory _validatorFactory;
        private readonly IMapper _mapper;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="repositoryFactory">Handles data base operations.</param>
        /// <param name="validatorFactory">Validate models.</param>
        /// <param name="mapper">Map objects.</param>
        public DeleteScheduleCommand(IRepositoryFactory repositoryFactory,
            Contracts.IValidatorFactory validatorFactory,
            IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _validatorFactory = validatorFactory;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        /// <param name="schedule">Nullable parameter.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ValidationException"/>
        public Task Run(ScheduleDto schedule)
        {
            Guard.Against.Null(schedule, nameof(schedule));

            var validator = _validatorFactory.GetInstance<ScheduleValidator>();

            validator.ValidateAndThrow(schedule, nameof(ScheduleDto.Id));

            var repository = _repositoryFactory.GetRepository<Core.Schedule>();

            using (var transaction = repository.GetTransaction())
            {
                var scheduleEntity = _mapper.Map<Core.Schedule>(schedule);

                repository.Delete(scheduleEntity);

                return transaction.CommitAsync();
            }
        }
    }
}
