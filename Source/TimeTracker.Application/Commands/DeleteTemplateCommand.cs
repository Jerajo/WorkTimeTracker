using Ardalis.GuardClauses;
using AutoMapper;
using FluentValidation;
using Murk.Command;
using System;
using System.Threading.Tasks;
using TimeTracker.Application.Dtos;
using TimeTracker.Application.Validators;
using TimeTracker.Core.Contracts;
using TimeTracker.Core.ValueObjects;

namespace TimeTracker.Application.Commands
{
    /// <summary>
    /// Delete the selected description.
    /// </summary>
    public class DeleteTemplateCommand : BaseCommandDisableAbleAsync<TaskExportTemplateDto>
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
        public DeleteTemplateCommand(IRepositoryFactory repositoryFactory,
            Contracts.IValidatorFactory validatorFactory,
            IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _validatorFactory = validatorFactory;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        /// <param name="template">Nullable parameter.</param>
        /// <exception cref="ArgumentNullException"/>
        public override Task<bool> CanExecuteAsync(TaskExportTemplateDto template)
        {
            Guard.Against.Null(template, nameof(template));

            // TODO: Implement logic.
            return Task.FromResult(true);
        }

        /// <inheritdoc/>
        /// <param name="template">Nullable parameter.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ValidationException"/>
        public override Task ExecuteAsync(TaskExportTemplateDto template)
        {
            if (!CanExecute(template))
                return Task.CompletedTask;

            var validator = _validatorFactory.GetInstance<TemplateValidator>();

            validator.ValidateAndThrow(template, nameof(TaskExportTemplateDto.Id));

            var repository = _repositoryFactory.GetRepository<Description>();

            using (var transaction = repository.GetTransaction())
            {
                var templateEntity = _mapper.Map<Description>(template);

                repository.Delete(templateEntity);

                return transaction.CommitAsync();
            }
        }
    }
}
