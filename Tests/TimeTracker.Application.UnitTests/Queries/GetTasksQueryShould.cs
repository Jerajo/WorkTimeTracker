using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTracker.Application.Factories;
using TimeTracker.Application.Queries;

namespace TimeTracker.Application.UnitTests.Queries
{
    [TestClass]
    public class GetTasksQueryShould
    {
        private GetTasksQuery _Sut;

        [TestInitialize]
        public void TextInitialize()
        {
            var factory = AssemblyConfiguration.Kernel.Get<QueryFactory>();

            _Sut = factory.GetInstance<GetTasksQuery>();
        }

        [TestMethod]
        public void GuardAgainstNullArguments()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _Sut.Run(null));
        }

        [TestMethod]
        public async Task ReturnEmptyListOrAListOfTasks()
        {
            var emptyList = new List<Domain.Task>();
            var result = await _Sut.Run(new Domain.Task());

            Assert.IsInstanceOfType(result, typeof(List<Domain.Task>));
            CollectionAssert.AreEqual(result, emptyList);
        }
    }
}
