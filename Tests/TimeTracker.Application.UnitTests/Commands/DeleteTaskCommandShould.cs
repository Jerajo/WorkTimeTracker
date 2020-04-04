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
    public class DeleteTaskCommandShould
    {
        private DeleteTaskCommand _sut;
        private CreateTaskCommand _createTaskCommand;
        private GetTasksQuery _getTaskQuery;
        private IMapper _mapper;

        [TestInitialize]
        public void TestInitialize()
        {
            var commandFactory = AssemblyConfiguration.Kernel.Get<CommandFactory>();
            var queryFactory = AssemblyConfiguration.Kernel.Get<QueryFactory>();

            _mapper = AssemblyConfiguration.Kernel.Get<IMapper>();
            _createTaskCommand = commandFactory.GetInstance<CreateTaskCommand>();
            _getTaskQuery = queryFactory.GetInstance<GetTasksQuery>();
            _sut = commandFactory.GetInstance<DeleteTaskCommand>();
        }

        [TestMethod]
        public async Task GuardAgainstNull()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.Run(null));
        }

        [TestMethod]
        public void ValidateModelState()
        {
            var tempTask = new TaskDto()
            {
                Name = "",
                GroupId = -1,
                DescriptionId = -1
            };

            Func<Task> function = () => _sut.Run(tempTask);

            function.Should().Throw<ValidationException>()
                .And.Errors.Should()
                .Contain(x => x.PropertyName == nameof(TaskDto.Id));
        }

        [TestMethod]
        public async Task DeleteATask()
        {
            var tempTask = new TaskDto() { Name = "Test task" };

            await _createTaskCommand.Run(tempTask);

            var result = await _getTaskQuery.Run(x => x.Name != null);

            var task = result.FirstOrDefault();

            task.Name = "Test task updated";

            tempTask = _mapper.Map<TaskDto>(task);

            await _sut.Run(tempTask);
        }
    }
}
