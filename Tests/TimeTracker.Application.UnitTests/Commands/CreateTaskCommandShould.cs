using FluentAssertions;
using FluentAssertions.Execution;
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
    public class CreateTaskCommandShould
    {
        private CreateTaskCommand _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            var factory = AssemblyConfiguration.Kernel.Get<CommandFactory>();
            _sut = factory.GetInstance<CreateTaskCommand>();
        }

        [TestMethod]
        public async Task GuardAgainstNull()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.ExecuteAsync(null));
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

            Func<Task> function = () => _sut.ExecuteAsync(tempTask);

            using (new AssertionScope())
            {
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
        public async Task CreateNewTask()
        {
            var tempTask = new TaskDto() { Name = "Test task" };

            await _sut.ExecuteAsync(tempTask);
        }
    }
}
