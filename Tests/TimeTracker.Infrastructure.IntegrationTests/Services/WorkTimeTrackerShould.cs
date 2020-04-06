using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using TimeTracker.Infrastructure.Services;

namespace TimeTracker.Infrastructure.IntegrationTests.Services
{
    [TestClass]
    public class WorkTimeTrackerShould
    {
        private WorkTimeTracker _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            var kernel = AssemblyConfiguration.Kernel;

            _sut = kernel.Get<WorkTimeTracker>();
            _sut.Database.EnsureCreated();
        }

        [TestMethod]
        public void ConnectToSqLiteDb()
        {
            _sut.Database.CanConnect().Should().BeTrue();
        }

        [TestMethod]
        public void HasEntities()
        {
            _sut.Tasks.Should().NotBeNull();
            _sut.Groups.Should().NotBeNull();
            _sut.Schedules.Should().NotBeNull();
            _sut.TasksSchedules.Should().NotBeNull();
            _sut.Descriptions.Should().NotBeNull();
        }
    }
}
