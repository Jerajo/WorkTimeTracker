using FluentAssertions;
using FluentAssertions.Execution;
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
    public class UpdateTemplateCommandShould
    {
        private UpdateTemplateCommand _sut;
        private CreateTemplateCommand _createTemplateCommand;
        private GetTemplatesQuery _getTemplateQuery;

        [TestInitialize]
        public void TestInitialize()
        {
            var commandFactory = AssemblyConfiguration.Kernel.Get<CommandFactory>();
            var queryFactory = AssemblyConfiguration.Kernel.Get<QueryFactory>();

            _createTemplateCommand = commandFactory.GetInstance<CreateTemplateCommand>();
            _getTemplateQuery = queryFactory.GetInstance<GetTemplatesQuery>();
            _sut = commandFactory.GetInstance<UpdateTemplateCommand>();
        }

        [TestMethod]
        public async Task GuardAgainstNull()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.ExecuteAsync(null));
        }

        [TestMethod]
        public void ValidateModelState()
        {
            var tempGroup = new TaskExportTemplateDto();

            Func<Task> function = () => _sut.ExecuteAsync(tempGroup);

            using (new AssertionScope())
            {
                function.Should().Throw<ValidationException>()
                    .And.Errors.Should()
                    .Contain(x => x.PropertyName == nameof(TaskExportTemplateDto.Id));

                function.Should().Throw<ValidationException>()
                    .And.Errors.Should()
                    .Contain(x => x.PropertyName == nameof(TaskExportTemplateDto.Template));
            }
        }

        [TestMethod]
        public async Task UpdateATemplate()
        {
            var tempTemplate = new TaskExportTemplateDto() { Template = nameof(UpdateATemplate) };

            await _createTemplateCommand.ExecuteAsync(tempTemplate);

            var result = await _getTemplateQuery.Run(x => x.Template == tempTemplate.Template);

            var description = result.First();

            description.Template += " updated";

            await _sut.ExecuteAsync(description);
        }
    }
}
