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
    public class MusicalProtagonistManagementTest : ManagementTest
    {

        private DbContext context;
        private MusicalProtagonistManagement management;

        private MusicalProtagonist musicalProtagonistOk;
        private List<MusicalProtagonist> musicalProtagonistsOK;

        [TestInitialize]
        public void InitTest()
        {
            musicalProtagonistOk = new Soloist()
            {
                MusicalProtagonistId = 1,
                Name = "Test protagonist",
                ArtistId = 1,
                GenderId = 1,
                StartDate = DateTime.Now
            };

            musicalProtagonistsOK = new List<MusicalProtagonist>
            {
                musicalProtagonistOk
            };

            CreateDataBase();
        }

        private void CreateDataBase()
        {
            context = CreateDbContext();
            context.Set<MusicalProtagonist>().Add(musicalProtagonistOk);
            context.SaveChanges();

            management = new MusicalProtagonistManagement(context);
        }

        [TestMethod]
        public void GetTest()
        {
            var result = management.GetMusicalProtagonist().ToList();
            Assert.IsTrue(musicalProtagonistsOK.SequenceEqual(result));
        }

        [TestMethod]
        public void GetWithFilterThatExistsTest()
        {
            Func<MusicalProtagonist, bool> filter = new Func<MusicalProtagonist, bool>(x => x.Name == musicalProtagonistOk.Name);
            var result = management.GetMusicalProtagonist(filter).ToList();
            Assert.IsTrue(musicalProtagonistsOK.SequenceEqual(result));
        }

        [TestMethod]
        public void GetWithFilterByIdTest()
        {
            Func<MusicalProtagonist, bool> filter = new Func<MusicalProtagonist, bool>(x => x.MusicalProtagonistId == musicalProtagonistOk.MusicalProtagonistId);
            var result = management.GetMusicalProtagonist(filter).ToList();
            Assert.IsTrue(musicalProtagonistsOK.SequenceEqual(result));
        }

        [TestMethod]
        public void GetById()
        {
            MusicalProtagonist musicalProtagonist = management.GetMusicalProtagonistById(musicalProtagonistOk.MusicalProtagonistId);
            Assert.AreEqual(musicalProtagonistOk, musicalProtagonist);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.context.Database.EnsureDeleted();
        }
    }
}
