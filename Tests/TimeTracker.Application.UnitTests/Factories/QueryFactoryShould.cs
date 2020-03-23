using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using TimeTracker.Application.Factories;
using TimeTracker.Application.Queries;
using TimeTracker.Tests.Common.Helpers;

namespace TimeTracker.Application.UnitTests.Factories
{
    [TestClass]
    public class QueryFactoryShould
    {
        private QueryFactory _Sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _Sut = new QueryFactory(new StandardKernel(new TestModule()));
        }

        [TestMethod]
        public void CreateGetTasksQuery()
        {
            var result = _Sut.GetInstance<GetTasksQuery>();

            Assert.IsInstanceOfType(result, typeof(GetTasksQuery));
        }

        [TestMethod]
        public void CreateGetGroupsQuery()
        {
            var result = _Sut.GetInstance<GetGroupsQuery>();

            Assert.IsInstanceOfType(result, typeof(GetGroupsQuery));
        }
    }
}
