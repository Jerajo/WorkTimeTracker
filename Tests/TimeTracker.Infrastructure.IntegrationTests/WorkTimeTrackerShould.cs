using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TimeTracker.Infrastructure.Entities;

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
        }

        [TestMethod]
        public void ConnetToSQLiteDB()
        {
            Assert.IsTrue(_Sut.Database.CanConnect());
        }
    }
}
