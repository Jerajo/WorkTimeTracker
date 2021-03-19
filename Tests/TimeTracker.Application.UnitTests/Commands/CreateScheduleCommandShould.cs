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
    public class CreateScheduleCommandShould
    {
        private CreateScheduleCommand _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            var factory = AssemblyConfiguration.Kernel.Get<CommandFactory>();
            _sut = factory.GetInstance<CreateScheduleCommand>();
        }

        [TestMethod]
        public async Task GuardAgainstNull()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.ExecuteAsync(null));
        }

        [TestMethod]
        public void ValidateModelState()
        {
            var tempSchedule = new ScheduleDto()
            {
                ScheduleDate = DateTimeOffset.Now.AddDays(-1).Date,
            };

            Func<Task> function = () => _sut.ExecuteAsync(tempSchedule);

            function.Should().Throw<ValidationException>()
                .And.Errors.Should()
                .Contain(x => x.PropertyName == nameof(ScheduleDto.ScheduleDate));
        }

        [TestMethod]
        public async Task CreateNewGroup()
        {
            var tempSchedule = new ScheduleDto()
            {
                ScheduleDate = DateTimeOffset.Now.Date,
            };

            await _sut.ExecuteAsync(tempSchedule);
        }
    }
}
