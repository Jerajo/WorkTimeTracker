using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using TimeTracker.Application.Dtos;
using TimeTracker.Application.Factories;
using TimeTracker.Application.Validators;

namespace TimeTracker.Application.UnitTests.Validators
{
    [TestClass]
    public class GroupValidatorShould
    {
        private GroupValidator _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            var factory = AssemblyConfiguration.Kernel.Get<ValidatorFactory>();
            _sut = factory.GetInstance<GroupValidator>();
        }

        [TestMethod]
        public void ValidateNameIsNotNull()
        {
            var tempTask = new GroupDto() { Name = null };

            _sut.Validate(tempTask).Errors.Should()
                .Contain(x => x.PropertyName == nameof(TaskDto.Name));
        }

        [TestMethod]
        public void ValidateNameIsNotEmpty()
        {
            var tempTask = new GroupDto() { Name = "" };

            _sut.Validate(tempTask).Errors.Should()
                .Contain(x => x.PropertyName == nameof(TaskDto.Name));
        }

        [TestMethod]
        public void ValidateNameIsLessThan500Characters()
        {
            var tempTask = new GroupDto()
            {
                Name = "123456789012345678901234567890123456789012345678901234567890" +
                       "123456789012345678901234567890123456789012345678901234567890" +
                       "123456789012345678901234567890123456789012345678901234567890" +
                       "123456789012345678901234567890123456789012345678901234567890" +
                       "123456789012345678901234567890123456789012345678901234567890" +
                       "123456789012345678901234567890123456789012345678901234567890" +
                       "123456789012345678901234567890123456789012345678901234567890" +
                       "123456789012345678901234567890123456789012345678901234567890" +
                       "123456789012345678901234567890123456789012345678901234567890"
            };

            _sut.Validate(tempTask).Errors.Should()
                .Contain(x => x.PropertyName == nameof(TaskDto.Name));
        }

        [TestMethod]
        public void ValidateCodeHasMoreThan3Characters()
        {
            var tempTask = new GroupDto()
            {
                Name = "Test group",
                Code = "123"
            };

            _sut.Validate(tempTask).Errors.Should()
                .Contain(x => x.PropertyName == nameof(GroupDto.Code));
        }

        [TestMethod]
        public void ValidateCodeHasLessThan11Characters()
        {
            var tempTask = new GroupDto()
            {
                Name = "Test group",
                Code = "12345678901"
            };

            _sut.Validate(tempTask).Errors.Should()
                .Contain(x => x.PropertyName == nameof(GroupDto.Code));
        }
    }
}
