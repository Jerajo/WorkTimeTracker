using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeTracker.Infrastructure.Services;

namespace TimeTracker.Infrastructure.IntegrationTests
{
    [TestClass]
    public class WorkTimeTrackerShould
    {
        private WorkTimeTracker _Sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _Sut = new WorkTimeTracker();
            _Sut.Database.EnsureCreated();
        }

        [TestMethod]
        public void ConnectToSQLiteDB()
        {
            Assert.IsTrue(_Sut.Database.CanConnect());
        }
    }
}
