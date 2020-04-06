using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Core.Contracts;
using TimeTracker.Core.ValueObjects;
using TimeTracker.Infrastructure.Services;

namespace TimeTracker.Infrastructure.IntegrationTests.Services
{
    [TestClass]
    public class DataRepositoryShould
    {
        private DataRepository<Description> _sut;
        private IUnitOfWork _unitOfWork;

        [TestInitialize]
        public void TestInitialize()
        {
            var kernel = AssemblyConfiguration.Kernel;
            var dbContext = kernel.Get<WorkTimeTracker>();

            _sut = new DataRepository<Description>(dbContext);
            _unitOfWork = _sut.GetTransaction();
        }

        #region GET

        [TestMethod]
        public void GetEntity()
        {
            _sut.Add(new Description
            {
                Template = nameof(GetEntity)
            });

            _unitOfWork.Commit();

            var entity = _sut.Get(x => x.Template == nameof(GetEntity));

            entity.Should().BeAssignableTo<Description>()
                .And.NotBeNull();
        }

        [TestMethod]
        public async Task GetEntityAsync()
        {
            await _sut.AddAsync(new Description
            {
                Template = nameof(GetEntityAsync)
            });

            await _unitOfWork.CommitAsync();

            var entity = await _sut.GetAllAsync(x => x.Template == nameof(GetEntityAsync));

            entity.Should().BeAssignableTo<Description>()
                .And.NotBeNull();
        }

        [TestMethod]
        public void QueryEntity()
        {
            _sut.AddGroup(new List<Description>
            {
                new Description { Template = nameof(QueryEntity) },
                new Description { Template = nameof(QueryEntity) }
            });

            _unitOfWork.Commit();

            var entities = _sut.Query(x => x.Template == nameof(QueryEntity));

            entities.Should().BeAssignableTo<List<Description>>()
                .And.HaveCount(2);
        }

        [TestMethod]
        public async Task QueryEntityAsync()
        {
            await _sut.AddGroupAsync(new List<Description>
            {
                new Description { Template = nameof(QueryEntityAsync) },
                new Description { Template = nameof(QueryEntityAsync) }
            });

            await _unitOfWork.CommitAsync();

            var entities = await _sut.QueryAsync(x => x.Template == nameof(QueryEntityAsync));

            entities.Should().BeAssignableTo<List<Description>>()
                .And.HaveCount(2);
        }

        [TestMethod]
        public void GetQueryAbleEntity()
        {
            _sut.AddGroup(new List<Description>
            {
                new Description { Template = nameof(GetQueryAbleEntity) },
                new Description { Template = nameof(GetQueryAbleEntity) }
            });

            _unitOfWork.Commit();

            var entities = _sut.GetAll()
                .Select(x => new
                {
                    TemplateId = x.Id,
                    Description = x.Template
                })
                .Where(x => x.Description == nameof(GetQueryAbleEntity))
                .OrderBy(x => x.Description);

            entities.ToList().Should().HaveCount(2);
        }

        [TestMethod]
        public async Task GetQueryAbleEntityAsync()
        {
            await _sut.AddGroupAsync(new List<Description>
            {
                new Description { Template = nameof(GetQueryAbleEntityAsync) },
                new Description { Template = nameof(GetQueryAbleEntityAsync) }
            });

            await _unitOfWork.CommitAsync();

            var entities = (await _sut.GetAllAsync())
                .Select(x => new
                {
                    TemplateId = x.Id,
                    Description = x.Template
                })
                .Where(x => x.Description == nameof(GetQueryAbleEntityAsync))
                .OrderBy(x => x.Description);

            entities.ToList().Should().HaveCount(2);
        }

        #endregion

        #region POST

        [TestMethod]
        public void AddEntity()
        {
            _sut.Add(new Description
            {
                Template = nameof(AddEntity)
            });

            _unitOfWork.Commit();

            var entity = _sut.Get(x => x.Template == nameof(AddEntity));

            entity.Should().BeAssignableTo<Description>()
                  .And.NotBeNull();
        }

        [TestMethod]
        public async Task AddEntityAsync()
        {
            await _sut.AddAsync(new Description
            {
                Template = nameof(AddEntityAsync)
            });

            await _unitOfWork.CommitAsync();

            var entity = await _sut.GetAllAsync(x => x.Template == nameof(AddEntityAsync));

            entity.Should().BeAssignableTo<Description>()
                  .And.NotBeNull();
        }

        [TestMethod]
        public void AddGroupEntity()
        {
            _sut.AddGroup(new List<Description>
            {
                new Description { Template = nameof(AddGroupEntity) },
                new Description { Template = nameof(AddGroupEntity) }
            });

            _unitOfWork.Commit();

            var entities = _sut.Query(x => x.Template == nameof(AddGroupEntity));

            entities.Should().BeAssignableTo<List<Description>>()
                    .And.HaveCount(2);
        }

        [TestMethod]
        public async Task AddGroupEntityAsync()
        {
            await _sut.AddGroupAsync(new List<Description>
            {
                new Description { Template = nameof(AddGroupEntityAsync) },
                new Description { Template = nameof(AddGroupEntityAsync) }
            });

            await _unitOfWork.CommitAsync();

            var entities = await _sut.QueryAsync(x => x.Template == nameof(AddGroupEntityAsync));

            entities.Should().BeAssignableTo<List<Description>>()
                    .And.HaveCount(2);
        }

        #endregion

        #region PUT/PATCH

        [TestMethod]
        public void UpdateEntity()
        {
            _sut.Add(new Description
            {
                Template = nameof(UpdateEntity)
            });

            _unitOfWork.Commit();

            var entity = _sut.Get(x => x.Template == nameof(UpdateEntity));

            entity.Template += " Update";

            _sut.Update(entity);

            _unitOfWork.Commit();

            var updatedEntity = _sut.Get(x => x.Id == entity.Id);

            entity.Should().BeSameAs(updatedEntity);
        }

        [TestMethod]
        public async Task UpdateEntityAsync()
        {
            await _sut.AddAsync(new Description
            {
                Template = nameof(UpdateEntityAsync)
            });

            await _unitOfWork.CommitAsync();

            var entity = await _sut.GetAllAsync(x => x.Template == nameof(UpdateEntityAsync));

            entity.Template += " Update";

            await _sut.UpdateAsync(entity);

            await _unitOfWork.CommitAsync();

            var updatedEntity = await _sut.GetAllAsync(x => x.Id == entity.Id);

            entity.Should().BeSameAs(updatedEntity);
        }

        [TestMethod]
        public void UpdateGroupEntity()
        {
            _sut.AddGroup(new List<Description>
            {
                new Description { Template = nameof(UpdateGroupEntity) },
                new Description { Template = nameof(UpdateGroupEntity) }
            });

            _unitOfWork.Commit();

            var entity = _sut.Get(x => x.Template == nameof(UpdateGroupEntity));

            entity.Template += " Update";

            _sut.UpdateGroup(entity, x => x.Template == nameof(UpdateGroupEntity));

            _unitOfWork.Commit();

            var updatedEntities = _sut.Query(x => x.Template == entity.Template);

            updatedEntities.Should().BeAssignableTo<List<Description>>()
                           .And.HaveCount(2);
        }

        [TestMethod]
        public async Task UpdateGroupEntityAsync()
        {
            await _sut.AddGroupAsync(new List<Description>
            {
                new Description { Template = nameof(UpdateGroupEntityAsync) },
                new Description { Template = nameof(UpdateGroupEntityAsync) }
            });

            await _unitOfWork.CommitAsync();

            var entity = await _sut.GetAllAsync(x => x.Template == nameof(UpdateGroupEntityAsync));

            entity.Template += " Update";

            await _sut.UpdateGroupAsync(entity, x => x.Template == nameof(UpdateGroupEntityAsync));

            await _unitOfWork.CommitAsync();

            var updatedEntities = await _sut.QueryAsync(x => x.Template == entity.Template);

            updatedEntities.Should().BeAssignableTo<List<Description>>()
                .And.HaveCount(2);
        }

        #endregion

        #region DELETE

        [TestMethod]
        public void DeleteEntity()
        {
            _sut.Add(new Description
            {
                Template = nameof(DeleteEntity)
            });

            _unitOfWork.Commit();

            var entity = _sut.Get(x => x.Template == nameof(DeleteEntity));

            _sut.Delete(entity);

            _unitOfWork.Commit();

            var deletedEntity = _sut.Get(x => x.Template == nameof(DeleteEntity));

            deletedEntity.Should().BeNull();
        }

        [TestMethod]
        public async Task DeleteEntityAsync()
        {
            await _sut.AddAsync(new Description
            {
                Template = nameof(DeleteEntityAsync)
            });

            await _unitOfWork.CommitAsync();

            var entity = await _sut.GetAllAsync(x => x.Template == nameof(DeleteEntityAsync));

            await _sut.DeleteAsync(entity);

            await _unitOfWork.CommitAsync();

            var deletedEntity = await _sut.GetAllAsync(x => x.Template == nameof(DeleteEntityAsync));

            deletedEntity.Should().BeNull();
        }

        [TestMethod]
        public void DeleteGroupEntity()
        {
            _sut.AddGroup(new List<Description>
            {
                new Description { Template = nameof(DeleteGroupEntity) },
                new Description { Template = nameof(DeleteGroupEntity) }
            });

            _unitOfWork.Commit();

            var entity = _sut.Get(x => x.Template == nameof(DeleteGroupEntity));

            _sut.DeleteGroup(x => x.Template == entity.Template);

            _unitOfWork.Commit();

            var emptyList = _sut.Query(x => x.Template == nameof(DeleteGroupEntity));

            emptyList.Should().BeAssignableTo<List<Description>>()
                .And.BeEmpty();
        }

        [TestMethod]
        public async Task DeleteGroupEntityAsync()
        {
            await _sut.AddGroupAsync(new List<Description>
            {
                new Description { Template = nameof(QueryEntityAsync) },
                new Description { Template = nameof(QueryEntityAsync) }
            });

            await _unitOfWork.CommitAsync();

            var entity = await _sut.GetAllAsync(x => x.Template == nameof(QueryEntityAsync));

            await _sut.DeleteGroupAsync(x => x.Template == entity.Template);

            await _unitOfWork.CommitAsync();

            var emptyList = await _sut.QueryAsync(x => x.Template == nameof(DeleteGroupEntity));

            emptyList.Should().BeAssignableTo<List<Description>>()
                .And.BeEmpty();
        }

        #endregion

        #region OPTIONS

        [TestMethod]
        public void HasEntity()
        {
            _sut.Add(new Description { Template = nameof(HasEntity) });

            _unitOfWork.Commit();

            var hasEntities = _sut.Any();

            hasEntities.Should().BeTrue();
        }

        [TestMethod]
        public async Task HasEntityAsync()
        {
            await _sut.AddAsync(new Description { Template = nameof(GetEntityAsync) });

            await _unitOfWork.CommitAsync();

            var hasEntities = await _sut.AnyAsync();

            hasEntities.Should().BeTrue();
        }

        #endregion
    }
}
