using ArenaGestor.Business;
using ArenaGestor.BusinessInterface;
using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArenaGestor.BusinessTest
{
    [TestClass]
    public class SoloistsServiceTest
    {
        private Mock<ISoloistsManagement> managementMock;
        private SoloistsService managementService;
        private Mock<IGendersService> serviceGenderMock;
        private Mock<IArtistsService> serviceArtistMock;

        private Soloist soloistOK;
        private Soloist soloistWithConcerts;
        private Soloist soloistNull;
        private Soloist soloistEmptyName;
        private Soloist soloistNullName;
        private Soloist soloistNullArtist;
        private Soloist soloistArtistNonExist;
        private Soloist soloistNullGender;
        private Soloist soloistGenderNonExist;
        private Soloist soloistNonValidStartDateFuture;
        private Soloist soloistNonValidStartDatePast;
        private RoleArtist roleArtistOK;

        private IEnumerable<Soloist> soloistsOK;
        private IEnumerable<Soloist> soloistsEmpty;
        private Artist artistOK;
        private Artist artistNonExist;

        private Gender genderOK;
        private Gender genderNull;
        private Artist artistNull;

        private int soloistIdZero;
        private int soloistIdInexistant;

        [TestInitialize]
        public void InitTest()
        {
            serviceArtistMock = new Mock<IArtistsService>(MockBehavior.Strict);
            serviceGenderMock = new Mock<IGendersService>(MockBehavior.Strict);

            managementMock = new Mock<ISoloistsManagement>(MockBehavior.Strict);
            managementService = new SoloistsService(managementMock.Object, serviceGenderMock.Object, serviceArtistMock.Object);

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
                Name = "Kurt Cobain"
            };

            artistNonExist = new Artist()
            {
                ArtistId = 2,
                Name = "Robert Smith"
            };

            soloistEmptyName = new Soloist()
            {
                MusicalProtagonistId = 1,
                Name = "",
                Gender = genderOK,
                Artist = artistOK,
                StartDate = new DateTime(1987, 08, 01),
                RoleArtist = roleArtistOK
            };

            soloistNullName = new Soloist()
            {
                MusicalProtagonistId = 1,
                Name = null,
                Gender = genderOK,
                Artist = artistOK,
                StartDate = new DateTime(1987, 08, 01),
                RoleArtist = roleArtistOK
            };

            soloistNullArtist = new Soloist()
            {
                MusicalProtagonistId = 1,
                Name = "Kurt Cobain",
                Gender = genderOK,
                Artist = null,
                StartDate = new DateTime(1987, 08, 01),
                RoleArtist = roleArtistOK
            };

            soloistArtistNonExist = new Soloist()
            {
                MusicalProtagonistId = 1,
                Name = "Kurt Cobain",
                Gender = genderOK,
                GenderId = 1,
                Artist = artistNonExist,
                ArtistId = 10,
                StartDate = new DateTime(1987, 08, 01),
                RoleArtistId = roleArtistOK.RoleArtistId
            };

            soloistNullGender = new Soloist()
            {
                MusicalProtagonistId = 1,
                Name = "Kurt Cobain",
                Gender = null,
                Artist = artistOK,
                StartDate = new DateTime(1987, 08, 01),
                RoleArtist = roleArtistOK
            };

            soloistGenderNonExist = new Soloist()
            {
                MusicalProtagonistId = 1,
                Name = "Kurt Cobain",
                GenderId = 2,
                ArtistId = 1,
                StartDate = new DateTime(1987, 08, 01),
                RoleArtistId = roleArtistOK.RoleArtistId
            };

            soloistNonValidStartDateFuture = new Soloist()
            {
                MusicalProtagonistId = 1,
                Name = "Kurt Cobain",
                Gender = genderOK,
                Artist = artistOK,
                StartDate = DateTime.Now.AddDays(1),
                RoleArtist = roleArtistOK
            };

            soloistNonValidStartDatePast = new Soloist()
            {
                MusicalProtagonistId = 1,
                Name = "Kurt Cobain",
                Gender = genderOK,
                Artist = artistOK,
                StartDate = DateTime.Now.AddDays(-1).AddYears(-50),
                RoleArtist = roleArtistOK
            };

            soloistOK = new Soloist()
            {
                MusicalProtagonistId = 1,
                Name = "Kurt Cobain",
                GenderId = 1,
                ArtistId = 1,
                StartDate = new DateTime(1987, 08, 01),
                RoleArtist = roleArtistOK,
                RoleArtistId = roleArtistOK.RoleArtistId
            };

            soloistWithConcerts = new Soloist()
            {
                MusicalProtagonistId = 1,
                Name = "Kurt Cobain",
                GenderId = 1,
                ArtistId = 1,
                StartDate = new DateTime(1987, 08, 01),
                RoleArtist = roleArtistOK,
                Concerts = new List<ConcertProtagonist>()
                {
                    new ConcertProtagonist()
                    {
                        Concert = new Concert()
                        {
                            TourName = "Test concert"
                        }
                    }
                }
            };

            soloistNull = null;
            genderNull = null;
            artistNull = null;
            soloistsOK = new List<Soloist>() { soloistOK };
            soloistsEmpty = new List<Soloist>();
            soloistIdZero = 0;
            soloistIdInexistant = 2;
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void GetSoloistByIdInvalidIdTest()
        {
            managementService.GetSoloistById(soloistIdZero);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void GetSoloistByIdNonExistTest()
        {
            managementMock.Setup(x => x.GetSoloistById(soloistIdInexistant)).Returns(soloistNull);
            managementService.GetSoloistById(soloistIdInexistant);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetSoloistByIdOkTest()
        {
            managementMock.Setup(x => x.GetSoloistById(soloistOK.MusicalProtagonistId)).Returns(soloistOK);
            managementService.GetSoloistById(soloistOK.MusicalProtagonistId);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetAllSoloistsTest()
        {
            managementMock.Setup(x => x.GetSoloists()).Returns(soloistsOK);
            managementService.GetSoloists();
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertNullTest()
        {
            managementService.InsertSoloist(soloistNull);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertEmptyNameTest()
        {
            managementService.InsertSoloist(soloistEmptyName);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertNullNameTest()
        {
            managementService.InsertSoloist(soloistNullName);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertNullArtistTest()
        {
            managementService.InsertSoloist(soloistNullArtist);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]

        public void InsertNonExistArtistTest()
        {
            serviceArtistMock.Setup(x => x.GetArtistById(It.IsAny<int>())).Returns(artistNull);
            serviceGenderMock.Setup(x => x.GetGenderById(It.IsAny<int>())).Returns(genderOK);

            managementService.InsertSoloist(soloistArtistNonExist);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]

        public void InsertNullGenderTest()
        {
            serviceArtistMock.Setup(x => x.GetArtistById(It.IsAny<int>())).Returns(artistOK);
            serviceGenderMock.Setup(x => x.GetGenderById(It.IsAny<int>())).Returns(genderNull);

            managementService.InsertSoloist(soloistNullGender);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]

        public void InsertNonExistGenderTest()
        {
            serviceArtistMock.Setup(x => x.GetArtistById(It.IsAny<int>())).Returns(artistOK);
            serviceGenderMock.Setup(x => x.GetGenderById(It.IsAny<int>())).Returns(genderNull);

            managementService.InsertSoloist(soloistGenderNonExist);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertNonValidStartDateFutureTest()
        {
            serviceArtistMock.Setup(x => x.GetArtistById(It.IsAny<int>())).Returns(artistOK);
            serviceGenderMock.Setup(x => x.GetGenderById(It.IsAny<int>())).Returns(genderOK);

            managementService.InsertSoloist(soloistNonValidStartDateFuture);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertNonValidStartDatePastTest()
        {
            serviceArtistMock.Setup(x => x.GetArtistById(It.IsAny<int>())).Returns(artistOK);
            serviceGenderMock.Setup(x => x.GetGenderById(It.IsAny<int>())).Returns(genderOK);

            managementService.InsertSoloist(soloistNonValidStartDatePast);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertSoloistNameExists()
        {
            serviceArtistMock.Setup(x => x.GetArtistById(It.IsAny<int>())).Returns(artistOK);
            serviceGenderMock.Setup(x => x.GetGenderById(It.IsAny<int>())).Returns(genderOK);
            managementMock.Setup(x => x.GetSoloists(It.IsAny<Func<Soloist, bool>>())).Returns(soloistsOK);
            managementService.InsertSoloist(soloistOK);
        }

        [TestMethod]
        public void InsertSoloistOkTest()
        {
            managementMock.Setup(x => x.GetSoloists(It.IsAny<Func<Soloist, bool>>())).Returns(soloistsEmpty);
            serviceArtistMock.Setup(x => x.GetArtistById(It.IsAny<int>())).Returns(artistOK);
            serviceGenderMock.Setup(x => x.GetGenderById(It.IsAny<int>())).Returns(genderOK);
            managementMock.Setup(x => x.InsertSoloist(soloistOK));
            managementMock.Setup(x => x.Save());
            managementService.InsertSoloist(soloistOK);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateNullTest()
        {
            managementService.UpdateSoloist(soloistNull);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateEmptyNameTest()
        {
            managementService.UpdateSoloist(soloistEmptyName);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateNullNameTest()
        {
            managementService.UpdateSoloist(soloistNullName);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void UpdateSoloistNonExistTest()
        {
            serviceArtistMock.Setup(x => x.GetArtistById(It.IsAny<int>())).Returns(artistOK);
            serviceGenderMock.Setup(x => x.GetGenderById(It.IsAny<int>())).Returns(genderOK);
            managementMock.Setup(x => x.GetSoloistById(soloistOK.MusicalProtagonistId)).Returns(soloistNull);
            managementService.UpdateSoloist(soloistOK);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void UpdateSoloistOkTest()
        {
            serviceArtistMock.Setup(x => x.GetArtistById(It.IsAny<int>())).Returns(artistOK);
            serviceGenderMock.Setup(x => x.GetGenderById(It.IsAny<int>())).Returns(genderOK);
            managementMock.Setup(x => x.GetSoloistById(It.IsAny<int>())).Returns(soloistOK);
            managementMock.Setup(x => x.UpdateSoloist(soloistOK));
            managementMock.Setup(x => x.Save());
            managementService.UpdateSoloist(soloistOK);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void DeleteSoloistInvalidIdTest()
        {
            managementService.DeleteSoloist(soloistIdZero);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void DeleteSoloistNonExistTest()
        {
            managementMock.Setup(x => x.GetSoloistById(soloistIdInexistant)).Returns(soloistNull);
            managementService.DeleteSoloist(soloistIdInexistant);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void DeleteSoloistWithConcertsTest()
        {
            managementMock.Setup(x => x.GetSoloistById(soloistIdInexistant)).Returns(soloistWithConcerts);
            managementService.DeleteSoloist(soloistIdInexistant);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void DeleteSoloistOkTest()
        {
            managementMock.Setup(x => x.GetSoloistById(soloistOK.MusicalProtagonistId)).Returns(soloistOK);
            managementMock.Setup(x => x.DeleteSoloist(soloistOK));
            managementMock.Setup(x => x.Save());
            managementService.DeleteSoloist(soloistOK.MusicalProtagonistId);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetFilterSoloistNullNameTest()
        {
            managementMock.Setup(x => x.GetSoloists()).Returns(soloistsOK);
            managementService.GetSoloists(soloistNullName);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetFilterSoloistEmptyNameTest()
        {
            managementMock.Setup(x => x.GetSoloists()).Returns(soloistsOK);
            managementService.GetSoloists(soloistEmptyName);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetFilterSoloistTest()
        {
            managementMock.Setup(x => x.GetSoloists(It.IsAny<Func<Soloist, bool>>())).Returns(soloistsOK);
            managementService.GetSoloists(soloistOK);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void SoloistByArtistNull()
        {
            IEnumerable<Soloist> soloists = managementService.GetSoloistsByArtist(null);
            Assert.AreEqual(0, soloists.Count());
        }

        [TestMethod]
        public void SoloistByArtistOk()
        {
            managementMock.Setup(x => x.GetSoloists(It.IsAny<Func<Soloist, bool>>())).Returns(soloistsOK);
            IEnumerable<Soloist> soloists = managementService.GetSoloistsByArtist(artistOK);
            managementMock.VerifyAll();
        }

    }
}
