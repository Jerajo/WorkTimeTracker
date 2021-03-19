using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Application.Commands;
using TimeTracker.Application.Dtos;
using TimeTracker.Application.Factories;
using TimeTracker.Application.Queries;

namespace TimeTracker.Application.UnitTests.Commands
{
    [TestClass]
    public class DeleteGroupCommandShould
    {
        private DeleteGroupCommand _sut;
        private CreateGroupCommand _createGroupCommand;
        private GetGroupsQuery _getGroupQuery;
        private IMapper _mapper;

        [TestInitialize]
        public void TestInitialize()
        {
            var commandFactory = AssemblyConfiguration.Kernel.Get<CommandFactory>();
            var queryFactory = AssemblyConfiguration.Kernel.Get<QueryFactory>();

            _mapper = AssemblyConfiguration.Kernel.Get<IMapper>();
            _createGroupCommand = commandFactory.GetInstance<CreateGroupCommand>();
            _getGroupQuery = queryFactory.GetInstance<GetGroupsQuery>();
            _sut = commandFactory.GetInstance<DeleteGroupCommand>();
        }

        [TestMethod]
        public async Task GuardAgainstNull()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.ExecuteAsync(null));
        }

        [TestMethod]
        public void ValidateModelState()
        {
            var tempGroup = new GroupDto();

            Func<Task> function = () => _sut.ExecuteAsync(tempGroup);

            function.Should().Throw<ValidationException>()
                .And.Errors.Should()
                .Contain(x => x.PropertyName == nameof(GroupDto.Id));
        }

        [TestMethod]
        public async Task DeleteAGroup()
        {
            var tempGroup = new GroupDto() { Name = nameof(DeleteAGroup) };

            await _createGroupCommand.ExecuteAsync(tempGroup);

            var result = await _getGroupQuery.ExecuteAsync(x => x.Name == tempGroup.Name);

            var group = result.First();

            tempGroup = _mapper.Map<GroupDto>(group);

            await _sut.ExecuteAsync(tempGroup);
        }
    }
}
