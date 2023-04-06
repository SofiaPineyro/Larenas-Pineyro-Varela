using ArenaGestor.API.Controllers;
using ArenaGestor.APIContracts.Users;
using ArenaGestor.BusinessInterface;
using ArenaGestor.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace ArenaGestor.APITest
{
    [TestClass]
    public class UsersControllerTest
    {
        private Mock<IUsersService> mock;
        private Mock<IMapper> mockMapper;

        private UsersController api;

        private User userOK;
        private UserChangePassword userChangePasswordOK;
        private IEnumerable<User> usersOK;

        private UserGetUsersDto getUsersDto;
        private UserUpdateUserDto updateUserDto;
        private UserInsertUserDto insertUserDto;
        private UserResultUserDto resultUserDto;
        private IEnumerable<UserResultUserDto> resultUsersDto;
        private UserChangePasswordDto userChangePasswordDto;
        private string randomToken;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IUsersService>(MockBehavior.Strict);
            mockMapper = new Mock<IMapper>(MockBehavior.Strict);

            api = new UsersController(mock.Object, mockMapper.Object);

            randomToken = BusinessHelpers.StringGenerator.GenerateRandomToken(64);

            userOK = new User()
            {
                UserId = 1,
                Name = "Test",
                Surname = "User",
                Email = "test@user.com",
                Password = "testuser123",
                Roles = new List<UserRole>() {
                    new UserRole()
                    {
                        RoleId = RoleCode.Administrador
                    }
                }
            };

            usersOK = new List<User>() { userOK };

            getUsersDto = new UserGetUsersDto()
            {
                Name = "Test",
                Surname = "User",
                Email = "test@user.com"
            };

            resultUserDto = new UserResultUserDto()
            {
                UserId = 1,
                Name = "Test",
                Surname = "User",
                Email = "test@user.com"
            };

            resultUsersDto = new List<UserResultUserDto>()
            {
                resultUserDto
            };

            updateUserDto = new UserUpdateUserDto()
            {
                UserId = 1,
                Name = "Test",
                Surname = "User",
                Roles = new List<UserRoleDto>() {
                    new UserRoleDto()
                    {
                        RoleId = 1
                    }
                }
            };

            insertUserDto = new UserInsertUserDto()
            {
                Name = "Test",
                Surname = "User",
                Email = "test@user.com",
                Password = "testuser123",
                Roles = new List<UserRoleDto>() {
                    new UserRoleDto()
                    {
                        RoleId = 1
                    }
                }
            };

            userChangePasswordDto = new UserChangePasswordDto()
            {
                Email = "test@user.com",
                OldPassword = "testuser123",
                NewPassword = "newuser123"
            };

            userChangePasswordOK = new UserChangePassword("test@user.com", "testuser123", "newuser123");
        }

        [TestMethod]
        public void GetUsersOkTest()
        {
            mock.Setup(x => x.GetUsers(userOK)).Returns(usersOK);
            mockMapper.Setup(x => x.Map<User>(getUsersDto)).Returns(userOK);
            mockMapper.Setup(x => x.Map<IEnumerable<UserResultUserDto>>(usersOK)).Returns(resultUsersDto);

            var result = api.GetUsers(getUsersDto);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void GetUserByIdOkTest()
        {
            mock.Setup(x => x.GetUserById(userOK.UserId)).Returns(userOK);
            mockMapper.Setup(x => x.Map<UserResultUserDto>(userOK)).Returns(resultUserDto);

            var result = api.GetUserById(userOK.UserId);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void PostUserOkTest()
        {
            mock.Setup(x => x.InsertUser(userOK)).Returns(userOK);
            mockMapper.Setup(x => x.Map<User>(insertUserDto)).Returns(userOK);
            mockMapper.Setup(x => x.Map<UserResultUserDto>(userOK)).Returns(resultUserDto);

            var result = api.PostUser(insertUserDto);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void PutUserOkTest()
        {
            mock.Setup(x => x.UpdateUser(userOK)).Returns(userOK);
            mockMapper.Setup(x => x.Map<User>(updateUserDto)).Returns(userOK);
            mockMapper.Setup(x => x.Map<UserResultUserDto>(userOK)).Returns(resultUserDto);

            var result = api.PutUser(updateUserDto);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void DeleteUserOkTest()
        {
            mock.Setup(x => x.DeleteUser(It.IsAny<int>()));
            var result = api.DeleteUser(It.IsAny<int>());
            var objectResult = result as OkResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void ChangePasswordOkTest()
        {
            mock.Setup(x => x.ChangePassword(userChangePasswordOK));
            mockMapper.Setup(x => x.Map<UserChangePassword>(userChangePasswordDto)).Returns(userChangePasswordOK);

            var result = api.PutUserPassword(userChangePasswordDto);
            var objectResult = result as OkResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void ChangePasswordLoggedInOkTest()
        {
            mock.Setup(x => x.ChangePassword(It.IsAny<string>(), userChangePasswordOK));
            mockMapper.Setup(x => x.Map<UserChangePassword>(userChangePasswordDto)).Returns(userChangePasswordOK);
            api.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            api.ControllerContext.HttpContext.Request.Headers["token"] = randomToken;

            var result = api.PutUserLoggedInPassword(userChangePasswordDto);
            var objectResult = result as OkResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void PutUserLoggedInOkTest()
        {
            mock.Setup(x => x.UpdateUser(It.IsAny<string>(), It.IsAny<User>())).Returns(userOK);
            mockMapper.Setup(x => x.Map<User>(updateUserDto)).Returns(userOK);
            mockMapper.Setup(x => x.Map<UserResultUserDto>(userOK)).Returns(resultUserDto);
            api.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            api.ControllerContext.HttpContext.Request.Headers["token"] = randomToken;
            var result = api.PutUserLoggedIn(updateUserDto);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }
    }
}
