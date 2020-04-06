using AutoMapper;
using FluentAssertions;
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
    public class DeleteScheduleCommandShould
    {
        private DeleteScheduleCommand _sut;
        private CreateScheduleCommand _createScheduleCommand;
        private GetSchedulesQuery _getScheduleQuery;
        private IMapper _mapper;

        [TestInitialize]
        public void TestInitialize()
        {
            var commandFactory = AssemblyConfiguration.Kernel.Get<CommandFactory>();
            var queryFactory = AssemblyConfiguration.Kernel.Get<QueryFactory>();

            _mapper = AssemblyConfiguration.Kernel.Get<IMapper>();
            _createScheduleCommand = commandFactory.GetInstance<CreateScheduleCommand>();
            _getScheduleQuery = queryFactory.GetInstance<GetSchedulesQuery>();
            _sut = commandFactory.GetInstance<DeleteScheduleCommand>();
        }

        [TestMethod]
        public async Task GuardAgainstNull()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.Run(null));
        }

        [TestMethod]
        public void ValidateModelState()
        {
            var templateTask = new ScheduleDto();

            Func<Task> function = () => _sut.Run(templateTask);

            function.Should().Throw<ValidationException>()
                .And.Errors.Should()
                .Contain(x => x.PropertyName == nameof(ScheduleDto.Id));
        }

        [TestMethod]
        public async Task DeleteATemplate()
        {
            var tempSchedule = new ScheduleDto()
            {
                ScheduleDate = DateTimeOffset.Now.Date
            };

            await _createScheduleCommand.Run(tempSchedule);

            var result = await _getScheduleQuery.Run(x =>
                x.ScheduleDate == tempSchedule.ScheduleDate);

            var schedule = result.First();

            tempSchedule = _mapper.Map<ScheduleDto>(schedule);

            await _sut.Run(tempSchedule);
        }
    }
}
