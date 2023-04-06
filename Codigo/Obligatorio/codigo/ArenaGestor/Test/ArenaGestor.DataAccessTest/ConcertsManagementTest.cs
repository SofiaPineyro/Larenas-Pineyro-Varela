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
    public class ConcertsManagementTest : ManagementTest
    {
        private DbContext context;
        private ConcertsManagement management;

        private Concert concertOK;
        private Concert concertNotExists;
        private List<Concert> concertsOK;
        private List<Concert> concertsAdded;
        private Country coutryOk;

        private DateRange dateRangeEmpty;
        private DateRange dateRangeOK;

        [TestInitialize]
        public void InitTest()
        {
            coutryOk = new Country()
            {
                CountryId = 1,
                Name = "Uruguay"
            };

            concertOK = new Concert()
            {
                ConcertId = 1,
                TourName = "Olé Tour",
                Date = DateTime.Now.AddDays(10),
                Protagonists = new List<ConcertProtagonist>() {
                    new ConcertProtagonist()
                    {
                        Protagonist = new Band()
                        {
                            MusicalProtagonistId = 1,
                            Name = "The Rolling Stones",
                            StartDate = DateTime.Now,
                            Gender = new Gender()
                            {
                                GenderId = 1,
                                Name = "Rock"
                            },
                            Artists = new List<ArtistBand>()
                        }
                    }
                },
                Location = new Location()
                {
                    Country = coutryOk,
                    LocationId = 1,
                    Number = 1234,
                    Place = "Estadio Centenario",
                    Street = "Av. Ricaldoni"
                }
            };

            concertNotExists = new Concert()
            {
                ConcertId = 2,
                TourName = "Vengas Tour",
                Protagonists = new List<ConcertProtagonist>() {
                    new ConcertProtagonist()
                    {
                        Protagonist = new Band()
                        {
                            MusicalProtagonistId = 2,
                            Name = "Julieta Venegas",
                            StartDate = DateTime.Now,
                            Gender = new Gender()
                            {
                                GenderId = 2,
                                Name = "Pop"
                            }
                        }
                    }
                },
                LocationId = 1
            };

            concertsOK = new List<Concert>
            {
                concertOK
            };

            concertsAdded = new List<Concert>
            {
                concertOK,
                concertNotExists
            };

            dateRangeEmpty = new DateRange()
            {
                StartDate = DateTime.Now,
                EndDate = concertOK.Date.AddDays(-1)
            };

            dateRangeOK = new DateRange()
            {
                StartDate = DateTime.Now,
                EndDate = concertOK.Date.AddDays(1)
            };

            CreateDataBase();
        }

        private void CreateDataBase()
        {
            context = CreateDbContext();
            context.Set<Concert>().Add(concertOK);
            context.Set<Location>().Add(concertOK.Location);
            context.SaveChanges();

            management = new ConcertsManagement(context);
        }

        [TestMethod]
        public void GetTest()
        {
            var result = management.GetConcerts().ToList();
            Assert.IsTrue(concertsOK.SequenceEqual(result));
        }

        [TestMethod]
        public void GetWithFilterThatExistsTest()
        {
            Func<Concert, bool> filter = new Func<Concert, bool>(x => x.TourName.Trim().ToUpper() == concertOK.TourName.Trim().ToUpper());
            var result = management.GetConcerts(filter).ToList();
            Assert.IsTrue(concertsOK.SequenceEqual(result));
        }

        [TestMethod]
        public void GetWithFilterThatNotExistsTest()
        {
            Func<Concert, bool> filter = new Func<Concert, bool>(x => x.TourName.Trim().ToUpper() == concertNotExists.TourName.Trim().ToUpper());
            int size = management.GetConcerts(filter).ToList().Count;
            Assert.AreEqual(0, size);
        }

        [TestMethod]
        public void GetWithFilterByIdTest()
        {
            Func<Concert, bool> filter = new Func<Concert, bool>(x => x.ConcertId == concertOK.ConcertId);
            var result = management.GetConcerts(filter).ToList();
            Assert.IsTrue(concertsOK.SequenceEqual(result));
        }

        [TestMethod]
        public void GetById()
        {
            Concert concert = management.GetConcertById(concertOK.ConcertId);
            Assert.AreEqual(concertOK, concert);
        }

        [TestMethod]
        public void GetUpcomingConcertsTest()
        {
            var result = management.GetConcerts().ToList();
            Assert.IsTrue(concertsOK.SequenceEqual(result));
        }

        [TestMethod]
        public void GetDateRangeConcertsThatNotExistsTest()
        {
            Func<Concert, bool> filter = new Func<Concert, bool>(x => x.Date >= dateRangeEmpty.StartDate && x.Date <= dateRangeEmpty.EndDate);
            int size = management.GetConcerts(filter).ToList().Count;
            Assert.AreEqual(0, size);
        }

        [TestMethod]
        public void GetDateRangeConcertsTest()
        {
            Func<Concert, bool> filter = new Func<Concert, bool>(x => x.Date >= dateRangeOK.StartDate && x.Date <= dateRangeOK.EndDate);
            var result = management.GetConcerts(filter).ToList();
            Assert.IsTrue(concertsOK.SequenceEqual(result));
        }

        [TestMethod]
        public void GetDateRangeConcertsByMusicalProtagonist() 
        {
            var result = management.GetDateRangeConcertsByMusicalProtagonist(dateRangeOK, 1).ToList();
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            management.InsertConcert(concertNotExists);
            management.Save();
            var result = management.GetConcerts().ToList();
            Assert.IsTrue(concertsAdded.SequenceEqual(result));
        }

        [TestMethod]
        public void UpdateTest()
        {
            concertOK.TourName = "Olé Tour 2";
            management.UpdateConcert(concertOK);
            management.Save();
            string newName = management.GetConcerts().Where(g => g.ConcertId == concertOK.ConcertId).First().TourName;
            Assert.AreEqual("Olé Tour 2", newName);
        }

        [TestMethod]
        public void DeleteTest()
        {
            management.DeleteConcert(concertOK);
            management.Save();
            int size = management.GetConcerts().ToList().Count;
            Assert.AreEqual(0, size);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.context.Database.EnsureDeleted();
        }
    }
}
