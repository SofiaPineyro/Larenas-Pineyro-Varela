using ArenaGestor.Business;
using ArenaGestor.BusinessInterface;
using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace ArenaGestor.BusinessTest
{
    [TestClass]
    public class UsersServiceTest
    {
        private Mock<IUsersManagement> managementMock;
        private UsersService managementService;

        private Mock<ISecurityService> securityServiceMock;

        private User userOK;
        private User userNull;
        private User userOnlyName;
        private User userOnlySurname;
        private User userOnlyEmail;
        private User userOnlyEmptyName;
        private User userOnlyNullName;
        private User userOnlyEmptySurname;
        private User userOnlyNullSurname;
        private User userOnlyEmptyEmail;
        private User userOnlyNullEmail;
        private User userEmptyName;
        private User userNullName;
        private User userEmptySurname;
        private User userNullSurname;
        private User userEmptyEmail;
        private User userNullEmail;
        private User userInvalidEmail;
        private User userNewEmail;
        private User userEmptyPassword;
        private User userNullPassword;
        private User userEmptyRol;

        private IEnumerable<User> usersOK;

        private int userIdZero;
        private int userIdInexistant;

        [TestInitialize]
        public void InitTest()
        {
            managementMock = new Mock<IUsersManagement>(MockBehavior.Strict);
            securityServiceMock = new Mock<ISecurityService>(MockBehavior.Strict);

            managementService = new UsersService(managementMock.Object, securityServiceMock.Object);

            UserRole role = new UserRole()
            {
                RoleId = RoleCode.Administrador
            };

            List<UserRole> roles = new List<UserRole>() {
                role
            };

            userOK = new User()
            {
                UserId = 1,
                Name = "Test",
                Surname = "User",
                Email = "test@user.com",
                Password = "testuser123",
                Roles = roles
            };

            userOnlyName = new User()
            {
                Name = "Test"
            };

            userOnlySurname = new User()
            {
                Surname = "User"
            };

            userOnlyEmail = new User()
            {
                Email = "test@user.com"
            };

            userOnlyEmptyName = new User()
            {
                Name = ""
            };

            userOnlyEmptySurname = new User()
            {
                Surname = ""
            };

            userOnlyEmptyEmail = new User()
            {
                Email = ""
            };

            userOnlyNullName = new User()
            {
                Name = null
            };

            userOnlyNullSurname = new User()
            {
                Surname = null
            };

            userOnlyNullEmail = new User()
            {
                Email = null
            };

            userEmptyName = new User()
            {
                UserId = 1,
                Name = "",
                Surname = "User",
                Email = "test@user.com",
                Password = "testuser123",
                Roles = roles
            };

            userNullName = new User()
            {
                UserId = 1,
                Name = null,
                Surname = "User",
                Email = "test@user.com",
                Password = "testuser123",
                Roles = roles
            };

            userEmptySurname = new User()
            {
                UserId = 1,
                Name = "Test",
                Surname = "",
                Email = "test@user.com",
                Password = "testuser123",
                Roles = roles
            };

            userNullSurname = new User()
            {
                UserId = 1,
                Name = "Test",
                Surname = null,
                Email = "test@user.com",
                Password = "testuser123",
                Roles = roles
            };

            userEmptyEmail = new User()
            {
                UserId = 1,
                Name = "Test",
                Surname = "User",
                Email = "",
                Password = "testuser123",
                Roles = roles
            };

            userNullEmail = new User()
            {
                UserId = 1,
                Name = "Test",
                Surname = "User",
                Email = null,
                Password = "testuser123",
                Roles = roles
            };

            userInvalidEmail = new User()
            {
                UserId = 1,
                Name = "Test",
                Surname = "User",
                Email = "testuser.com",
                Password = "testuser123",
                Roles = roles
            };

            userEmptyPassword = new User()
            {
                UserId = 1,
                Name = "Test",
                Surname = "User",
                Email = "test@user.com",
                Password = "",
                Roles = roles
            };

            userNullPassword = new User()
            {
                UserId = 1,
                Name = "Test",
                Surname = "User",
                Email = "test@user.com",
                Password = null,
                Roles = roles
            };

            userNewEmail = new User()
            {
                UserId = 1,
                Name = "Test",
                Surname = "User",
                Email = "new@user.com",
                Password = "testuser123",
                Roles = roles
            };

            userEmptyRol = new User()
            {
                UserId = 1,
                Name = "Test",
                Surname = "User",
                Email = "test@user.com",
                Password = "testuser123",
                Roles = new List<UserRole>()
            };

            userNull = null;
            usersOK = new List<User>() { userOK };
            userIdZero = 0;
            userIdInexistant = 2;
        }

        [TestMethod]
        public void GetUserByIdTest()
        {
            managementMock.Setup(x => x.GetUserById(userOK.UserId)).Returns(userOK);
            managementService.GetUserById(userOK.UserId);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void GetUserByIdZeroTest()
        {
            managementService.GetUserById(userIdZero);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void GetUserByIdInexistantTest()
        {
            managementMock.Setup(x => x.GetUserById(userIdInexistant)).Returns(userNull);
            managementService.GetUserById(userIdInexistant);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetAllUsersTest()
        {
            managementMock.Setup(x => x.GetUsers()).Returns(usersOK);
            managementService.GetUsers();
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetFilterUserNameTest()
        {
            managementMock.Setup(x => x.GetUsers(It.IsAny<Func<User, bool>>())).Returns(usersOK);
            managementService.GetUsers(userOnlyName);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetFilterUserSurnameTest()
        {
            managementMock.Setup(x => x.GetUsers()).Returns(usersOK);
            managementMock.Setup(x => x.GetUsers(It.IsAny<Func<User, bool>>())).Returns(usersOK);
            managementService.GetUsers(userOnlySurname);
            managementMock.Verify();
        }

        [TestMethod]
        public void GetFilterUserEmailTest()
        {
            managementMock.Setup(x => x.GetUsers()).Returns(usersOK);
            managementMock.Setup(x => x.GetUsers(It.IsAny<Func<User, bool>>())).Returns(usersOK);
            managementService.GetUsers(userOnlyEmail);
            managementMock.Verify();
        }

        [TestMethod]
        public void GetFilterUserEmptyNameTest()
        {
            managementMock.Setup(x => x.GetUsers()).Returns(usersOK);
            managementService.GetUsers(userOnlyEmptyName);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetFilterUserNullNameTest()
        {
            managementMock.Setup(x => x.GetUsers()).Returns(usersOK);
            managementService.GetUsers(userOnlyNullName);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetFilterUserEmptySurnameTest()
        {
            managementMock.Setup(x => x.GetUsers()).Returns(usersOK);
            managementService.GetUsers(userOnlyEmptySurname);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetFilterUserNullSurnameTest()
        {
            managementMock.Setup(x => x.GetUsers()).Returns(usersOK);
            managementService.GetUsers(userOnlyNullSurname);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetFilterUserEmptyEmailTest()
        {
            managementMock.Setup(x => x.GetUsers()).Returns(usersOK);
            managementService.GetUsers(userOnlyEmptyEmail);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetFilterUserNullEmailTest()
        {
            managementMock.Setup(x => x.GetUsers()).Returns(usersOK);
            managementService.GetUsers(userOnlyNullEmail);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertNullTest()
        {
            managementService.InsertUser(userNull);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertEmptyNameTest()
        {
            managementService.InsertUser(userEmptyName);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertNullNameTest()
        {
            managementService.InsertUser(userNullName);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertEmptySurnameTest()
        {
            managementService.InsertUser(userEmptySurname);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertNullSurnameTest()
        {
            managementService.InsertUser(userNullSurname);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertEmptyEmailTest()
        {
            managementService.InsertUser(userEmptyEmail);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertNullEmailTest()
        {
            managementService.InsertUser(userNullEmail);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertInvalidEmailTest()
        {
            managementService.InsertUser(userInvalidEmail);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertUniqueEmailTest()
        {
            managementMock.Setup(x => x.GetUsers(It.IsAny<Func<User, bool>>())).Returns(usersOK);
            managementService.InsertUser(userOK);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertEmptyPasswordTest()
        {
            managementService.InsertUser(userEmptyPassword);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertNullPasswordTest()
        {
            managementService.InsertUser(userNullPassword);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertAtLeastOneRoleTest()
        {
            managementService.InsertUser(userEmptyRol);
        }

        [TestMethod]
        public void InsertUserOkTest()
        {
            managementMock.Setup(x => x.GetUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User>());
            managementMock.Setup(x => x.InsertUser(userOK));
            managementMock.Setup(x => x.Save());
            managementService.InsertUser(userOK);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateNullTest()
        {
            managementMock.Setup(x => x.GetUserById(userOK.UserId)).Returns(userNull);
            managementService.UpdateUser(userNull);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateNullNameTest()
        {
            managementMock.Setup(x => x.GetUserById(userOK.UserId)).Returns(userOK);
            managementService.UpdateUser(userEmptyName);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateEmptyNameTest()
        {
            managementMock.Setup(x => x.GetUserById(userOK.UserId)).Returns(userOK);
            managementService.UpdateUser(userNullName);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateEmptySurnameTest()
        {
            managementMock.Setup(x => x.GetUserById(userOK.UserId)).Returns(userOK);
            managementService.UpdateUser(userEmptySurname);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateNullSurnameTest()
        {
            managementMock.Setup(x => x.GetUserById(userOK.UserId)).Returns(userOK);
            managementService.UpdateUser(userNullSurname);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void UpdateUserNonExistTest()
        {
            managementMock.Setup(x => x.GetUserById(userOK.UserId)).Returns(userNull);
            managementService.UpdateUser(userOK);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void UpdateUserOkTest()
        {
            managementMock.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(userOK);
            managementMock.Setup(x => x.UpdateUser(userOK));
            managementMock.Setup(x => x.Save());
            managementService.UpdateUser(userOK);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void DeleteUserLessThanZeroTest()
        {
            managementService.DeleteUser(userIdZero);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void DeleteUserNonExistTest()
        {
            managementMock.Setup(x => x.GetUserById(userIdInexistant)).Returns(userNull);
            managementService.DeleteUser(userIdInexistant);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void DeleteUserOkTest()
        {
            managementMock.Setup(x => x.GetUserById(userOK.UserId)).Returns(userOK);
            managementMock.Setup(x => x.DeleteUser(userOK));
            managementMock.Setup(x => x.Save());
            managementService.DeleteUser(userOK.UserId);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void ChangePasswordInvalidUserTest()
        {
            managementMock.Setup(x => x.GetUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User>());
            managementService.ChangePassword(new UserChangePassword(userOK.Email, "TestPass", "newPass"));
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ChangeEmptyEmailTest()
        {
            managementMock.Setup(x => x.GetUsers(It.IsAny<Func<User, bool>>())).Returns(usersOK);
            managementService.ChangePassword(new UserChangePassword("", "TestPass", "newPass"));
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ChangeNullEmailTest()
        {
            managementMock.Setup(x => x.GetUsers(It.IsAny<Func<User, bool>>())).Returns(usersOK);
            managementService.ChangePassword(new UserChangePassword(null, "TestPass", "newPass"));
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ChangeEmptyNewPasswordTest()
        {
            managementMock.Setup(x => x.GetUsers(It.IsAny<Func<User, bool>>())).Returns(usersOK);
            managementService.ChangePassword(new UserChangePassword(userOK.Email, "TestPass", ""));
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ChangeNullNewPasswordTest()
        {
            managementMock.Setup(x => x.GetUsers(It.IsAny<Func<User, bool>>())).Returns(usersOK);
            managementService.ChangePassword(new UserChangePassword(userOK.Email, "TestPass", null));
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ChangeEmptyOldPasswordTest()
        {
            managementMock.Setup(x => x.GetUsers(It.IsAny<Func<User, bool>>())).Returns(usersOK);
            managementService.ChangePassword(new UserChangePassword(userOK.Email, "", "newPass"));
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ChangeNullOldPasswordTest()
        {
            managementMock.Setup(x => x.GetUsers(It.IsAny<Func<User, bool>>())).Returns(usersOK);
            managementService.ChangePassword(new UserChangePassword(userOK.Email, null, "newPass"));
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ChangeOldDifferentPasswordTest()
        {
            managementMock.Setup(x => x.GetUsers(It.IsAny<Func<User, bool>>())).Returns(usersOK);
            managementService.ChangePassword(new UserChangePassword(userOK.Email, "testuserbad", "newPass"));
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void ChangePasswordOkTest()
        {
            managementMock.Setup(x => x.GetUsers(It.IsAny<Func<User, bool>>())).Returns(usersOK);
            managementMock.Setup(x => x.UpdateUserHeader(userOK));
            managementMock.Setup(x => x.Save());
            managementService.ChangePassword(new UserChangePassword(userOK.Email, "testuser123", "NewPass"));
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ChangePasswordNull()
        {
            managementService.ChangePassword(null);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateLoggedInNullTest()
        {
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userNull);
            managementService.UpdateUser(It.IsAny<string>(), userNull);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateLoggedInNullNameTest()
        {
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userOK);
            managementService.UpdateUser(It.IsAny<string>(), userEmptyName);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateLoggedInEmptyNameTest()
        {
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userOK);
            managementService.UpdateUser(It.IsAny<string>(), userNullName);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateLoggedInEmptySurnameTest()
        {
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userOK);
            managementService.UpdateUser(It.IsAny<string>(), userEmptySurname);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateLoggedInNullSurnameTest()
        {
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userOK);
            managementService.UpdateUser(It.IsAny<string>(), userNullSurname);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void UpdateLoggedInUserNonExistTest()
        {
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userNull);
            managementService.UpdateUser(It.IsAny<string>(), userOK);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void UpdateLoggedInUserOkTest()
        {
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userOK);
            managementMock.Setup(x => x.UpdateUser(userOK));
            managementMock.Setup(x => x.Save());
            managementService.UpdateUser(It.IsAny<string>(), userOK);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void ChangePasswordLoggedInUserInvalidUserTest()
        {
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userNull);
            managementService.ChangePassword(It.IsAny<string>(), new UserChangePassword(userOK.Email, "TestPass", "newPass"));
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ChangeLoggedInUserEmptyEmailTest()
        {
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userOK);
            managementService.ChangePassword(It.IsAny<string>(), new UserChangePassword("", "TestPass", "newPass"));
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ChangeLoggedInUserNullEmailTest()
        {
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userOK);
            managementService.ChangePassword(It.IsAny<string>(), new UserChangePassword(null, "TestPass", "newPass"));
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ChangeLoggedInUserEmptyNewPasswordTest()
        {
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userOK);
            managementService.ChangePassword(It.IsAny<string>(), new UserChangePassword(userOK.Email, "TestPass", ""));
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ChangeLoggedInUserNullNewPasswordTest()
        {
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userOK);
            managementService.ChangePassword(It.IsAny<string>(), new UserChangePassword(userOK.Email, "TestPass", null));
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ChangeLoggedInUserEmptyOldPasswordTest()
        {
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userOK);
            managementService.ChangePassword(It.IsAny<string>(), new UserChangePassword(userOK.Email, "", "newPass"));
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ChangeLoggedInUserNullOldPasswordTest()
        {
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userOK);
            managementService.ChangePassword(It.IsAny<string>(), new UserChangePassword(userOK.Email, null, "newPass"));
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ChangeLoggedInUserOldDifferentPasswordTest()
        {
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userOK);
            managementService.ChangePassword(It.IsAny<string>(), new UserChangePassword(userOK.Email, "testuserbad", "newPass"));
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void ChangePasswordLoggedInUserOkTest()
        {
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userOK);
            managementMock.Setup(x => x.UpdateUserHeader(userOK));
            managementMock.Setup(x => x.Save());
            managementService.ChangePassword(It.IsAny<string>(), new UserChangePassword(userOK.Email, "testuser123", "NewPass"));
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ChangePasswordLoggedInUserNull()
        {
            managementService.ChangePassword(It.IsAny<string>(), null);
            managementMock.VerifyAll();
        }
    }
}
