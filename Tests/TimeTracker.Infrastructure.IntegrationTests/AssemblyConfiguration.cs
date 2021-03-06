﻿using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using TimeTracker.EF6.Services;
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
            Kernel = new StandardKernel(new TestModule(options => options
                .UseInMemoryDatabase("Infrastructure_Integration_Tests")
                .UseLazyLoadingProxies()
                .Options));

            _dbContext = Kernel.Get<WorkTimeTracker>();
            _dbContext.CanConnect();
            //if (!_dbContext.CanConnect())
            //{
            //    _dbContext.EnsureCreated();
            //    _dbContext.FetchInitialData().Wait();
            //}
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
