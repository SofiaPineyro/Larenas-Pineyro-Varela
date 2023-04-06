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
    public class ConcertsServiceTest
    {
        private Mock<IConcertsManagement> managementMock;
        private Mock<IMusicalProtagonistService> musicalProtagonistMock;
        private Mock<ICountrysService> countrysMock;
        private Mock<ISecurityService> securityServiceMock;

        private ConcertsService concertService;

        private Concert concertOk;
        private Concert concertWithTickets;
        private Concert concertNull;
        private Concert concertEmptyName;
        private Concert concertNullName;
        private Concert concertInvalidDate;
        private Concert concertInvalidPrice;
        private Concert concertInvalidTickets;
        private Concert concertNoMusicalProtagonist;
        private Concert concertMusicalProtagonistZero;
        private IEnumerable<Concert> emptyList;

        private MusicalProtagonist musicalProtagonistNull;
        private MusicalProtagonist musicalProtagonistOk;
        private Country coutryOk;

        private IEnumerable<Concert> concertsOK;
        private int concertIdZero;
        private int concertIdInexistant;

        private ConcertFilter concertFilterOk;
        private ConcertFilter concertFilterEmptyName;
        private ConcertFilter concertFilterNullName;

        private ConcertFilter concertFilterRangeOk;
        private ConcertFilter concertFilterRangeBadOk;
        private ConcertFilter concertFilterRangeNullOk;
        private ConcertFilter concertFilteUpcomingrOk;

        private User userArtistOk;

        [TestInitialize]
        public void InitTest()
        {
            managementMock = new Mock<IConcertsManagement>(MockBehavior.Strict);
            musicalProtagonistMock = new Mock<IMusicalProtagonistService>(MockBehavior.Strict);
            countrysMock = new Mock<ICountrysService>(MockBehavior.Strict);
            securityServiceMock = new Mock<ISecurityService>(MockBehavior.Strict);

            concertService = new ConcertsService(managementMock.Object, musicalProtagonistMock.Object, countrysMock.Object, securityServiceMock.Object);

            emptyList = new List<Concert>() { };

            coutryOk = new Country()
            {
                CountryId = 1,
                Name = "Uruguay"
            };

            concertOk = new Concert()
            {
                ConcertId = 1,
                TourName = "Olé Tour",
                Date = DateTime.Now.AddDays(10),
                Price = 100,
                TicketCount = 500,
                Protagonists = new List<ConcertProtagonist>()
                {
                    new ConcertProtagonist()
                    {
                        MusicalProtagonistId = 1
                    }
                },
                Location = new Location()
                {
                    Country = coutryOk,
                    CountryId = coutryOk.CountryId,
                    LocationId = 1,
                    Number = 1234,
                    Place = "Estadio Centenario",
                    Street = "Av. Ricaldoni"
                }
            };

            concertFilterOk = new ConcertFilter()
            {
                TourName = "Olé Tour"
            };

            concertFilterEmptyName = new ConcertFilter()
            {
                TourName = ""
            };

            concertFilterNullName = new ConcertFilter()
            {
                TourName = null
            };

            concertFilterRangeOk = new ConcertFilter()
            {
                DateRange = new DateRange()
                {
                    StartDate = DateTime.Now,
                    EndDate = concertOk.Date.AddDays(1)
                }
            };

            concertFilterRangeBadOk = new ConcertFilter()
            {
                DateRange = new DateRange()
                {
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(-1)
                }
            };

            concertFilterRangeNullOk = new ConcertFilter()
            {
                DateRange = null
            };

            concertFilteUpcomingrOk = new ConcertFilter()
            {
                Upcoming = true
            };

            concertWithTickets = new Concert()
            {
                ConcertId = 1,
                TourName = "Olé Tour",
                Date = DateTime.Now.AddDays(10),
                Price = 100,
                TicketCount = 500,
                Tickets = new List<Ticket>()
                {
                    new Ticket()
                    {
                        TicketId = Guid.NewGuid()
                    }
                },
                Location = new Location()
                {
                    Country = coutryOk,
                    CountryId = coutryOk.CountryId,
                    LocationId = 1,
                    Number = 1234,
                    Place = "Estadio Centenario",
                    Street = "Av. Ricaldoni"
                }
            };

            concertEmptyName = new Concert()
            {
                ConcertId = 1,
                TourName = ""
            };
            concertNullName = new Concert()
            {
                ConcertId = 1,
                TourName = null
            };
            concertInvalidDate = new Concert()
            {
                ConcertId = 1,
                TourName = "Olé Tour",
                Date = new DateTime(1987, 08, 01),
                Price = 100,
                TicketCount = 500
            };
            concertInvalidPrice = new Concert()
            {
                ConcertId = 1,
                TourName = "Olé Tour",
                Date = DateTime.Now.AddDays(10),
                Price = 0,
                TicketCount = 500
            };
            concertInvalidTickets = new Concert()
            {
                ConcertId = 1,
                TourName = "Olé Tour",
                Date = DateTime.Now.AddDays(10),
                Price = 100,
                TicketCount = 0
            };
            concertNoMusicalProtagonist = new Concert()
            {
                ConcertId = 1,
                TourName = "Olé Tour",
                Date = DateTime.Now.AddDays(10),
                Price = 100,
                TicketCount = 500,
                Protagonists = new List<ConcertProtagonist>()
            };
            concertMusicalProtagonistZero = new Concert()
            {
                ConcertId = 1,
                TourName = "Olé Tour",
                Date = DateTime.Now.AddDays(10),
                Price = 100,
                TicketCount = 500,
                Protagonists = new List<ConcertProtagonist>()
                {
                    new ConcertProtagonist()
                    {
                        MusicalProtagonistId = 0
                    }
                },
                Location = new Location()
                {
                    Country = coutryOk,
                    CountryId = coutryOk.CountryId,
                    LocationId = 1,
                    Number = 1234,
                    Place = "Estadio Centenario",
                    Street = "Av. Ricaldoni"
                }
            };

            concertNull = null;
            concertsOK = new List<Concert>() { concertOk };
            concertIdZero = 0;
            concertIdInexistant = 2;
            musicalProtagonistNull = null;

            Artist artistOK = new Artist()
            {
                ArtistId = 1,
                Name = "Kurt Cobain"
            };

            Gender genderOK = new Gender()
            {
                GenderId = 1,
                Name = "Rock"
            };

            musicalProtagonistOk = new Soloist()
            {
                MusicalProtagonistId = 1,
                Name = "Kurt Cobain",
                Gender = genderOK,
                Artist = artistOK,
                StartDate = new DateTime(1987, 08, 01)
            };

            userArtistOk = new User()
            {
                UserId = 1,
                Name = "Test",
                Surname = "User",
                Email = "test@user.com",
                Password = "testuser123",
                Roles = new List<UserRole>() {
                    new UserRole()
                    {
                        RoleId = RoleCode.Artista
                    }
                }
            };
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void GetConcertByIdInvalidIdTest()
        {
            concertService.GetConcertById(concertIdZero);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void GetConcertByIdNonExistTest()
        {
            managementMock.Setup(x => x.GetConcertById(concertIdInexistant)).Returns(concertNull);
            concertService.GetConcertById(concertIdInexistant);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetConcertByIdOkTest()
        {
            managementMock.Setup(x => x.GetConcertById(concertOk.ConcertId)).Returns(concertOk);
            concertService.GetConcertById(concertOk.ConcertId);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetAllConcertsTest()
        {
            managementMock.Setup(x => x.GetConcerts(It.IsAny<Func<Concert, bool>>())).Returns(concertsOK);
            concertService.GetConcerts();
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetFilterConcertTest()
        {
            managementMock.Setup(x => x.GetConcerts(It.IsAny<Func<Concert, bool>>())).Returns(concertsOK);
            concertService.GetConcerts(concertFilterOk);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetFilterConcertEmptyNameTest()
        {
            managementMock.Setup(x => x.GetConcerts(It.IsAny<Func<Concert, bool>>())).Returns(concertsOK);
            concertService.GetConcerts(concertFilterEmptyName);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetFilterConcertNullNameTest()
        {
            managementMock.Setup(x => x.GetConcerts(It.IsAny<Func<Concert, bool>>())).Returns(concertsOK);
            concertService.GetConcerts(concertFilterNullName);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetUpcomingConcertsTest()
        {
            managementMock.Setup(x => x.GetConcerts(It.IsAny<Func<Concert, bool>>())).Returns(concertsOK);
            concertService.GetConcerts(concertFilteUpcomingrOk);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetDateRangeConcertsNullRangeTest()
        {
            managementMock.Setup(x => x.GetConcerts(It.IsAny<Func<Concert, bool>>())).Returns(concertsOK);
            concertService.GetConcerts(concertFilterRangeNullOk);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void GetDateRangeConcertsBadRangeTest()
        {
            concertService.GetConcerts(concertFilterRangeBadOk);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetDateRangeConcertsOkTest()
        {
            managementMock.Setup(x => x.GetConcerts(It.IsAny<Func<Concert, bool>>())).Returns(concertsOK);
            concertService.GetConcerts(concertFilterRangeOk);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertNullTest()
        {
            concertService.InsertConcert(concertNull);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertEmptyNameTest()
        {
            concertService.InsertConcert(concertEmptyName);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertNullNameTest()
        {
            concertService.InsertConcert(concertNullName);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertInvalidDateTest()
        {
            concertService.InsertConcert(concertInvalidDate);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertInvalidTicketsTest()
        {
            concertService.InsertConcert(concertInvalidTickets);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertInvalidPriceTest()
        {
            concertService.InsertConcert(concertInvalidPrice);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertWithNoProtagonists()
        {
            concertService.InsertConcert(concertNoMusicalProtagonist);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void InvalidMusicalProtagonistTest()
        {
            musicalProtagonistMock.Setup(x => x.GetMusicalProtagonistById(It.IsAny<int>())).Returns(musicalProtagonistNull);
            countrysMock.Setup(x => x.GetCountryById(It.IsAny<int>())).Returns(coutryOk);

            concertService.InsertConcert(concertMusicalProtagonistZero);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertLimitDailyTest()
        {
            var emptyList = new List<Concert>() { };

            musicalProtagonistMock.Setup(x => x.GetMusicalProtagonistById(It.IsAny<int>())).Returns(musicalProtagonistOk);
            countrysMock.Setup(x => x.GetCountryById(It.IsAny<int>())).Returns(coutryOk);

            managementMock.Setup(x => x.GetDateRangeConcertsByMusicalProtagonist(It.IsAny<DateRange>(), It.IsAny<int>())).Returns(concertsOK);

            concertService.InsertConcert(concertOk);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void InsertLimitDailyNullTest()
        {
            var emptyList = new List<Concert>() { };

            List<Concert> concertsNull = null;

            musicalProtagonistMock.Setup(x => x.GetMusicalProtagonistById(It.IsAny<int>())).Returns(musicalProtagonistOk);
            countrysMock.Setup(x => x.GetCountryById(It.IsAny<int>())).Returns(coutryOk);

            managementMock.Setup(x => x.GetDateRangeConcertsByMusicalProtagonist(It.IsAny<DateRange>(), It.IsAny<int>())).Returns(concertsNull);

            managementMock.Setup(x => x.InsertConcert(concertOk));
            managementMock.Setup(x => x.Save());
            concertService.InsertConcert(concertOk);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void InsertOkTest()
        {
            musicalProtagonistMock.Setup(x => x.GetMusicalProtagonistById(It.IsAny<int>())).Returns(musicalProtagonistOk);
            countrysMock.Setup(x => x.GetCountryById(It.IsAny<int>())).Returns(coutryOk);

            managementMock.Setup(x => x.GetDateRangeConcertsByMusicalProtagonist(It.IsAny<DateRange>(), It.IsAny<int>())).Returns(emptyList);

            managementMock.Setup(x => x.InsertConcert(concertOk));
            managementMock.Setup(x => x.Save());
            concertService.InsertConcert(concertOk);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void InsertMultipleOk()
        {
            musicalProtagonistMock.Setup(x => x.GetMusicalProtagonistById(It.IsAny<int>())).Returns(musicalProtagonistOk);
            countrysMock.Setup(x => x.GetCountryById(It.IsAny<int>())).Returns(coutryOk);

            managementMock.Setup(x => x.GetDateRangeConcertsByMusicalProtagonist(It.IsAny<DateRange>(), It.IsAny<int>())).Returns(emptyList);

            managementMock.Setup(x => x.InsertConcert(concertOk));
            managementMock.Setup(x => x.Save());
            List<Concert> concerts = new List<Concert>() { concertOk };
            ConcertsInsertResult result = concertService.InsertConcerts(concerts);
            managementMock.VerifyAll();
            Assert.AreEqual(result.InsertedRecords, 1);
        }

        [TestMethod]
        public void InsertMultipleError()
        {
            musicalProtagonistMock.Setup(x => x.GetMusicalProtagonistById(It.IsAny<int>())).Returns(musicalProtagonistNull);
            countrysMock.Setup(x => x.GetCountryById(It.IsAny<int>())).Returns(coutryOk);

            List<Concert> concerts = new List<Concert>() { concertOk };
            ConcertsInsertResult result = concertService.InsertConcerts(concerts);
            managementMock.VerifyAll();
            Assert.AreEqual(result.NotInsertedRecords, 1);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateNullTest()
        {
            concertService.UpdateConcert(concertNull);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateEmptyNameTest()
        {
            concertService.UpdateConcert(concertEmptyName);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateNullNameTest()
        {
            concertService.UpdateConcert(concertNullName);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateInvalidDateTest()
        {
            concertService.UpdateConcert(concertInvalidDate);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateInvalidTicketsTest()
        {
            concertService.UpdateConcert(concertInvalidTickets);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateInvalidPriceTest()
        {
            concertService.UpdateConcert(concertInvalidPrice);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void InvalidMusicalProtagonistUpdateTest()
        {
            musicalProtagonistMock.Setup(x => x.GetMusicalProtagonistById(It.IsAny<int>())).Returns(musicalProtagonistNull);
            countrysMock.Setup(x => x.GetCountryById(It.IsAny<int>())).Returns(coutryOk);

            concertService.UpdateConcert(concertMusicalProtagonistZero);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void UpdateConcertNonExistTest()
        {
            musicalProtagonistMock.Setup(x => x.GetMusicalProtagonistById(It.IsAny<int>())).Returns(musicalProtagonistOk);
            countrysMock.Setup(x => x.GetCountryById(It.IsAny<int>())).Returns(coutryOk);

            managementMock.Setup(x => x.GetConcertById(It.IsAny<int>())).Returns(concertNull);
            managementMock.Setup(x => x.GetDateRangeConcertsByMusicalProtagonist(It.IsAny<DateRange>(), It.IsAny<int>())).Returns(emptyList);

            concertService.UpdateConcert(concertOk);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateConcertLimitDailyTest()
        {
            musicalProtagonistMock.Setup(x => x.GetMusicalProtagonistById(It.IsAny<int>())).Returns(musicalProtagonistOk);
            countrysMock.Setup(x => x.GetCountryById(It.IsAny<int>())).Returns(coutryOk);

            managementMock.Setup(x => x.GetConcertById(It.IsAny<int>())).Returns(concertOk);
            managementMock.Setup(x => x.GetDateRangeConcertsByMusicalProtagonist(It.IsAny<DateRange>(), It.IsAny<int>())).Returns(concertsOK);

            concertService.UpdateConcert(concertOk);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void UpdateOkTest()
        {
            musicalProtagonistMock.Setup(x => x.GetMusicalProtagonistById(It.IsAny<int>())).Returns(musicalProtagonistOk);
            countrysMock.Setup(x => x.GetCountryById(It.IsAny<int>())).Returns(coutryOk);

            managementMock.Setup(x => x.GetConcertById(It.IsAny<int>())).Returns(concertOk);
            managementMock.Setup(x => x.GetDateRangeConcertsByMusicalProtagonist(It.IsAny<DateRange>(), It.IsAny<int>())).Returns(emptyList);
            managementMock.Setup(x => x.UpdateConcert(concertOk));
            managementMock.Setup(x => x.Save());
            concertService.UpdateConcert(concertOk);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void DeleteConcertInvalidIdTest()
        {
            concertService.DeleteConcert(concertIdZero);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void DeleteConcertNonExistTest()
        {
            managementMock.Setup(x => x.GetConcertById(It.IsAny<int>())).Returns(concertNull);
            concertService.DeleteConcert(concertIdInexistant);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void DeleteConcertWithTicketsTest()
        {
            managementMock.Setup(x => x.GetConcertById(It.IsAny<int>())).Returns(concertWithTickets);
            concertService.DeleteConcert(concertIdInexistant);
        }

        [TestMethod]
        public void DeleteConcertOkTest()
        {
            managementMock.Setup(x => x.GetConcertById(It.IsAny<int>())).Returns(concertOk);
            managementMock.Setup(x => x.DeleteConcert(concertOk));
            managementMock.Setup(x => x.Save());
            concertService.DeleteConcert(concertOk.ConcertId);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetAllConcertsByArtistTest()
        {
            managementMock.Setup(x => x.GetConcerts(It.IsAny<Func<Concert, bool>>())).Returns(concertsOK);
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userArtistOk);
            securityServiceMock.Setup(x => x.UserHaveRole(It.IsAny<RoleCode>(), It.IsAny<string>())).Returns(true);

            concertService.GetConcerts(It.IsAny<string>());
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetFilterConcertByArtistTest()
        {
            managementMock.Setup(x => x.GetConcerts(It.IsAny<Func<Concert, bool>>())).Returns(concertsOK);
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userArtistOk);
            securityServiceMock.Setup(x => x.UserHaveRole(It.IsAny<RoleCode>(), It.IsAny<string>())).Returns(true);

            concertService.GetConcerts(It.IsAny<string>(), concertFilterOk);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetFilterConcertEmptyNameByArtistTest()
        {
            managementMock.Setup(x => x.GetConcerts(It.IsAny<Func<Concert, bool>>())).Returns(concertsOK);
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userArtistOk);
            securityServiceMock.Setup(x => x.UserHaveRole(It.IsAny<RoleCode>(), It.IsAny<string>())).Returns(true);

            concertService.GetConcerts(It.IsAny<string>(), concertFilterEmptyName);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetFilterConcertNullNameByArtistTest()
        {
            managementMock.Setup(x => x.GetConcerts(It.IsAny<Func<Concert, bool>>())).Returns(concertsOK);
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userArtistOk);
            securityServiceMock.Setup(x => x.UserHaveRole(It.IsAny<RoleCode>(), It.IsAny<string>())).Returns(true);

            concertService.GetConcerts(It.IsAny<string>(), concertFilterNullName);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetUpcomingConcertsByArtistTest()
        {
            managementMock.Setup(x => x.GetConcerts(It.IsAny<Func<Concert, bool>>())).Returns(concertsOK);
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userArtistOk);
            securityServiceMock.Setup(x => x.UserHaveRole(It.IsAny<RoleCode>(), It.IsAny<string>())).Returns(true);

            concertService.GetConcerts(It.IsAny<string>(), concertFilteUpcomingrOk);
            managementMock.VerifyAll();
        }

    }
}
