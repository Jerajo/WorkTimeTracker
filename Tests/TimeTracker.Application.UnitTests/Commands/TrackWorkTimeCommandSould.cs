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
    public class TrackWorkTimeCommandSould
    {
        private GetCurrentScheduleQuery _getCurrentScheduleQuery;
        private CreateScheduleCommand _createScheduleCommand;
        private CreateTaskCommand _createTaskCommand;
        private GetTasksQuery _getTaskQuery;
        private TrackWorkTimeCommand _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            var commandFactory = AssemblyConfiguration.Kernel.Get<CommandFactory>();
            var queryFactory = AssemblyConfiguration.Kernel.Get<QueryFactory>();

            _getCurrentScheduleQuery = queryFactory.GetInstance<GetCurrentScheduleQuery>();
            _getTaskQuery = queryFactory.GetInstance<GetTasksQuery>();
            _createTaskCommand = commandFactory.GetInstance<CreateTaskCommand>();
            _createScheduleCommand = commandFactory.GetInstance<CreateScheduleCommand>();
            _sut = commandFactory.GetInstance<TrackWorkTimeCommand>();
        }

        [TestMethod]
        public async Task GuardAgainstNull()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.ExecuteAsync(null));
        }

        [TestMethod]
        public void ValidateModelState()
        {
            var tempTaskSchedule = new TasksScheduleDto()
            {
                Duration = TimeSpan.FromSeconds(-1),
            };

            Func<Task> function = () => _sut.ExecuteAsync(tempTaskSchedule);

            using (new AssertionScope())
            {
                function.Should().Throw<ValidationException>()
                    .And.Errors.Should()
                    .Contain(x => x.PropertyName == nameof(TasksScheduleDto.Duration));

                function.Should().Throw<ValidationException>()
                    .And.Errors.Should()
                    .Contain(x => x.PropertyName == nameof(TasksScheduleDto.TaskId));

                function.Should().Throw<ValidationException>()
                    .And.Errors.Should()
                    .Contain(x => x.PropertyName == nameof(TasksScheduleDto.ScheduleId));
            }
        }

        [TestMethod]
        public async Task TrackWorkTime()
        {
            await _createTaskCommand.ExecuteAsync(new TaskDto
            {
                Name = nameof(TrackWorkTime)
            });

            var tasks = await _getTaskQuery.ExecuteAsync(x => x.Name == nameof(TrackWorkTime));

            await _createScheduleCommand.ExecuteAsync(new ScheduleDto
            {
                ScheduleDate = DateTimeOffset.Now.Date
            });

            var task = tasks.First();
            var schedule = await _getCurrentScheduleQuery.ExecuteAsync();

            var tempTaskSchedule = new TasksScheduleDto()
            {
                Duration = TimeSpan.Zero,
                TaskId = task.Id,
                ScheduleId = schedule.Id,
            };

            await _sut.ExecuteAsync(tempTaskSchedule);
        }
    }
}
