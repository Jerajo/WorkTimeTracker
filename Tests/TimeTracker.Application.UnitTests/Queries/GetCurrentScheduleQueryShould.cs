using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Threading.Tasks;
using TimeTracker.Application.Queries;

namespace TimeTracker.Application.UnitTests.Queries
{
    [TestClass]
    public class GetCurrentScheduleQueryShould
    {
        private GetCurrentScheduleQuery _sut;

        [TestInitialize]
        public void TextInitialize()
        {
            var kernel = AssemblyConfiguration.Kernel;
            _sut = kernel.Get<GetCurrentScheduleQuery>();
        }

        [TestMethod]
        public async Task GetCurrentSchedule()
        {
            var result = await _sut.Run();

            result.Should().BeAssignableTo<Domain.Schedule>()
                .And.NotBeNull();
        }
    }
}
