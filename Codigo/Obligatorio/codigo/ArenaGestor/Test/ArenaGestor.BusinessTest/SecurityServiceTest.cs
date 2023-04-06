using ArenaGestor.Business;
using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace ArenaGestor.BusinessTest
{
    [TestClass]
    public class SecurityServiceTest
    {
        private Mock<ISessionManagement> sessionManagement;
        private Mock<IUsersManagement> usersManagement;
        private SecurityService securityServiceMock;

        private string randomToken;
        private Session sessionOk;
        private User userOK;

        [TestInitialize]
        public void InitTest()
        {
            sessionManagement = new Mock<ISessionManagement>(MockBehavior.Strict);
            usersManagement = new Mock<IUsersManagement>(MockBehavior.Strict);
            securityServiceMock = new SecurityService(sessionManagement.Object, usersManagement.Object);

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
            sessionOk = new Session()
            {
                SessionId = 1,
                Token = randomToken,
                Created = DateTime.Now,
                User = userOK
            };
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void GetUserOfTokenNotExists()
        {
            sessionManagement.Setup(x => x.GetSessions(It.IsAny<Func<Session, bool>>())).Returns(new List<Session>());
            securityServiceMock.GetUserOfToken("xxxx");
        }

        [TestMethod]
        public void GetUserOfTokenOk()
        {
            sessionManagement.Setup(x => x.GetSessions(It.IsAny<Func<Session, bool>>())).Returns(new List<Session>() { sessionOk });
            securityServiceMock.GetUserOfToken(randomToken);
            sessionManagement.VerifyAll();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void LoginUserNotExist()
        {
            usersManagement.Setup(x => x.GetUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User>());
            sessionManagement.Setup(x => x.InsertSession(It.IsAny<Session>()));
            sessionManagement.Setup(x => x.Save());

            securityServiceMock.Login("test@test.com", "xxxx");

            sessionManagement.VerifyAll();
            usersManagement.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void LoginIncorrectMailEmpty()
        {
            securityServiceMock.Login("", "xxxx");

            sessionManagement.VerifyAll();
            usersManagement.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void LoginMailIncorrect()
        {
            securityServiceMock.Login("xxxx", "xxxx");

            sessionManagement.VerifyAll();
            usersManagement.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void LoginPasswordEmpty()
        {
            securityServiceMock.Login("test@test.com", "");

            sessionManagement.VerifyAll();
            usersManagement.VerifyAll();
        }

        [TestMethod]
        public void LoginOk()
        {
            usersManagement.Setup(x => x.GetUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User>() { userOK });
            sessionManagement.Setup(x => x.InsertSession(It.IsAny<Session>()));
            sessionManagement.Setup(x => x.Save());

            securityServiceMock.Login("test@test.com", "xxxx");

            sessionManagement.VerifyAll();
            usersManagement.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void LogoutTokenNotExists()
        {
            sessionManagement.Setup(x => x.GetSessions(It.IsAny<Func<Session, bool>>())).Returns(new List<Session>());
            securityServiceMock.Logout("xxxx");
        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void LogoutInvalidTokenNotExists()
        {
            sessionManagement.Setup(x => x.GetSessions(It.IsAny<Func<Session, bool>>())).Returns(new List<Session>());
            securityServiceMock.Logout("");
        }

        [TestMethod]
        public void LogoutOk()
        {
            sessionManagement.Setup(x => x.GetSessions(It.IsAny<Func<Session, bool>>())).Returns(new List<Session>() { sessionOk });
            sessionManagement.Setup(x => x.DeleteSession(It.IsAny<Session>()));
            sessionManagement.Setup(x => x.Save());
            securityServiceMock.Logout("xxxx");
            sessionManagement.VerifyAll();
            usersManagement.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UserHaveRoleIncorrectToken()
        {
            sessionManagement.Setup(x => x.GetSessions(It.IsAny<Func<Session, bool>>())).Returns(new List<Session>());
            securityServiceMock.UserHaveRole(RoleCode.Administrador, "xxxx");
        }

        [TestMethod]
        public void UserHaveRoleThatExists()
        {
            sessionManagement.Setup(x => x.GetSessions(It.IsAny<Func<Session, bool>>())).Returns(new List<Session>() { sessionOk });
            bool result = securityServiceMock.UserHaveRole(RoleCode.Administrador, randomToken);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UserHaveRoleThatNotExists()
        {
            sessionManagement.Setup(x => x.GetSessions(It.IsAny<Func<Session, bool>>())).Returns(new List<Session>() { sessionOk });
            bool result = securityServiceMock.UserHaveRole(RoleCode.Vendedor, randomToken);
            Assert.IsFalse(result);
        }

    }
}
