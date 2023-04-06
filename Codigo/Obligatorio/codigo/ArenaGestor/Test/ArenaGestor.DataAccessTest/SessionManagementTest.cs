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
    public class SessionManagementTest : ManagementTest
    {
        private DbContext context;
        private SessionManagement management;

        private string randomToken;
        private Session sessionOK;
        private Session sessionNotExists;
        private List<Session> sessionsOK;
        private List<Session> sessionsAdded;

        [TestInitialize]
        public void InitTest()
        {
            randomToken = BusinessHelpers.StringGenerator.GenerateRandomToken(64);

            User userOK = new User()
            {
                UserId = 1,
                Name = "Test",
                Surname = "User",
                Email = "test@user.com",
                Password = "testuser123",
                Roles = new List<UserRole>() {
                    new UserRole()
                    {
                        UserId = 1,
                        RoleId = RoleCode.Administrador,
                        Role = new Role()
                        {
                            RoleId = RoleCode.Administrador,
                            Name = "Administrador"
                        }
                    }
                }
            };

            sessionOK = new Session()
            {
                SessionId = 1,
                Token = randomToken,
                Created = DateTime.Now,
                User = userOK
            };

            sessionNotExists = new Session()
            {
                SessionId = 2,
                Token = BusinessHelpers.StringGenerator.GenerateRandomToken(64),
                Created = DateTime.Now,
                User = userOK
            };

            sessionsOK = new List<Session>
            {
                sessionOK
            };

            sessionsAdded = new List<Session>
            {
                sessionOK,
                sessionNotExists
            };

            CreateDataBase();
        }

        private void CreateDataBase()
        {
            context = CreateDbContext();
            context.Set<Role>().Add(sessionOK.User.Roles.FirstOrDefault().Role);
            context.Set<UserRole>().Add(sessionOK.User.Roles.FirstOrDefault());
            context.Set<Session>().Add(sessionOK);
            context.SaveChanges();
            Func<Session, bool> filter = new Func<Session, bool>(x => x.Token == randomToken);
            management = new SessionManagement(context);
            int size = management.GetSessions(filter).ToList().Count;
        }

        [TestMethod]
        public void GetWithFilterThatExistsTest()
        {
            Func<Session, bool> filter = new Func<Session, bool>(x => x.Token == randomToken);
            var result = management.GetSessions(filter).ToList();
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void GetWithFilterThatNotExistsTest()
        {
            Func<Session, bool> filter = new Func<Session, bool>(x => x.UserId == 2);
            int size = management.GetSessions(filter).ToList().Count;
            Assert.AreEqual(0, size);
        }

        [TestMethod]
        public void InsertTest()
        {
            management.InsertSession(sessionNotExists);
            management.Save();
            Func<Session, bool> filter = new Func<Session, bool>(x => true);
            var result = management.GetSessions(filter).ToList();
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void DeleteTest()
        {
            management.DeleteSession(sessionOK);
            management.Save();
            Func<Session, bool> filter = new Func<Session, bool>(x => true);
            int size = management.GetSessions(filter).ToList().Count;
            Assert.AreEqual(0, size);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.context.Database.EnsureDeleted();
        }
    }
}
