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
    public class BandsManagementTest : ManagementTest
    {

        private DbContext context;
        private BandsManagement management;

        private Band bandOK;
        private Band bandNotExists;
        private List<Band> bandsOK;
        private List<Band> bandsAdded;
        private Gender genderOK;
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

            bandOK = new Band()
            {
                MusicalProtagonistId = 1,
                Name = "Nirvana",
                Artists = new List<ArtistBand>() {
                    new ArtistBand()
                    {
                        ArtistId = 1,
                        Artist = new Artist()
                        {
                            ArtistId = 1,
                            Name = "Dummy artist"
                        },
                        RoleArtist = roleArtistOK
                    }
                },
                Gender = genderOK,
                GenderId = genderOK.GenderId
            };

            bandNotExists = new Band()
            {
                MusicalProtagonistId = 2,
                Name = "Foo Fighters",
                Artists = new List<ArtistBand>() {
                    new ArtistBand()
                    {
                        ArtistId = 2,
                        Artist = new Artist()
                        {
                            ArtistId = 2,
                            Name = "Dummy artist 2"
                        },
                        RoleArtist = roleArtistOK
                    }
                },
                Gender = genderOK,
                GenderId = genderOK.GenderId
            };

            bandsOK = new List<Band>
            {
                bandOK
            };

            bandsAdded = new List<Band>
            {
                bandOK,
                bandNotExists
            };

            CreateDataBase();
        }

        private void CreateDataBase()
        {
            context = CreateDbContext();
            context.Set<Band>().Add(bandOK);
            foreach (ArtistBand artist in bandOK.Artists)
            {
                context.Set<Artist>().Add(artist.Artist);
            }
            foreach (ArtistBand artist in bandNotExists.Artists)
            {
                context.Set<Artist>().Add(artist.Artist);
            }
            context.SaveChanges();

            management = new BandsManagement(context);
        }

        [TestMethod]
        public void GetTest()
        {
            var result = management.GetBands().ToList();
            Assert.IsTrue(bandsOK.SequenceEqual(result));
        }

        [TestMethod]
        public void GetWithFilterThatExistsTest()
        {
            Func<Band, bool> filter = new Func<Band, bool>(x => x.Name == bandOK.Name);
            var result = management.GetBands(filter).ToList();
            Assert.IsTrue(bandsOK.SequenceEqual(result));
        }

        [TestMethod]
        public void GetWithFilterThatNotExistsTest()
        {
            Func<Band, bool> filter = new Func<Band, bool>(x => x.Name == bandNotExists.Name);
            int size = management.GetBands(filter).ToList().Count;
            Assert.AreEqual(0, size);
        }

        [TestMethod]
        public void GetWithFilterByIdTest()
        {
            Func<Band, bool> filter = new Func<Band, bool>(x => x.MusicalProtagonistId == bandOK.MusicalProtagonistId);
            var result = management.GetBands(filter).ToList();
            Assert.IsTrue(bandsOK.SequenceEqual(result));
        }

        [TestMethod]
        public void GetById()
        {
            Band band = management.GetBandById(bandOK.MusicalProtagonistId);
            Assert.AreEqual(bandOK, band);
        }

        [TestMethod]
        public void InsertTest()
        {
            management.InsertBand(bandNotExists);
            management.Save();
            var result = management.GetBands().ToList();
            Assert.IsTrue(bandsAdded.SequenceEqual(result));
        }

        [TestMethod]
        public void UpdateTest()
        {
            bandOK.Name = "Nirvanak";
            management.UpdateBand(bandOK);
            management.Save();
            string newName = management.GetBands().Where(g => g.MusicalProtagonistId == bandOK.MusicalProtagonistId).First().Name;
            Assert.AreEqual("Nirvanak", newName);
        }

        [TestMethod]
        public void DeleteTest()
        {
            management.DeleteBand(bandOK);
            management.Save();
            int size = management.GetBands().ToList().Count;
            Assert.AreEqual(0, size);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.context.Database.EnsureDeleted();
        }
    }
}
