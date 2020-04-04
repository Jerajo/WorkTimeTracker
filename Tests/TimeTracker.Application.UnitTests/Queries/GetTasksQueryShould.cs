using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeTracker.Application.Commands;
using TimeTracker.Application.Dtos;
using TimeTracker.Application.Queries;
using TimeTracker.Domain;
using AsyncOperation = System.Threading.Tasks.Task;

namespace TimeTracker.Application.UnitTests.Queries
{
    [TestClass]
    public class GetTasksQueryShould
    {
        private IKernel _kernel;
        private GetTasksQuery _sut;

        [TestInitialize]
        public void TextInitialize()
        {
            _kernel = AssemblyConfiguration.Kernel;
            _sut = _kernel.Get<GetTasksQuery>();
        }

        [TestMethod]
        public void GuardAgainstNullArguments()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _sut.Run(null));
        }

        [TestMethod]
        public async AsyncOperation ReturnAEmptyListOfTasks()
        {
            var result = await _sut.Run(x => x.Id < 0);

            Assert.IsInstanceOfType(result, typeof(List<Task>));
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public async AsyncOperation ReturnAListOfTasks()
        {
            var createTask = _kernel.Get<CreateTaskCommand>();
            await createTask.Run(new TaskDto
            {
                Code = "4526",
                Name = "Hola",
                DescriptionId = 1
            });

            var result = await _sut.Run(x => true);

            Assert.IsInstanceOfType(result, typeof(List<Task>));
            Assert.IsTrue(result.Any());
        }
    }
}
