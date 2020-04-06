using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using TimeTracker.Application.Commands;
using TimeTracker.Application.Dtos;
using TimeTracker.Application.Factories;
using TimeTracker.Application.Validators;

namespace TimeTracker.Application.UnitTests.Validators
{
    [TestClass]
    public class TaskValidatorShould
    {
        private TaskValidator _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            var factory = AssemblyConfiguration.Kernel.Get<ValidatorFactory>();
            _sut = factory.GetInstance<TaskValidator>();
        }

        [TestMethod]
        public void ValidateNameIsNotNull()
        {
            var tempTask = new TaskDto() { Name = null };

            _sut.Validate(tempTask).Errors.Should()
                .Contain(x => x.PropertyName == nameof(TaskDto.Name));
        }

        [TestMethod]
        public void ValidateNameIsNotEmpty()
        {
            var tempTask = new TaskDto() { Name = "" };

            _sut.Validate(tempTask).Errors.Should()
                .Contain(x => x.PropertyName == nameof(TaskDto.Name));
        }

        [TestMethod]
        public void ValidateNameIsLessThan500Characters()
        {
            var tempTask = new TaskDto()
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
        public void ValidateGroupIdExits()
        {
            var tempTask = new TaskDto()
            {
                Name = "Test task",
                GroupId = -1
            };

            _sut.Validate(tempTask).Errors.Should()
                .Contain(x => x.PropertyName == nameof(TaskDto.GroupId));
        }

        [TestMethod]
        public void ValidateDescriptionExits()
        {
            var tempTask = new TaskDto()
            {
                Name = "Test task",
                DescriptionId = -1,
            };

            _sut.Validate(tempTask).Errors.Should()
                .Contain(x => x.PropertyName == nameof(TaskDto.DescriptionId));
        }
    }
}
