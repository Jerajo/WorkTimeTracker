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
using TimeTracker.Core.ValueObjects;

namespace TimeTracker.Application.Commands
{
    /// <summary>
    /// Create a new template for exporting task to Excel document.
    /// </summary>
    public class CreateTemplateCommand : DisposableBase, ICommand<TaskExportTemplateDto>
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
        public CreateTemplateCommand(IRepositoryFactory repositoryFactory,
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
        /// <exception cref="ValidationException"/>
        public Task Run(TaskExportTemplateDto template)
        {
            Guard.Against.Null(template, nameof(template));

            var validator = _validatorFactory.GetInstance<TemplateValidator>();

            validator.ValidateAndThrow(template);

            var repository = _repositoryFactory.GetRepository<Description>();

            using (var transaction = repository.GetTransaction())
            {
                var templateEntity = _mapper.Map<Description>(template);

                repository.Add(templateEntity);

                return transaction.CommitAsync();
            }
        }
    }
}
