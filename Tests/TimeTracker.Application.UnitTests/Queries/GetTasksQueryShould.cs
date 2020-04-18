using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
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
        private CreateTaskCommand _createTaskCommand;
        private GetTasksQuery _sut;

        [TestInitialize]
        public void TextInitialize()
        {
            var kernel = AssemblyConfiguration.Kernel;
            _createTaskCommand = kernel.Get<CreateTaskCommand>();
            _sut = kernel.Get<GetTasksQuery>();
        }

        [TestMethod]
        public void GuardAgainstNullArguments()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _sut.Run(null));
        }

        [TestMethod]
        public async AsyncOperation ReturnAEmptyListOfTasks()
        {
            await _createTaskCommand.ExecuteAsync(new TaskDto
            {
                Name = nameof(ReturnAListOfTasks),
                DescriptionId = 1
            });

            var result = await _sut.Run(x => false);

            result.Should().BeAssignableTo<List<Task>>()
                .And.BeEmpty();
        }

        [TestMethod]
        public async AsyncOperation ReturnAListOfTasks()
        {
            await _createTaskCommand.ExecuteAsync(new TaskDto
            {
                Name = nameof(ReturnAListOfTasks),
                DescriptionId = 1
            });

            var result = await _sut.Run(x => true);

            result.Should().BeAssignableTo<List<Task>>()
                .And.NotBeEmpty();
        }
    }
}
