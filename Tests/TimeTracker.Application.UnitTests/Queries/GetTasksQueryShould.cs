using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using TimeTracker.Application.Factories;
using TimeTracker.Application.Queries;
using TimeTracker.Domain;
using AsyncOperation = System.Threading.Tasks.Task;

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
        public async AsyncOperation ReturnEmptyListOrAListOfTasks()
        {
            var emptyList = new List<Task>();
            var result = await _Sut.Run(new Task());

            Assert.IsInstanceOfType((object)result, typeof(List<Task>));
            CollectionAssert.AreEqual(result, emptyList);
        }
    }
}
