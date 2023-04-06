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
    public class ArtistsServiceTest
    {
        private Mock<IArtistsManagement> managementMock;
        private Mock<IUsersService> usersServiceMock;
        private ArtistsService managementService;

        private Artist artistOK;
        private Artist artistNull;
        private Artist artistEmptyName;
        private Artist artistNullName;
        private Artist artistWithBands;
        private Artist artistWithSoloists;
        private Artist artistNullBands;
        private Artist artistNullSoloists;
        private Artist artistWithUserOK;
        private User userOK;
        private User userNoArtist;

        private IEnumerable<Artist> artistsOK;
        private IEnumerable<Artist> artistsEmpty;
        private int artistIdZero;
        private int artistIdInexistant;

        [TestInitialize]
        public void InitTest()
        {
            managementMock = new Mock<IArtistsManagement>(MockBehavior.Strict);
            usersServiceMock = new Mock<IUsersService>(MockBehavior.Strict);

            managementService = new ArtistsService(managementMock.Object, usersServiceMock.Object);

            UserRole roleOk = new UserRole()
            {
                RoleId = RoleCode.Artista
            };

            List<UserRole> rolesOk = new List<UserRole>() {
                roleOk
            };

            UserRole roleNoArtist = new UserRole()
            {
                RoleId = RoleCode.Administrador
            };

            List<UserRole> rolesNoArtist = new List<UserRole>() {
                roleNoArtist
            };

            userOK = new User()
            {
                UserId = 1,
                Roles = rolesOk
            };

            userNoArtist = new User()
            {
                UserId = 1,
                Roles = rolesNoArtist
            };

            artistOK = new Artist()
            {
                ArtistId = 1,
                Name = "Kurt Cobain"
            };

            artistWithUserOK = new Artist()
            {
                ArtistId = 1,
                Name = "Kurt Cobain",
                UserId = 1,
                User = new User()
                {
                    UserId = 1
                }
            };

            artistEmptyName = new Artist()
            {
                ArtistId = 1,
                Name = ""
            };

            artistNullName = new Artist()
            {
                ArtistId = 1,
                Name = null
            };

            artistWithBands = new Artist()
            {
                ArtistId = 1,
                Name = "Kurt Cobain",
                Bands = new List<ArtistBand>()
                {
                    new ArtistBand()
                    {
                        Band = new Band(){
                            Name = "Test Band"
                        }
                    }
                }
            };

            artistWithSoloists = new Artist()
            {
                ArtistId = 1,
                Name = "Kurt Cobain",
                Soloists = new List<Soloist>()
                {
                    new Soloist()
                    {
                        Name = "Test soloist"
                    }
                }
            };

            artistNullBands = new Artist()
            {
                ArtistId = 1,
                Name = "Kurt Cobain",
                Bands = null
            };

            artistNullSoloists = new Artist()
            {
                ArtistId = 1,
                Name = "Kurt Cobain",
                Soloists = null
            };

            artistNull = null;
            artistsOK = new List<Artist>() { artistOK };
            artistsEmpty = new List<Artist>();
            artistIdZero = 0;
            artistIdInexistant = 2;
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void GetArtistByIdInvalidIdTest()
        {
            managementService.GetArtistById(artistIdZero);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void GetArtistByIdNonExistTest()
        {
            managementMock.Setup(x => x.GetArtistById(artistIdInexistant)).Returns(artistNull);
            managementService.GetArtistById(artistIdInexistant);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetArtistByIdOkTest()
        {
            managementMock.Setup(x => x.GetArtistById(artistOK.ArtistId)).Returns(artistOK);
            managementService.GetArtistById(artistOK.ArtistId);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetAllArtistsTest()
        {
            managementMock.Setup(x => x.GetArtists()).Returns(artistsOK);
            managementService.GetArtists();
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetFilterArtistTest()
        {
            managementMock.Setup(x => x.GetArtists(It.IsAny<Func<Artist, bool>>())).Returns(artistsOK);
            managementService.GetArtists(artistOK);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetFilterArtistEmptyNameTest()
        {
            managementMock.Setup(x => x.GetArtists()).Returns(artistsOK);
            managementService.GetArtists(artistEmptyName);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetFilterArtistNullNameTest()
        {
            managementMock.Setup(x => x.GetArtists()).Returns(artistsOK);
            managementService.GetArtists(artistNullName);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertNullTest()
        {
            managementService.InsertArtist(artistNull);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertEmptyNameTest()
        {
            managementService.InsertArtist(artistEmptyName);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertNullNameTest()
        {
            managementService.InsertArtist(artistNullName);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertArtistNameExists()
        {
            managementMock.Setup(x => x.GetArtists(It.IsAny<Func<Artist, bool>>())).Returns(artistsOK);
            managementService.InsertArtist(artistOK);
        }

        [TestMethod]
        public void InsertArtistWithUserOkTest()
        {
            managementMock.Setup(x => x.GetArtists(It.IsAny<Func<Artist, bool>>())).Returns(artistsEmpty);
            usersServiceMock.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(userOK);

            managementMock.Setup(x => x.InsertArtist(artistWithUserOK));
            managementMock.Setup(x => x.Save());
            managementService.InsertArtist(artistWithUserOK);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void InsertArtistWithUserNoArtistTest()
        {
            managementMock.Setup(x => x.GetArtists(It.IsAny<Func<Artist, bool>>())).Returns(artistsEmpty);
            usersServiceMock.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(userNoArtist);

            managementService.InsertArtist(artistWithUserOK);
        }

        [TestMethod]
        public void InsertArtistOkTest()
        {
            managementMock.Setup(x => x.GetArtists(It.IsAny<Func<Artist, bool>>())).Returns(artistsEmpty);
            managementMock.Setup(x => x.InsertArtist(artistOK));
            managementMock.Setup(x => x.Save());
            managementService.InsertArtist(artistOK);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateNullTest()
        {
            managementService.UpdateArtist(artistNull);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateEmptyNameTest()
        {
            managementService.UpdateArtist(artistEmptyName);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateNullNameTest()
        {
            managementService.UpdateArtist(artistNullName);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void UpdateArtistNonExistTest()
        {
            managementMock.Setup(x => x.GetArtistById(artistOK.ArtistId)).Returns(artistNull);
            managementService.UpdateArtist(artistOK);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void UpdateArtistWithUserOkTest()
        {
            managementMock.Setup(x => x.GetArtistById(It.IsAny<int>())).Returns(artistOK);
            usersServiceMock.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(userOK);

            managementMock.Setup(x => x.UpdateArtist(artistWithUserOK));
            managementMock.Setup(x => x.Save());
            managementService.UpdateArtist(artistWithUserOK);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void UpdateArtistWithUserNoArtistTest()
        {
            managementMock.Setup(x => x.GetArtistById(It.IsAny<int>())).Returns(artistOK);
            usersServiceMock.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(userNoArtist);

            managementService.UpdateArtist(artistWithUserOK);
        }

        [TestMethod]
        public void UpdateArtistOkTest()
        {
            managementMock.Setup(x => x.GetArtistById(It.IsAny<int>())).Returns(artistOK);
            managementMock.Setup(x => x.UpdateArtist(artistOK));
            managementMock.Setup(x => x.Save());
            managementService.UpdateArtist(artistOK);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void DeleteArtistInvalidIdTest()
        {
            managementService.DeleteArtist(artistIdZero);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void DeleteArtistNonExistTest()
        {
            managementMock.Setup(x => x.GetArtistById(artistIdInexistant)).Returns(artistNull);
            managementService.DeleteArtist(artistIdInexistant);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void DeleteArtistWithBands()
        {
            managementMock.Setup(x => x.GetArtistById(artistIdInexistant)).Returns(artistWithBands);
            managementService.DeleteArtist(artistIdInexistant);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void DeleteArtistWithSoloists()
        {
            managementMock.Setup(x => x.GetArtistById(It.IsAny<int>())).Returns(artistWithSoloists);
            managementMock.Setup(x => x.DeleteArtist(artistOK));
            managementMock.Setup(x => x.Save());
            managementService.DeleteArtist(artistOK.ArtistId);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void DeleteArtistWithNullSoloists()
        {
            managementMock.Setup(x => x.GetArtistById(It.IsAny<int>())).Returns(artistNullSoloists);
            managementMock.Setup(x => x.DeleteArtist(artistOK));
            managementMock.Setup(x => x.Save());
            managementService.DeleteArtist(artistOK.ArtistId);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void DeleteArtistWithNullBands()
        {
            managementMock.Setup(x => x.GetArtistById(It.IsAny<int>())).Returns(artistNullBands);
            managementMock.Setup(x => x.DeleteArtist(artistOK));
            managementMock.Setup(x => x.Save());
            managementService.DeleteArtist(artistOK.ArtistId);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void DeleteArtistOkTest()
        {
            managementMock.Setup(x => x.GetArtistById(It.IsAny<int>())).Returns(artistOK);
            managementMock.Setup(x => x.DeleteArtist(artistOK));
            managementMock.Setup(x => x.Save());
            managementService.DeleteArtist(artistOK.ArtistId);
            managementMock.VerifyAll();
        }
    }
}
