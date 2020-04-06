using AutoMapper;
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
    public class StopWorkTimeTrackingCommandShould
    {
        private GetCurrentScheduleQuery _getCurrentScheduleQuery;
        private CreateScheduleCommand _createScheduleCommand;
        private GetTasksScheduleQuery _getTasksScheduleQuery;
        private TrackWorkTimeCommand _trackWorkTimeCommand;
        private CreateTaskCommand _createTaskCommand;
        private StopWorkTimeTrackingCommand _sut;
        private GetTasksQuery _getTasksQuery;
        private IMapper _mapper;

        [TestInitialize]
        public void TestInitialize()
        {
            var commandFactory = AssemblyConfiguration.Kernel.Get<CommandFactory>();
            var queryFactory = AssemblyConfiguration.Kernel.Get<QueryFactory>();
            _mapper = AssemblyConfiguration.Kernel.Get<IMapper>();

            _getCurrentScheduleQuery = queryFactory.GetInstance<GetCurrentScheduleQuery>();
            _getTasksQuery = queryFactory.GetInstance<GetTasksQuery>();
            _getTasksScheduleQuery = queryFactory.GetInstance<GetTasksScheduleQuery>();
            _createTaskCommand = commandFactory.GetInstance<CreateTaskCommand>();
            _createScheduleCommand = commandFactory.GetInstance<CreateScheduleCommand>();
            _trackWorkTimeCommand = commandFactory.GetInstance<TrackWorkTimeCommand>();
            _sut = commandFactory.GetInstance<StopWorkTimeTrackingCommand>();
        }

        [TestMethod]
        public async Task GuardAgainstNull()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.Run(null));
        }

        [TestMethod]
        public void ValidateModelState()
        {
            var tempTaskSchedule = new TasksScheduleDto()
            {
                Duration = TimeSpan.FromSeconds(-1),
            };

            Func<Task> function = () => _sut.Run(tempTaskSchedule);

            using (new AssertionScope())
            {
                function.Should().Throw<ValidationException>()
                    .And.Errors.Should()
                    .Contain(x => x.PropertyName == nameof(TasksScheduleDto.Id));

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
        public async Task DeleteWorkTimeTracked()
        {
            await _createTaskCommand.Run(new TaskDto
            {
                Name = nameof(DeleteWorkTimeTracked)
            });

            var tasks = await _getTasksQuery.Run(x => x.Name == nameof(DeleteWorkTimeTracked));

            await _createScheduleCommand.Run(new ScheduleDto
            {
                ScheduleDate = DateTimeOffset.Now.Date
            });

            var task = tasks.First();
            var schedule = await _getCurrentScheduleQuery.Run();

            var tempTaskSchedule = new TasksScheduleDto()
            {
                Duration = TimeSpan.Zero,
                TaskId = task.Id,
                ScheduleId = schedule.Id,
            };

            await _trackWorkTimeCommand.Run(tempTaskSchedule);

            var result = await _getTasksScheduleQuery.Run(x => x.TaskId == task.Id
                                                            && x.ScheduleId == schedule.Id);

            var trackedTime = result.First();

            var timeToDelete = _mapper.Map<TasksScheduleDto>(trackedTime);

            await _sut.Run(timeToDelete);
        }
    }
}
