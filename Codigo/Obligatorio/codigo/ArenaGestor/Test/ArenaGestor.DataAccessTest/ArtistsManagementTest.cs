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
    public class ArtistsManagementTest : ManagementTest
    {
        private DbContext context;
        private ArtistsManagement management;

        private Artist artistOK;
        private Artist artistWithUserOK;
        private Artist artistNotExists;
        private Artist artistNotExistsWithUser;
        private List<Artist> artistsOK;
        private List<Artist> artistsAdded;

        [TestInitialize]
        public void InitTest()
        {
            artistOK = new Artist()
            {
                ArtistId = 1,
                Name = "Kurt Cobain",
                UserId = 1,
                Bands = new List<ArtistBand>() 
                {
                    new ArtistBand(){ }
                },
                Soloists = new List<Soloist>() 
                {
                    new Soloist(){ }
                },
            };

            artistWithUserOK = new Artist()
            {
                ArtistId = 1,
                Name = "Kurt Cobain",
                User = new User()
                {
                    UserId = 1,
                    Name = "Test",
                    Surname = "User",
                    Email = "test@user.com",
                    Password = "testuser123",
                    Roles = new List<UserRole>() {
                        new UserRole()
                        {
                            RoleId = RoleCode.Artista,
                            Role = new Role()
                            {
                                RoleId = RoleCode.Artista,
                                Name = "Artista"
                            }
                        }
                    }
                },
                Bands = new List<ArtistBand>(),
                Soloists = new List<Soloist>(),
            };

            artistNotExists = new Artist()
            {
                ArtistId = 2,
                Name = "Dave Grohl",
                UserId = 1
            };

            artistNotExistsWithUser = new Artist()
            {
                ArtistId = 2,
                Name = "Dave Grohl",
                UserId = 1
            };

            artistsOK = new List<Artist>
            {
                artistWithUserOK
            };

            artistsAdded = new List<Artist>
            {
                artistWithUserOK,
                artistNotExists
            };

            CreateDataBase();
        }

        private void CreateDataBase()
        {
            context = CreateDbContext();
            context.Set<Artist>().Add(artistWithUserOK);
            context.SaveChanges();

            management = new ArtistsManagement(context);
        }

        [TestMethod]
        public void GetTest()
        {
            var result = management.GetArtists().ToList();
            Assert.IsTrue(artistsOK.SequenceEqual(result));
        }

        [TestMethod]
        public void GetWithFilterThatExistsTest()
        {
            Func<Artist, bool> filter = new Func<Artist, bool>(x => x.Name == artistOK.Name);
            var result = management.GetArtists(filter).ToList();
            Assert.IsTrue(artistsOK.SequenceEqual(result));
        }

        [TestMethod]
        public void GetWithFilterThatNotExistsTest()
        {
            Func<Artist, bool> filter = new Func<Artist, bool>(x => x.Name == artistNotExists.Name);
            int size = management.GetArtists(filter).ToList().Count;
            Assert.AreEqual(0, size);
        }

        [TestMethod]
        public void GetWithFilterByIdTest()
        {
            Func<Artist, bool> filter = new Func<Artist, bool>(x => x.ArtistId == artistOK.ArtistId);
            var result = management.GetArtists(filter).ToList();
            Assert.IsTrue(artistsOK.SequenceEqual(result));
        }

        [TestMethod]
        public void GetById()
        {
            Artist artist = management.GetArtistById(artistOK.ArtistId);
            Assert.AreEqual(artistOK, artist);
        }

        [TestMethod]
        public void InsertTest()
        {
            management.InsertArtist(artistNotExists);
            management.Save();
            var result = management.GetArtists().ToList();
            Assert.IsTrue(artistsAdded.SequenceEqual(result));
        }

        [TestMethod]
        public void UpdateTest()
        {
            artistWithUserOK.Name = "Kurt Cobaink";
            management.UpdateArtist(artistWithUserOK);
            management.Save();
            string newName = management.GetArtists().Where(g => g.ArtistId == artistWithUserOK.ArtistId).First().Name;
            Assert.AreEqual("Kurt Cobaink", newName);
        }

        [TestMethod]
        public void InsertWithUserTest()
        {
            management.InsertArtist(artistNotExistsWithUser);
            management.Save();
            var result = management.GetArtists().ToList();
            Assert.IsTrue(artistsAdded.SequenceEqual(result));
        }

        [TestMethod]
        public void UpdateWithUserTest()
        {
            artistWithUserOK.Name = "Kurt Cobaink";
            management.UpdateArtist(artistWithUserOK);
            management.Save();
            string newName = management.GetArtists().Where(g => g.ArtistId == artistWithUserOK.ArtistId).First().Name;
            Assert.AreEqual("Kurt Cobaink", newName);
        }

        [TestMethod]
        public void DeleteTest()
        {
            management.DeleteArtist(artistWithUserOK);
            management.Save();
            int size = management.GetArtists().ToList().Count;
            Assert.AreEqual(0, size);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.context.Database.EnsureDeleted();
        }
    }
}
