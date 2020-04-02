using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using TimeTracker.Infrastructure.Services;
using TimeTracker.Tests.Common.Helpers;

namespace TimeTracker.Application.UnitTests
{
    [TestClass]
    public static class AssemblyConfiguration
    {
        public static IKernel Kernel;
        private static WorkTimeTracker _WorkTimeTracker;

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            Kernel = new StandardKernel(new TestModule());

            _WorkTimeTracker = Kernel.Get<WorkTimeTracker>();
            _WorkTimeTracker.Database.EnsureCreated();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            _WorkTimeTracker.Database.EnsureDeleted();
            _WorkTimeTracker.Dispose();
            Kernel.Dispose();
        }
    }
}
