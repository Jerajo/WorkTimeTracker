using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Threading.Tasks;
using TimeTracker.Core.Contracts;
using TimeTracker.Core.ValueObjects;
using TimeTracker.Infrastructure.Services;

namespace TimeTracker.Infrastructure.IntegrationTests.Services
{
    [TestClass]
    public class UnitOfWorkShould
    {
        private UnitOfWork _sut;
        private IDataRepository<Description> _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            var kernel = AssemblyConfiguration.Kernel;
            var dbContext = kernel.Get<WorkTimeTracker>();

            _sut = new UnitOfWork(dbContext);
            _repository = _sut.GetRepository<Description>();
        }

        [TestMethod]
        public void CommitChanges()
        {
            _repository.Add(new Description
            {
                Template = nameof(CommitChanges)
            });

            _sut.Commit();

            var entity = _repository.Get(x => x.Template == nameof(CommitChanges));

            entity.Should().BeAssignableTo<Description>()
                .And.NotBeNull();
        }

        [TestMethod]
        public async Task CommitChangesAsync()
        {
            await _repository.AddAsync(new Description
            {
                Template = nameof(CommitChangesAsync)
            });

            await _sut.CommitAsync();

            var entity = await _repository.GetAllAsync(x => x.Template == nameof(CommitChangesAsync));

            entity.Should().BeAssignableTo<Description>()
                .And.NotBeNull();
        }

        [TestMethod]
        public void GetRepository()
        {
            var repository = _sut.GetRepository<Description>();

            repository.Should().BeAssignableTo<DataRepository<Description>>()
                .And.NotBeNull();
        }
    }
}
