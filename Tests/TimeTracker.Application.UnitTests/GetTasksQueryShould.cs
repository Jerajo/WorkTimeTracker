using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TimeTracker.Application.UnitTests
{
    [TestClass]
    public class GetTasksQueryShould
    {
        private GetTasksQuery _Sut;

        [TestInitialize]
        public void TextInitialize()
        {
            _Sut = new GetTasksQuery();
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
