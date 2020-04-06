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
            Assert.ThrowsException<ArgumentNullException>(() => _sut.Run(null));
        }

        [TestMethod]
        public async Task ReturnAEmptyListOfTasksSchedule()
        {
            var result = await _sut.Run(x => x.Id < 0);

            result.Should().BeAssignableTo<List<Domain.TasksSchedule>>()
                .And.BeEmpty();
        }

        [TestMethod]
        public async Task ReturnAListOfTasksSchedule()
        {
            await _createTaskCommand.Run(new TaskDto
            {
                Name = nameof(ReturnAListOfTasksSchedule)
            });

            var tasks = await _getTaskQuery.Run(x => x.Name == nameof(ReturnAListOfTasksSchedule));

            await _createScheduleCommand.Run(new ScheduleDto
            {
                ScheduleDate = DateTimeOffset.Now.Date
            });

            var task = tasks.First();
            var schedule = await _getCurrentScheduleQuery.Run();

            await _trackWorkTimeCommand.Run(new TasksScheduleDto
            {
                Duration = TimeSpan.Zero,
                TaskId = task.Id,
                ScheduleId = schedule.Id
            });

            var result = await _sut.Run(x => x.ScheduleId == schedule.Id);

            result.Should().BeAssignableTo<List<Domain.TasksSchedule>>()
                .And.NotBeEmpty();
        }
    }
}
