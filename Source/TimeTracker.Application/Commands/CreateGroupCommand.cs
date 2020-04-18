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
    public class CreateGroupCommand : BaseCommandDisableAbleAsync<GroupDto>
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
        public CreateGroupCommand(IRepositoryFactory repositoryFactory,
            Contracts.IValidatorFactory validatorFactory,
            IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _validatorFactory = validatorFactory;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        /// <param name="group">Nullable parameter.</param>
        /// <exception cref="ArgumentNullException"/>
        public override Task<bool> CanExecuteAsync(GroupDto group)
        {
            Guard.Against.Null(group, nameof(group));

            // TODO: Implement logic.
            return Task.FromResult(true);
        }

        /// <inheritdoc/>
        /// <param name="group">Nullable parameter.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ValidationException"/>
        public override AsyncOperation ExecuteAsync(GroupDto group)
        {
            if (!CanExecute(group))
                return Task.CompletedTask;

            var validator = _validatorFactory.GetInstance<GroupValidator>();

            validator.ValidateAndThrow(group);

            var repository = _repositoryFactory.GetRepository<Core.Group>();

            using (var transaction = repository.GetTransaction())
            {
                var taskEntity = _mapper.Map<Core.Group>(group);

                repository.Add(taskEntity);

                return transaction.CommitAsync();
            }
        }
    }
}
