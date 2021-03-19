using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Threading.Tasks;
using TimeTracker.Application.Commands;
using TimeTracker.Application.Queries;

namespace TimeTracker.Application.UnitTests.Queries
{
    [TestClass]
    public class GetCurrentScheduleQueryShould
    {
        private CreateScheduleCommand _createScheduleCommand;
        private GetCurrentScheduleQuery _sut;

        [TestInitialize]
        public void TextInitialize()
        {
            var kernel = AssemblyConfiguration.Kernel;
            _createScheduleCommand = kernel.Get<CreateScheduleCommand>();
            _sut = kernel.Get<GetCurrentScheduleQuery>();
        }

        [TestMethod]
        public async Task GetCurrentSchedule()
        {
            await _createScheduleCommand.ExecuteAsync(new Dtos.ScheduleDto
            {
                ScheduleDate = DateTimeOffset.Now.Date
            });

            var result = await _sut.ExecuteAsync();

            result.Should().BeAssignableTo<Domain.Schedule>()
                .And.NotBeNull();
        }
    }
}
