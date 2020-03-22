using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using TimeTracker.Application.Query;

namespace TimeTracker.Application.UnitTests
{
    [TestClass]
    public class NijectShould
    {
        private IKernel _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _sut = new StandardKernel(new TestModule());
        }

        [TestMethod]
        public void GetGroupsQuery()
        {
            var result = _sut.Get<GetGroupsQuery>();

            Assert.IsInstanceOfType(result, typeof(GetGroupsQuery));
        }

        [TestMethod]
        public void GetTasksQuery()
        {
            var result = _sut.Get<GetTasksQuery>();

            Assert.IsInstanceOfType(result, typeof(GetTasksQuery));
        }
    }
}
