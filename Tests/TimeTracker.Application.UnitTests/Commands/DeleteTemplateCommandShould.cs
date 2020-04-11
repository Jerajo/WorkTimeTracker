using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Application.Commands;
using TimeTracker.Application.Dtos;
using TimeTracker.Application.Factories;
using TimeTracker.Application.Queries;

namespace TimeTracker.Application.UnitTests.Commands
{
    [TestClass]
    public class DeleteTemplateCommandShould
    {
        private DeleteTemplateCommand _sut;
        private CreateTemplateCommand _createTemplateCommand;
        private GetTemplatesQuery _getTemplateQuery;
        private IMapper _mapper;

        [TestInitialize]
        public void TestInitialize()
        {
            var commandFactory = AssemblyConfiguration.Kernel.Get<CommandFactory>();
            var queryFactory = AssemblyConfiguration.Kernel.Get<QueryFactory>();

            _mapper = AssemblyConfiguration.Kernel.Get<IMapper>();
            _createTemplateCommand = commandFactory.GetInstance<CreateTemplateCommand>();
            _getTemplateQuery = queryFactory.GetInstance<GetTemplatesQuery>();
            _sut = commandFactory.GetInstance<DeleteTemplateCommand>();
        }

        [TestMethod]
        public async Task GuardAgainstNull()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.ExecuteAsync(null));
        }

        [TestMethod]
        public void ValidateModelState()
        {
            var templateTask = new TaskExportTemplateDto();

            Func<Task> function = () => _sut.ExecuteAsync(templateTask);

            function.Should().Throw<ValidationException>()
                .And.Errors.Should()
                .Contain(x => x.PropertyName == nameof(TaskExportTemplateDto.Id));
        }

        [TestMethod]
        public async Task DeleteATemplate()
        {
            var tempTemplate = new TaskExportTemplateDto() { Template = nameof(DeleteATemplate) };

            await _createTemplateCommand.ExecuteAsync(tempTemplate);

            var result = await _getTemplateQuery.Run(x => x.Template == tempTemplate.Template);

            var task = result.First();

            tempTemplate = _mapper.Map<TaskExportTemplateDto>(task);

            await _sut.ExecuteAsync(tempTemplate);
        }
    }
}
