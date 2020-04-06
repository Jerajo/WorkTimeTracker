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
    /// Delete work time tracked for selected task.
    /// </summary>
    public class DeleteWorkTimeTrackedCommand : DisposableBase, ICommand<TasksScheduleDto>
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
        public DeleteWorkTimeTrackedCommand(IRepositoryFactory repositoryFactory,
            Contracts.IValidatorFactory validatorFactory,
            IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _validatorFactory = validatorFactory;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        /// <param name="taskSchedule">Nullable parameter.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ValidationException"/>
        public Task Run(TasksScheduleDto taskSchedule)
        {
            Guard.Against.Null(taskSchedule, nameof(taskSchedule));

            var validator = _validatorFactory.GetInstance<TasksScheduleValidator>();

            validator.ValidateAndThrow(taskSchedule, nameof(TasksScheduleDto.Id));

            var repository = _repositoryFactory.GetRepository<Core.TasksSchedule>();

            using (var transaction = repository.GetTransaction())
            {
                var taskEntity = _mapper.Map<Core.TasksSchedule>(taskSchedule);

                repository.Delete(taskEntity);

                return transaction.CommitAsync();
            }
        }
    }
}
