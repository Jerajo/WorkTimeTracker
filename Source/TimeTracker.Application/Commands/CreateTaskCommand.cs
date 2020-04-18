using Ardalis.GuardClauses;
using AutoMapper;
using FluentValidation;
using Murk.Command;
using System;
using System.Threading.Tasks;
using TimeTracker.Application.Dtos;
using TimeTracker.Application.Validators;
using TimeTracker.Core.Contracts;
using AsyncOperation = System.Threading.Tasks.Task;

namespace TimeTracker.Application.Commands
{
    /// <summary>
    /// Create a new task.
    /// </summary>
    public class CreateTaskCommand : BaseCommandDisableAbleAsync<TaskDto>
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
        public CreateTaskCommand(IRepositoryFactory repositoryFactory,
            Contracts.IValidatorFactory validatorFactory,
            IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _validatorFactory = validatorFactory;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        /// <param name="task">Nullable parameter.</param>
        /// <exception cref="ArgumentNullException"/>
        public override Task<bool> CanExecuteAsync(TaskDto task)
        {
            Guard.Against.Null(task, nameof(task));

            // TODO: Implement logic.
            return Task.FromResult(true);
        }

        /// <inheritdoc/>
        /// <param name="task">Nullable parameter.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ValidationException"/>
        public override AsyncOperation ExecuteAsync(TaskDto task)
        {
            if (!CanExecute(task))
                return Task.CompletedTask;

            var validator = _validatorFactory.GetInstance<TaskValidator>();

            validator.ValidateAndThrow(task);

            var repository = _repositoryFactory.GetRepository<Core.Task>();

            using (var transaction = repository.GetTransaction())
            {
                var taskEntity = _mapper.Map<Core.Task>(task);

                repository.Add(taskEntity);

                return transaction.CommitAsync();
            }
        }
    }
}
