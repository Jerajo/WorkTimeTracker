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
    public class UpdateTaskCommandShould
    {
        private UpdateTaskCommand _sut;
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
            _sut = commandFactory.GetInstance<UpdateTaskCommand>();
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

            using (new AssertionScope())
            {
                function.Should().Throw<ValidationException>()
                    .And.Errors.Should()
                    .Contain(x => x.PropertyName == nameof(TaskDto.Id));

                function.Should().Throw<ValidationException>()
                    .And.Errors.Should()
                    .Contain(x => x.PropertyName == nameof(TaskDto.Name));

                function.Should().Throw<ValidationException>()
                    .And.Errors.Should()
                    .Contain(x => x.PropertyName == nameof(TaskDto.DescriptionId));

                function.Should().Throw<ValidationException>()
                    .And.Errors.Should()
                    .Contain(x => x.PropertyName == nameof(TaskDto.GroupId));
            }
        }

        [TestMethod]
        public async Task UpdateATask()
        {
            var tempTask = new TaskDto() { Name = nameof(UpdateATask) };

            await _createTaskCommand.Run(tempTask);

            var result = await _getTaskQuery.Run(x => x.Name == tempTask.Name);

            var task = result.First();

            task.Name += " updated";

            tempTask = _mapper.Map<TaskDto>(task);

            await _sut.Run(tempTask);
        }
    }
}
