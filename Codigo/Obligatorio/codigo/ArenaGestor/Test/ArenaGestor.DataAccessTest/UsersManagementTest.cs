using ArenaGestor.DataAccess.Managements;
using ArenaGestor.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArenaGestor.DataAccessTest
{
    [TestClass]
    public class UsersManagementTest : ManagementTest
    {
        private DbContext context;
        private UsersManagement management;

        private User userOK;
        private User userNotExists;
        private List<User> usersOK;
        private List<User> usersAdded;

        [TestInitialize]
        public void InitTest()
        {
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
                        RoleId = RoleCode.Administrador,
                        Role = new Role()
                        {
                            RoleId = RoleCode.Administrador,
                            Name = "Administrador"
                        }
                    }
                }
            };

            userNotExists = new User()
            {
                UserId = 2,
                Name = "Test2",
                Surname = "User2",
                Email = "test2@user.com",
                Password = "testuser1233",
                Roles = new List<UserRole>() {
                    new UserRole()
                    {
                        RoleId = RoleCode.Vendedor,
                        Role = new Role()
                        {
                            RoleId = RoleCode.Vendedor,
                            Name = "Vendedor"
                        }
                    }
                }
            };

            usersOK = new List<User>
            {
                userOK
            };

            usersAdded = new List<User>
            {
                userOK,
                userNotExists
            };

            CreateDataBase();
        }

        private void CreateDataBase()
        {
            context = CreateDbContext();
            context.Set<User>().Add(userOK);

            foreach (UserRole role in userOK.Roles)
            {
                context.Set<Role>().Add(role.Role);
            }

            foreach (UserRole role in userNotExists.Roles)
            {
                context.Set<Role>().Add(role.Role);
            }

            context.SaveChanges();
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            management = new UsersManagement(context);
        }

        [TestMethod]
        public void GetTest()
        {
            var result = management.GetUsers().ToList();
            Assert.IsTrue(usersOK.SequenceEqual(result));
        }

        [TestMethod]
        public void GetWithFilterThatExistsTest()
        {
            Func<User, bool> filter = new Func<User, bool>(x => x.Name == userOK.Name);
            var result = management.GetUsers(filter).ToList();
            Assert.IsTrue(usersOK.SequenceEqual(result));
        }

        [TestMethod]
        public void GetWithFilterThatNotExistsTest()
        {
            Func<User, bool> filter = new Func<User, bool>(x => x.Name == userNotExists.Name);
            int size = management.GetUsers(filter).ToList().Count;
            Assert.AreEqual(0, size);
        }

        [TestMethod]
        public void GetWithFilterByIdTest()
        {
            Func<User, bool> filter = new Func<User, bool>(x => x.UserId == userOK.UserId);
            var result = management.GetUsers(filter).ToList();
            Assert.IsTrue(usersOK.SequenceEqual(result));
        }

        [TestMethod]
        public void GetById()
        {
            User user = management.GetUserById(userOK.UserId);
            Assert.AreEqual(userOK, user);
        }

        [TestMethod]
        public void InsertTest()
        {
            management.InsertUser(userNotExists);
            management.Save();
            var result = management.GetUsers().ToList();
            Assert.IsTrue(usersAdded.SequenceEqual(result));
        }

        [TestMethod]
        public void UpdateTest()
        {
            userOK.Name = "AnotherName";
            management.UpdateUser(userOK);
            management.Save();
            string newName = management.GetUsers().Where(g => g.UserId == userOK.UserId).First().Name;
            Assert.AreEqual("AnotherName", newName);
        }

        [TestMethod]
        public void UpdateHeaderTest()
        {
            userOK.Name = "AnotherName";
            management.UpdateUserHeader(userOK);
            management.Save();
            string newName = management.GetUsers().Where(g => g.UserId == userOK.UserId).First().Name;
            Assert.AreEqual("AnotherName", newName);
        }

        [TestMethod]
        public void DeleteTest()
        {
            management.DeleteUser(userOK);
            management.Save();
            int size = management.GetUsers().ToList().Count;
            Assert.AreEqual(0, size);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.context.Database.EnsureDeleted();
        }
    }
}
