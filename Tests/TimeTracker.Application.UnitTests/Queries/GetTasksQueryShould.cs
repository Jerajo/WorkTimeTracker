using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TimeTracker.Application.Queries;
using TimeTracker.Tests.Common.Configuration;

namespace TimeTracker.Application.UnitTests
{
    [TestClass]
    public class GetTasksQueryShould
    {
        private GetTasksQuery _Sut;

        [TestInitialize]
        public void TextInitialize()
        {
            var factory = AssemblyConfiguration.QFactory;
            _Sut = factory.GetInstance<GetTasksQuery>();
        }

        [TestMethod]
        public void GualdAgainstNullArguments()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _Sut.Run(null));
        }

        [TestMethod]
        public void ReturnEmptyListOrAListOfTasks()
        {
            List<Domain.Task> emptyList = new List<Domain.Task>();
            var task_run = _Sut.Run(new Domain.Task()
            {
                Id = 1,
                Code = "100",
                Name = "Task test"
            });
            
            var result = task_run.Result;

            Assert.IsInstanceOfType(result, typeof(List<Domain.Task>));
            CollectionAssert.Equals(result, emptyList);
        }
    }
}
