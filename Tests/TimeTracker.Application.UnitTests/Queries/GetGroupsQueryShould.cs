using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTracker.Application.Commands;
using TimeTracker.Application.Dtos;
using TimeTracker.Application.Queries;

namespace TimeTracker.Application.UnitTests.Queries
{
    [TestClass]
    public class GetGroupsQueryShould
    {
        private CreateGroupCommand _createGroupCommand;
        private GetGroupsQuery _sut;

        [TestInitialize]
        public void TextInitialize()
        {
            var kernel = AssemblyConfiguration.Kernel;
            _createGroupCommand = kernel.Get<CreateGroupCommand>();
            _sut = kernel.Get<GetGroupsQuery>();
        }

        [TestMethod]
        public void GuardAgainstNullArguments()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _sut.ExecuteAsync(null));
        }

        [TestMethod]
        public async Task ReturnAEmptyListOfGroups()
        {
            await _createGroupCommand.ExecuteAsync(new GroupDto
            {
                Name = nameof(ReturnAListOfGroups),
            });

            var result = await _sut.ExecuteAsync(x => false);

            result.Should().BeAssignableTo<List<Domain.Group>>()
                .And.BeEmpty();
        }

        [TestMethod]
        public async Task ReturnAListOfGroups()
        {
            await _createGroupCommand.ExecuteAsync(new GroupDto
            {
                Name = nameof(ReturnAListOfGroups),
            });

            var result = await _sut.ExecuteAsync(x => x.Name == nameof(ReturnAListOfGroups));

            result.Should().BeAssignableTo<List<Domain.Group>>()
                .And.NotBeEmpty();
        }
    }
}
