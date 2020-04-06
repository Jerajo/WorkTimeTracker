using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using TimeTracker.Infrastructure.Services;
using TimeTracker.Tests.Common.Helpers;

namespace TimeTracker.Infrastructure.IntegrationTests
{
    [TestClass]
    public static class AssemblyConfiguration
    {
        public static IKernel Kernel;
        private static WorkTimeTracker _dbContext;

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            Kernel = new StandardKernel(new TestModule());
            _dbContext = Kernel.Get<WorkTimeTracker>();

            if (!_dbContext.Database.CanConnect())
            {
                _dbContext.EnsureCreated();
                _dbContext.FetchInitialData().Wait();
            }
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            _dbContext.EnsureDeleted();
            _dbContext.Dispose();
            Kernel.Dispose();
        }
    }
}
