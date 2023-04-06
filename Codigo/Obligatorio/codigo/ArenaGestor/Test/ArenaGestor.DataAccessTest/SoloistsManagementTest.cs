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
    public class SoloistsManagementTest : ManagementTest
    {

        private DbContext context;
        private SoloistsManagement management;

        private Soloist soloistOK;
        private Soloist soloistNotExists;
        private List<Soloist> soloistsOK;
        private List<Soloist> soloistsAdded;
        private Gender genderOK;
        private Artist artistOK;
        private RoleArtist roleArtistOK;

        [TestInitialize]
        public void InitTest()
        {
            roleArtistOK = new RoleArtist()
            {
                Name = RoleArtistCode.Cantante.ToString(),
                RoleArtistId = RoleArtistCode.Cantante
            };

            genderOK = new Gender()
            {
                GenderId = 1,
                Name = "Rock"
            };

            artistOK = new Artist()
            {
                ArtistId = 1,
                Name = "Dummy artist"
            };

            soloistOK = new Soloist()
            {
                MusicalProtagonistId = 1,
                Name = "Kurt Cobain",
                Gender = genderOK,
                GenderId = genderOK.GenderId,
                Artist = artistOK,
                ArtistId = artistOK.ArtistId,
                RoleArtist = roleArtistOK
            };

            soloistNotExists = new Soloist()
            {
                MusicalProtagonistId = 2,
                Name = "Dave Grohl",
                Gender = genderOK,
                GenderId = genderOK.GenderId,
                Artist = artistOK,
                ArtistId = artistOK.ArtistId,
                RoleArtist = roleArtistOK
            };

            soloistsOK = new List<Soloist>
            {
                soloistOK
            };

            soloistsAdded = new List<Soloist>
            {
                soloistOK,
                soloistNotExists
            };

            CreateDataBase();
        }

        private void CreateDataBase()
        {
            context = CreateDbContext();
            context.Set<Soloist>().Add(soloistOK);
            context.SaveChanges();

            management = new SoloistsManagement(context);
        }

        [TestMethod]
        public void GetTest()
        {
            var result = management.GetSoloists().ToList();
            Assert.IsTrue(soloistsOK.SequenceEqual(result));
        }

        [TestMethod]
        public void GetWithFilterThatExistsTest()
        {
            Func<Soloist, bool> filter = new Func<Soloist, bool>(x => x.Name == soloistOK.Name);
            var result = management.GetSoloists(filter).ToList();
            Assert.IsTrue(soloistsOK.SequenceEqual(result));
        }

        [TestMethod]
        public void GetWithFilterThatNotExistsTest()
        {
            Func<Soloist, bool> filter = new Func<Soloist, bool>(x => x.Name == soloistNotExists.Name);
            int size = management.GetSoloists(filter).ToList().Count;
            Assert.AreEqual(0, size);
        }

        [TestMethod]
        public void GetWithFilterByIdTest()
        {
            Func<Soloist, bool> filter = new Func<Soloist, bool>(x => x.MusicalProtagonistId == soloistOK.MusicalProtagonistId);
            var result = management.GetSoloists(filter).ToList();
            Assert.IsTrue(soloistsOK.SequenceEqual(result));
        }

        [TestMethod]
        public void GetById()
        {
            Soloist soloist = management.GetSoloistById(soloistOK.MusicalProtagonistId);
            Assert.AreEqual(soloistOK, soloist);
        }

        [TestMethod]
        public void InsertTest()
        {
            management.InsertSoloist(soloistNotExists);
            management.Save();
            var result = management.GetSoloists().ToList();
            Assert.IsTrue(soloistsAdded.SequenceEqual(result));
        }

        [TestMethod]
        public void UpdateTest()
        {
            soloistOK.Name = "Kurt Cobaink";
            management.UpdateSoloist(soloistOK);
            management.Save();
            string newName = management.GetSoloists().Where(g => g.MusicalProtagonistId == soloistOK.MusicalProtagonistId).First().Name;
            Assert.AreEqual("Kurt Cobaink", newName);
        }

        [TestMethod]
        public void DeleteTest()
        {
            management.DeleteSoloist(soloistOK);
            management.Save();
            int size = management.GetSoloists().ToList().Count;
            Assert.AreEqual(0, size);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.context.Database.EnsureDeleted();
        }
    }
}
