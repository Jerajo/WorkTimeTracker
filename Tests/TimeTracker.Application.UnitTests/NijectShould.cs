using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using TimeTracker.Application.Query;

namespace TimeTracker.Application.UnitTests
{
    [TestClass]
    public class NijectShould
    {
        private IKernel _Sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _Sut = new StandardKernel(new TestModule());
        }

        [TestMethod]
        public void GetGroupsQuery()
        {
            var result = _Sut.Get<GetGroupsQuery>();

            Assert.IsInstanceOfType(result, typeof(GetGroupsQuery));
        }

        [TestMethod]
        public void GetTasksQuery()
        {
            var result = _Sut.Get<GetTasksQuery>();

            Assert.IsInstanceOfType(result, typeof(GetTasksQuery));
        }
    }
}
