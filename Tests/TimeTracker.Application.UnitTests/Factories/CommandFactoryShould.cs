using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using TimeTracker.Application.Commands;
using TimeTracker.Application.Factories;
using TimeTracker.Tests.Common.Helpers;

namespace TimeTracker.Application.UnitTests.Factories
{
    [TestClass]
    public class CommandFactoryShould
    {
        private CommandFactory _Sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _Sut = new CommandFactory(new StandardKernel(new TestModule()));
        }

        [TestMethod]
        public void GetCreateTaskCommand()
        {
            var result = _Sut.GetInstance<CreateTaskCommand>();

            Assert.IsInstanceOfType(result, typeof(CreateTaskCommand));
        }

        [TestMethod]
        public void GetUpdateTaskCommand()
        {
            var result = _Sut.GetInstance<UpdateTaskCommand>();

            Assert.IsInstanceOfType(result, typeof(UpdateTaskCommand));
        }

        [TestMethod]
        public void GetDeleteTaskCommand()
        {
            var result = _Sut.GetInstance<DeleteTaskCommand>();

            Assert.IsInstanceOfType(result, typeof(DeleteTaskCommand));
        }

        [TestMethod]
        public void GetCreateGroupCommand()
        {
            var result = _Sut.GetInstance<CreateGroupCommand>();

            Assert.IsInstanceOfType(result, typeof(CreateGroupCommand));
        }

        [TestMethod]
        public void GetUpdateGroupCommand()
        {
            var result = _Sut.GetInstance<UpdateGroupCommand>();

            Assert.IsInstanceOfType(result, typeof(UpdateGroupCommand));
        }

        [TestMethod]
        public void GetDeleteGroupCommand()
        {
            var result = _Sut.GetInstance<DeleteGroupCommand>();

            Assert.IsInstanceOfType(result, typeof(DeleteGroupCommand));
        }
    }
}
