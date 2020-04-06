using FluentAssertions;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Threading.Tasks;
using TimeTracker.Application.Commands;
using TimeTracker.Application.Dtos;
using TimeTracker.Application.Factories;

namespace TimeTracker.Application.UnitTests.Commands
{
    [TestClass]
    public class CreateTemplateCommandShould
    {
        private CreateTemplateCommand _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            var factory = AssemblyConfiguration.Kernel.Get<CommandFactory>();
            _sut = factory.GetInstance<CreateTemplateCommand>();
        }

        [TestMethod]
        public async Task GuardAgainstNull()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.Run(null));
        }

        [TestMethod]
        public void ValidateModelState()
        {
            var tempTemplate = new TaskExportTemplateDto()
            {
                Template = ""
            };

            Func<Task> function = () => _sut.Run(tempTemplate);

            function.Should().Throw<ValidationException>()
                .And.Errors.Should()
                .Contain(x => x.PropertyName == nameof(TaskExportTemplateDto.Template));
        }

        [TestMethod]
        public async Task CreateNewTemplate()
        {
            var tempTemplate = new TaskExportTemplateDto()
            {
                Template = nameof(CreateNewTemplate),
            };

            await _sut.Run(tempTemplate);
        }
    }
}
