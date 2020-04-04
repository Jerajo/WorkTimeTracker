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
    public class CreateGroupCommandShould
    {
        private CreateGroupCommand _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            var factory = AssemblyConfiguration.Kernel.Get<CommandFactory>();
            _sut = factory.GetInstance<CreateGroupCommand>();
        }

        [TestMethod]
        public async Task GuardAgainstNull()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.Run(null));
        }

        [TestMethod]
        public void ValidateModelState()
        {
            var tempTask = new GroupDto()
            {
                Code = "123",
                Name = ""
            };

            Func<Task> function = () => _sut.Run(tempTask);

            using (new AssertionScope())
            {
                function.Should().Throw<ValidationException>()
                    .And.Errors.Should()
                    .Contain(x => x.PropertyName == nameof(GroupDto.Name));

                function.Should().Throw<ValidationException>()
                    .And.Errors.Should()
                    .Contain(x => x.PropertyName == nameof(GroupDto.Code));
            }
        }

        [TestMethod]
        public async Task CreateNewGroup()
        {
            var tempTask = new GroupDto() { Name = "Test group" };

            await _sut.Run(tempTask);
        }
    }
}
