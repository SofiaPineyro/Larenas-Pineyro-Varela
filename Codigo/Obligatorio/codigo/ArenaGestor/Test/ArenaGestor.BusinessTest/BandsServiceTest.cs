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
    public class BandsServiceTest
    {
        private Mock<IBandsManagement> managementMock;

        private BandsService managementService;
        private Mock<IGendersService> serviceGenderMock;
        private Mock<IArtistsService> serviceArtistMock;

        private Band bandOK;
        private Band bandWithConcerts;
        private Band bandNull;
        private Band bandEmptyName;
        private Band bandNullName;
        private Band bandEmptyArtist;
        private Band bandNullArtist;
        private Band bandNonExistArtist;
        private Band bandNullGender;
        private Band bandNonExistGender;
        private Band bandNonValidStartDate;
        private RoleArtist roleArtistOK;

        private IEnumerable<Band> bandsOK;
        private IEnumerable<Band> bandsEmpty;
        private ArtistBand artistOK;
        private List<ArtistBand> artistsOK;
        private Gender genderOK;
        private Gender genderNull;
        private Artist artistNull;
        private int bandIdZero;
        private int bandIdInexistant;

        [TestInitialize]
        public void InitTest()
        {
            serviceArtistMock = new Mock<IArtistsService>(MockBehavior.Strict);
            serviceGenderMock = new Mock<IGendersService>(MockBehavior.Strict);
            managementMock = new Mock<IBandsManagement>(MockBehavior.Strict);
            managementService = new BandsService(managementMock.Object, serviceGenderMock.Object, serviceArtistMock.Object);

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

            artistOK = new ArtistBand()
            {
                ArtistId = 1,
                Artist = new Artist()
                {
                    ArtistId = 1,
                    Name = "Kurt Cobain"
                },
                RoleArtist = roleArtistOK,
                RoleArtistId = roleArtistOK.RoleArtistId
            };

            artistsOK = new List<ArtistBand>() { artistOK };
            bandsEmpty = new List<Band>();

            bandOK = new Band()
            {
                MusicalProtagonistId = 1,
                Name = "Nirvana",
                Artists = artistsOK,
                GenderId = 1,
                StartDate = new DateTime(1987, 08, 01)
            };

            bandWithConcerts = new Band()
            {
                MusicalProtagonistId = 1,
                Name = "Nirvana",
                Artists = artistsOK,
                GenderId = 1,
                StartDate = new DateTime(1987, 08, 01),
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

            bandEmptyName = new Band()
            {
                MusicalProtagonistId = 1,
                Name = "",
                Artists = artistsOK,
                Gender = genderOK,
                StartDate = new DateTime(1987, 08, 01)
            };

            bandNullName = new Band()
            {
                MusicalProtagonistId = 1,
                Name = null,
                Artists = artistsOK,
                Gender = genderOK,
                StartDate = new DateTime(1987, 08, 01)
            };

            bandEmptyArtist = new Band()
            {
                MusicalProtagonistId = 1,
                Name = "Nirvana",
                Artists = new List<ArtistBand>(),
                Gender = genderOK,
                StartDate = new DateTime(1987, 08, 01)
            };

            bandNullArtist = new Band()
            {
                MusicalProtagonistId = 1,
                Name = "Nirvana",
                Artists = null,
                Gender = genderOK,
                StartDate = new DateTime(1987, 08, 01)
            };

            bandNonExistArtist = new Band()
            {
                MusicalProtagonistId = 1,
                Name = "Nirvana",
                Artists = new List<ArtistBand>()
                {
                    new ArtistBand()
                    {
                        ArtistId = 2,
                        Artist = new Artist(){
                            ArtistId = 2,
                            Name = "Robert Smith",
                        },
                        RoleArtist = roleArtistOK,
                        RoleArtistId = roleArtistOK.RoleArtistId
                    }
                },
                GenderId = 10,
                StartDate = new DateTime(1987, 08, 01)
            };

            bandNullGender = new Band()
            {
                MusicalProtagonistId = 1,
                Name = "Nirvana",
                Artists = artistsOK,
                Gender = null,
                StartDate = new DateTime(1987, 08, 01)
            };

            bandNonExistGender = new Band()
            {
                MusicalProtagonistId = 1,
                Name = "Nirvana",
                Artists = artistsOK,
                GenderId = 10,
                StartDate = new DateTime(1987, 08, 01)
            };

            bandNonValidStartDate = new Band()
            {
                MusicalProtagonistId = 1,
                Name = "Nirvana",
                Artists = artistsOK,
                Gender = genderOK,
                StartDate = DateTime.Now.AddDays(1)
            };

            bandNull = null;
            genderNull = null;
            artistNull = null;
            bandsOK = new List<Band>() { bandOK };
            bandIdZero = 0;
            bandIdInexistant = 2;
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void GetBandByIdInvalidIdTest()
        {
            managementService.GetBandById(bandIdZero);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void GetBandByIdNonExistTest()
        {
            managementMock.Setup(x => x.GetBandById(bandIdInexistant)).Returns(bandNull);
            managementService.GetBandById(bandIdInexistant);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetBandByIdOkTest()
        {
            managementMock.Setup(x => x.GetBandById(bandOK.MusicalProtagonistId)).Returns(bandOK);
            managementService.GetBandById(bandOK.MusicalProtagonistId);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetAllBandsTest()
        {
            managementMock.Setup(x => x.GetBands()).Returns(bandsOK);
            managementService.GetBands();
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertNullTest()
        {
            managementService.InsertBand(bandNull);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertEmptyNameTest()
        {
            managementService.InsertBand(bandEmptyName);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertNullNameTest()
        {
            managementService.InsertBand(bandNullName);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertEmptyArtistTest()
        {
            managementService.InsertBand(bandEmptyArtist);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertNullArtistTest()
        {
            managementService.InsertBand(bandNullArtist);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void InsertNonExistArtistTest()
        {
            serviceArtistMock.Setup(x => x.GetArtistById(It.IsAny<int>())).Returns(artistNull);
            serviceGenderMock.Setup(x => x.GetGenderById(It.IsAny<int>())).Returns(genderOK);

            managementService.InsertBand(bandNonExistArtist);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]

        public void InsertNullGenderTest()
        {
            serviceArtistMock.Setup(x => x.GetArtistById(It.IsAny<int>())).Returns(artistOK.Artist);
            serviceGenderMock.Setup(x => x.GetGenderById(It.IsAny<int>())).Returns(genderNull);

            managementService.InsertBand(bandNullGender);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]

        public void InsertNonExistGenderTest()
        {
            serviceArtistMock.Setup(x => x.GetArtistById(It.IsAny<int>())).Returns(artistOK.Artist);
            serviceGenderMock.Setup(x => x.GetGenderById(It.IsAny<int>())).Returns(genderNull);

            managementService.InsertBand(bandNonExistGender);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertNonValidStartDateTest()
        {
            serviceArtistMock.Setup(x => x.GetArtistById(It.IsAny<int>())).Returns(artistOK.Artist);
            serviceGenderMock.Setup(x => x.GetGenderById(It.IsAny<int>())).Returns(genderOK);

            managementService.InsertBand(bandNonValidStartDate);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertBandNameExists()
        {
            serviceArtistMock.Setup(x => x.GetArtistById(It.IsAny<int>())).Returns(artistOK.Artist);
            serviceGenderMock.Setup(x => x.GetGenderById(It.IsAny<int>())).Returns(genderOK);
            managementMock.Setup(x => x.GetBands(It.IsAny<Func<Band, bool>>())).Returns(bandsOK);
            managementService.InsertBand(bandOK);
        }

        [TestMethod]
        public void InsertBandOkTest()
        {
            managementMock.Setup(x => x.GetBands(It.IsAny<Func<Band, bool>>())).Returns(bandsEmpty);
            serviceArtistMock.Setup(x => x.GetArtistById(It.IsAny<int>())).Returns(artistOK.Artist);
            serviceGenderMock.Setup(x => x.GetGenderById(It.IsAny<int>())).Returns(genderOK);
            managementMock.Setup(x => x.InsertBand(bandOK));
            managementMock.Setup(x => x.Save());
            managementService.InsertBand(bandOK);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateNullTest()
        {
            managementService.UpdateBand(bandNull);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateEmptyNameTest()
        {
            managementService.UpdateBand(bandEmptyName);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateNullNameTest()
        {
            managementService.UpdateBand(bandNullName);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void UpdateBandNonExistTest()
        {
            serviceArtistMock.Setup(x => x.GetArtistById(It.IsAny<int>())).Returns(artistOK.Artist);
            serviceGenderMock.Setup(x => x.GetGenderById(It.IsAny<int>())).Returns(genderOK);
            managementMock.Setup(x => x.GetBandById(bandOK.MusicalProtagonistId)).Returns(bandNull);
            managementService.UpdateBand(bandOK);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void UpdateBandOk()
        {
            serviceArtistMock.Setup(x => x.GetArtistById(It.IsAny<int>())).Returns(artistOK.Artist);
            serviceGenderMock.Setup(x => x.GetGenderById(It.IsAny<int>())).Returns(genderOK);
            managementMock.Setup(x => x.GetBandById(It.IsAny<int>())).Returns(bandOK);
            managementMock.Setup(x => x.UpdateBand(bandOK));
            managementMock.Setup(x => x.Save());
            managementService.UpdateBand(bandOK);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void DeleteBandInvalidIdTest()
        {
            managementService.DeleteBand(bandIdZero);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void DeleteBandNonExistTest()
        {
            managementMock.Setup(x => x.GetBandById(bandIdInexistant)).Returns(bandNull);
            managementService.DeleteBand(bandIdInexistant);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void DeleteBandWithConcertsTest()
        {
            managementMock.Setup(x => x.GetBandById(bandIdInexistant)).Returns(bandWithConcerts);
            managementService.DeleteBand(bandIdInexistant);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void DeleteBandOkTest()
        {
            managementMock.Setup(x => x.GetBandById(bandOK.MusicalProtagonistId)).Returns(bandOK);
            managementMock.Setup(x => x.DeleteBand(bandOK));
            managementMock.Setup(x => x.Save());
            managementService.DeleteBand(bandOK.MusicalProtagonistId);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetFilterBandNullNameTest()
        {
            managementMock.Setup(x => x.GetBands()).Returns(bandsOK);
            managementService.GetBands(bandNullName);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetFilterBandEmptyNameTest()
        {
            managementMock.Setup(x => x.GetBands()).Returns(bandsOK);
            managementService.GetBands(bandEmptyName);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetFilterBandTest()
        {
            managementMock.Setup(x => x.GetBands(It.IsAny<Func<Band, bool>>())).Returns(bandsOK);
            managementService.GetBands(bandOK);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void BandsByArtistNull()
        {
            IEnumerable<Band> bands = managementService.GetBandsByArtist(null);
            Assert.AreEqual(0, bands.Count());
        }

        [TestMethod]
        public void BandsByArtistOk()
        {
            managementMock.Setup(x => x.GetBands(It.IsAny<Func<Band, bool>>())).Returns(bandsOK);
            IEnumerable<Band> bands = managementService.GetBandsByArtist(artistOK.Artist);
            managementMock.VerifyAll();
        }
    }
}
