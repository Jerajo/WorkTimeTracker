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
            Assert.ThrowsException<ArgumentNullException>(() => _sut.Run(null));
        }

        [TestMethod]
        public async Task ReturnAEmptyListOfGroups()
        {
            var result = await _sut.Run(x => x.Id < 0);

            result.Should().BeAssignableTo<List<Domain.Group>>()
                .And.BeEmpty();
        }

        [TestMethod]
        public async Task ReturnAListOfGroups()
        {
            await _createGroupCommand.Run(new GroupDto
            {
                Code = "4526",
                Name = nameof(ReturnAListOfGroups),
            });

            var result = await _sut.Run(x => true);

            result.Should().BeAssignableTo<List<Domain.Group>>()
                .And.NotBeEmpty();
        }
    }
}
