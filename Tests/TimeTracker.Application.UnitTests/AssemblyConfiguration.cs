using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using TimeTracker.Core.Contracts;
using TimeTracker.Tests.Common.Helpers;

namespace TimeTracker.Application.UnitTests
{
    [TestClass]
    public static class AssemblyConfiguration
    {
        public static IKernel Kernel;
        private static IDbContext _dbContext;

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            Kernel = new StandardKernel(new TestModule("workTimeTracker_Application"));

            _dbContext = Kernel.Get<IDbContext>();
            _dbContext.EnsureCreated();
            _dbContext.FetchInitialData().Wait();
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
