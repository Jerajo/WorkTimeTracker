using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTracker.Application.Commands;
using TimeTracker.Application.Dtos;
using TimeTracker.Application.Queries;

namespace TimeTracker.Application.UnitTests.Queries
{
    [TestClass]
    public class GetTemplatesQueryShould
    {
        private CreateTemplateCommand _createScheduleCommand;
        private GetTemplatesQuery _sut;

        [TestInitialize]
        public void TextInitialize()
        {
            var kernel = AssemblyConfiguration.Kernel;
            _createScheduleCommand = kernel.Get<CreateTemplateCommand>();
            _sut = kernel.Get<GetTemplatesQuery>();
        }

        [TestMethod]
        public void GuardAgainstNullArguments()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _sut.ExecuteAsync(null));
        }

        [TestMethod]
        public async Task ReturnAEmptyListOfTemplates()
        {
            var result = await _sut.ExecuteAsync(x => x.Id < 0);

            result.Should().BeAssignableTo<List<TaskExportTemplateDto>>()
                .And.BeEmpty();
        }

        [TestMethod]
        public async Task ReturnAListOfTemplates()
        {
            await _createScheduleCommand.ExecuteAsync(new TaskExportTemplateDto
            {
                Template = nameof(ReturnAListOfTemplates)
            });

            var result = await _sut.ExecuteAsync(x => true);

            result.Should().BeAssignableTo<List<TaskExportTemplateDto>>()
                .And.NotBeEmpty();
        }
    }
}
