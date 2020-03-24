﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TimeTracker.Application.Commands;
using TimeTracker.Tests.Common.Configuration;

namespace TimeTracker.Application.UnitTests.Commands
{
    class CreateTaskCommandShould
    {
        private CreateTaskCommand _Sut;

        [TestInitialize]
        public void TestInitialize()
        {
            var factory = AssemblyConfiguration.CFactory;
            _Sut = factory.GetInstance<CreateTaskCommand>();
        }

        [TestMethod]
        public void ValidateModelState()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _Sut.Run(new Domain.Task()));
        }

        [TestMethod]
        public void ValidateUserStoryExits()
        {
            var tempTask = new Domain.Task()
            {
                Code = "100",
                Name = "Test task",
                DescriptionId = 1,
                GroupId = -1
            };
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _Sut.Run(tempTask));
        }

        [TestMethod]
        public void CreateNewTask()
        {
            var tempTask = new Domain.Task()
            {
                Code = "100",
                Name = "Test task",
                DescriptionId = 1,
                GroupId = 1 // optional data.
            };
            Assert.ThrowsException<ArgumentNullException>(() => _Sut.Run(tempTask));
        }
    }
}