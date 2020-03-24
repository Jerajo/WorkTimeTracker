using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Text;
using TimeTracker.Application.Factories;
using TimeTracker.Infrastructure.Entities;
using TimeTracker.Tests.Common.Helpers;

namespace TimeTracker.Tests.Common.Configuration
{
    [TestClass]
    public class AssemblyConfiguration
    {
        private static IKernel _Kernel;
        public static WorkTimeTracker DBConext;
        public static CommandFactory CFactory;
        public static QueryFactory QFactory;

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            _Kernel = new StandardKernel(new TestModule());
            CFactory = _Kernel.Get<CommandFactory>();
            QFactory = _Kernel.Get<QueryFactory>();
            DBConext = _Kernel.Get<WorkTimeTracker>();
        }
    }
}
