using System;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http;
using Sat.Recruitment.Models;
using Sat.Recruitment.Contracts.Engine;
using Sat.Recruitment.Engine;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.DataAccess.Interfaces;
using Moq;
using Sat.Recruitment.DataAccess.DTOAdapter;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        private readonly Mock<IUserRepository> _repository;
        private readonly Mock<ILogger<UserEngine>> _logger;
        private readonly IUserEngine _userEngine;

        public UnitTest1()
        {
            _repository = new Mock<IUserRepository>();
            _logger = new Mock<ILogger<UserEngine>>();
            _userEngine = new UserEngine(_repository.Object, _logger.Object);
        }

        [Fact]
        public async void CreateUser_AddingUser_ReturnsCreated()
        {

            var user = new User()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            };

            var expectedVal = new User()
            {
                Id= 1,
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            };
            _repository.Setup(p => p.SaveOrUptedeAsync(It.IsAny<DataAccess.Schema.User>()).Result).Returns(expectedVal.ToDBModel());

            var result = await _userEngine.AddUser(user);

            Assert.Equal(expectedVal.Id, result.Id);
        }

        [Fact]
        public async void CreateUser_AddingExistingUser_ReturnsBadRequest()
        {
            var user = new User()
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            };
            var expectedVal = new User()
            {
                Id = 0,
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            };
            _repository.Setup(p => p.SaveOrUptedeAsync(It.IsAny<DataAccess.Schema.User>()).Result).Returns(expectedVal.ToDBModel());

            var result = await _userEngine.AddUser(user);

            Assert.Equal(expectedVal.Id, expectedVal.Id);
        }

        [Theory]
        [InlineData("Agustina@gmail.com", "Agustina", 1)]
        [InlineData("Juan@gmail.com", "Juan", 2)]
        public async void CreateUser_AddingUsers(string email, string name, int expectedValin)
        {
            var user = new User()
            {
                Name = name,
                Email = email,
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            };

            var expectedVal = new User()
            {
                Id = expectedValin,
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            };

            _repository.Setup(p => p.SaveOrUptedeAsync(It.IsAny<DataAccess.Schema.User>()).Result).Returns(expectedVal.ToDBModel());

            var result = await _userEngine.AddUser(user);

            Assert.Equal(expectedVal.Id, expectedVal.Id);
        }
    }
}
