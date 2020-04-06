using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace TimeTracker.Domain.UnitTests
{
    [TestClass]
    public class MapperShould
    {
        [TestMethod]
        public void MapObject()
        {
            var mapper = AssemblyConfiguration.Kernel.Get<IMapper>();

            var entity = mapper.Map<Core.Task>(new Task { Name = nameof(MapObject) });

            entity.Should().BeAssignableTo<Core.Task>()
                .And.NotBeNull();
        }
    }
}
