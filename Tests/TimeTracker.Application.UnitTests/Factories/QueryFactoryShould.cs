using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeTracker.Application.Factories;
using TimeTracker.Application.Queries;

namespace TimeTracker.Application.UnitTests.Factories
{
    [TestClass]
    public class QueryFactoryShould
    {
        [TestMethod]
        public void CreateGetTasksQuery()
        {
            var sut = new QueryFactory<GetTasksQuery>();

            var result = sut.GetInstance();

            Assert.IsInstanceOfType(result, typeof(GetTasksQuery));
        }

        [TestMethod]
        public void CreateGetGroupsQuery()
        {
            var sut = new QueryFactory<GetGroupsQuery>();

            var result = sut.GetInstance();

            Assert.IsInstanceOfType(result, typeof(GetGroupsQuery));
        }
    }
}
