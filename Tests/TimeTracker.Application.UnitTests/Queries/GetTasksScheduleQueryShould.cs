using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Application.Commands;
using TimeTracker.Application.Dtos;
using TimeTracker.Application.Factories;
using TimeTracker.Application.Queries;

namespace TimeTracker.Application.UnitTests.Queries
{
    [TestClass]
    public class GetTasksScheduleQueryShould
    {
        private TrackWorkTimeCommand _trackWorkTimeCommand;
        private GetTasksScheduleQuery _sut;
        private GetCurrentScheduleQuery _getCurrentScheduleQuery;
        private GetTasksQuery _getTaskQuery;
        private CreateTaskCommand _createTaskCommand;
        private CreateScheduleCommand _createScheduleCommand;

        [TestInitialize]
        public void TextInitialize()
        {
            var commandFactory = AssemblyConfiguration.Kernel.Get<CommandFactory>();
            var queryFactory = AssemblyConfiguration.Kernel.Get<QueryFactory>();

            _getCurrentScheduleQuery = queryFactory.GetInstance<GetCurrentScheduleQuery>();
            _getTaskQuery = queryFactory.GetInstance<GetTasksQuery>();
            _createTaskCommand = commandFactory.GetInstance<CreateTaskCommand>();
            _createScheduleCommand = commandFactory.GetInstance<CreateScheduleCommand>();
            _trackWorkTimeCommand = commandFactory.GetInstance<TrackWorkTimeCommand>();
            _sut = queryFactory.GetInstance<GetTasksScheduleQuery>();
        }

        [TestMethod]
        public void GuardAgainstNullArguments()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _sut.ExecuteAsync(null));
        }

        [TestMethod]
        public async Task ReturnAEmptyListOfTasksSchedule()
        {
            await CreateTrackAsync();

            var result = await _sut.ExecuteAsync(x => false);

            result.Should().BeAssignableTo<List<Domain.TasksSchedule>>()
                .And.BeEmpty();
        }

        [TestMethod]
        public async Task ReturnAListOfTasksSchedule()
        {
            var scheduleId = await CreateTrackAsync();

            var result = await _sut.ExecuteAsync(x => x.ScheduleId == scheduleId);

            result.Should().BeAssignableTo<List<Domain.TasksSchedule>>()
                .And.NotBeEmpty();
        }

        private async Task<int> CreateTrackAsync()
        {
            await _createTaskCommand.ExecuteAsync(new TaskDto
            {
                Name = nameof(ReturnAListOfTasksSchedule)
            });

            var tasks = await _getTaskQuery.ExecuteAsync(x => x.Name == nameof(ReturnAListOfTasksSchedule));

            await _createScheduleCommand.ExecuteAsync(new ScheduleDto
            {
                ScheduleDate = DateTimeOffset.Now.Date
            });

            var task = tasks.First();
            var schedule = await _getCurrentScheduleQuery.ExecuteAsync();

            await _trackWorkTimeCommand.ExecuteAsync(new TasksScheduleDto
            {
                Duration = TimeSpan.Zero,
                TaskId = task.Id,
                ScheduleId = schedule.Id
            });

            return schedule.Id;
        }
    }
}
