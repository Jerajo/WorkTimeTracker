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
    public class GetSchedulesQueryShould
    {
        private CreateScheduleCommand _createScheduleCommand;
        private GetSchedulesQuery _sut;

        [TestInitialize]
        public void TextInitialize()
        {
            var kernel = AssemblyConfiguration.Kernel;
            _createScheduleCommand = kernel.Get<CreateScheduleCommand>();
            _sut = kernel.Get<GetSchedulesQuery>();
        }

        [TestMethod]
        public void GuardAgainstNullArguments()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _sut.ExecuteAsync(null));
        }

        [TestMethod]
        public async Task ReturnAEmptyListOfSchedules()
        {
            var result = await _sut.ExecuteAsync(x => x.Id < 0);

            result.Should().BeAssignableTo<List<Domain.Schedule>>()
                .And.BeEmpty();
        }

        [TestMethod]
        public async Task ReturnAListOfSchedules()
        {
            await _createScheduleCommand.ExecuteAsync(new ScheduleDto
            {
                ScheduleDate = DateTime.Now.Date
            });

            var result = await _sut.ExecuteAsync(x => true);

            result.Should().BeAssignableTo<List<Domain.Schedule>>()
                .And.NotBeEmpty();
        }
    }
}
