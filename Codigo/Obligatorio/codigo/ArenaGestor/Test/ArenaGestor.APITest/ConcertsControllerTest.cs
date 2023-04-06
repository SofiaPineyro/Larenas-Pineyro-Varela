using ArenaGestor.API.Controllers;
using ArenaGestor.APIContracts.Concert;
using ArenaGestor.BusinessInterface;
using ArenaGestor.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace ArenaGestor.APITest
{
    [TestClass]
    public class ConcertsControllerTest
    {
        private Mock<IConcertsService> mock;
        private Mock<IMapper> mockMapper;

        private ConcertsController api;

        private Concert concertOk;
        private ConcertFilter concertFilterOk;
        private ConcertFilter concertFilterRangeOk;
        private ConcertFilter concertFilteUpcomingrOk;

        private IEnumerable<Concert> concertsOk;

        private ConcertGetConcertsDto getConcertDto;
        private ConcertGetConcertsDto getConcertRangeDto;
        private ConcertGetConcertsDto getConcertUpcomingDto;

        private ConcertUpdateConcertDto updateConcertDto;
        private ConcertInsertConcertDto insertConcertDto;
        private ConcertResultConcertDto resultConcertDto;
        private IEnumerable<ConcertResultConcertDto> resultConcertsDto;
        private ConcertResultConcertArtistDto resultConcertWithUserDto;
        private IEnumerable<ConcertResultConcertArtistDto> resultConcertsWithUserDto;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IConcertsService>(MockBehavior.Strict);
            mockMapper = new Mock<IMapper>(MockBehavior.Strict);

            api = new ConcertsController(mock.Object, mockMapper.Object);

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
                        Protagonist = new Band()
                        {
                            MusicalProtagonistId = 1,
                            Name = "The Rolling Stones"
                        }
                    }
                },
                Location = new Location()
                {
                    Country = new Country()
                    {
                        CountryId = 1,
                        Name = "Uruguay"
                    },
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

            getConcertDto = new ConcertGetConcertsDto()
            {
                TourName = "Olé Tour"
            };

            concertFilterRangeOk = new ConcertFilter()
            {
                DateRange = new DateRange()
                {
                    StartDate = DateTime.Now,
                    EndDate = concertOk.Date.AddDays(1)
                }
            };

            getConcertRangeDto = new ConcertGetConcertsDto()
            {
                DateRange = new ConcertGetDateRangeConcertsDto()
                {
                    StartDate = DateTime.Now,
                    EndDate = concertOk.Date.AddDays(1)
                }
            };

            concertFilteUpcomingrOk = new ConcertFilter()
            {
                Upcoming = true
            };

            getConcertUpcomingDto = new ConcertGetConcertsDto()
            {
                Upcoming = true
            };

            updateConcertDto = new ConcertUpdateConcertDto()
            {
                ConcertId = 1,
                TourName = "Olé Tour",
                Date = DateTime.Now.AddDays(10),
                Price = 100,
                TicketCount = 500,
                Protagonists = new List<ConcertUpdateProtagonistDto>() { },
                Location = new ConcertUpdateLocationDto()
                {
                    LocationId = 1,
                    CountryId = 1,
                    Number = 1234,
                    Place = "Estadio Centenario",
                    Street = "Av. Ricaldoni"
                }
            };

            insertConcertDto = new ConcertInsertConcertDto()
            {
                TourName = "Olé Tour",
                Date = DateTime.Now.AddDays(10),
                Price = 100,
                TicketCount = 500,
                Protagonists = new List<ConcertInsertProtagonistDto>() { },
                Location = new ConcertInsertLocationDto()
                {
                    CountryId = 1,
                    Number = 1234,
                    Place = "Estadio Centenario",
                    Street = "Av. Ricaldoni"
                }
            };

            resultConcertDto = new ConcertResultConcertDto()
            {
                ConcertId = 1,
                TourName = "Olé Tour",
                Date = DateTime.Now.AddDays(10),
                Price = 100,
                TicketCount = 500,
                Protagonists = new List<ConcertGetMusicalProtagonistDto>()
                {
                    new ConcertGetMusicalProtagonistDto()
                    {
                        MusicalProtagonistId = 1,
                        Name = "The Rolling Stones"
                    }
                },
                Location = new ConcertResultLocationDto()
                {
                    LocationId = 1,
                    Country = new ConcertResultCountryDto()
                    {
                        CountryId = 1,
                        Name = "Uruguay"
                    },
                    Number = 1234,
                    Place = "Estadio Centenario",
                    Street = "Av. Ricaldoni"
                }
            };

            resultConcertsDto = new List<ConcertResultConcertDto>()
            {
                resultConcertDto
            };

            resultConcertWithUserDto = new ConcertResultConcertArtistDto()
            {
                ConcertId = 1,
                TourName = "Olé Tour",
                Date = DateTime.Now.AddDays(10),
                Price = 100,
                TicketCount = 500,
                Protagonists = new List<ConcertGetMusicalProtagonistDto>()
                {
                    new ConcertGetMusicalProtagonistDto()
                    {
                        MusicalProtagonistId = 1,
                        Name = "The Rolling Stones"
                    }
                },
                Location = new ConcertResultLocationDto()
                {
                    LocationId = 1,
                    Country = new ConcertResultCountryDto()
                    {
                        CountryId = 1,
                        Name = "Uruguay"
                    },
                    Number = 1234,
                    Place = "Estadio Centenario",
                    Street = "Av. Ricaldoni"
                }
            };

            resultConcertsWithUserDto = new List<ConcertResultConcertArtistDto>()
            {
                resultConcertWithUserDto
            };

            concertsOk = new List<Concert>() { concertOk };
        }

        [TestMethod]
        public void GetConcertByIdOkTest()
        {
            mock.Setup(x => x.GetConcertById(concertOk.ConcertId)).Returns(concertOk);
            mockMapper.Setup(x => x.Map<ConcertResultConcertDto>(concertOk)).Returns(resultConcertDto);

            var result = api.GetConcertById(concertOk.ConcertId);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void GetConcertsOkTest()
        {
            mock.Setup(x => x.GetConcerts(concertFilterOk)).Returns(concertsOk);
            mockMapper.Setup(x => x.Map<ConcertFilter>(getConcertDto)).Returns(concertFilterOk);
            mockMapper.Setup(x => x.Map<IEnumerable<ConcertResultConcertDto>>(concertsOk)).Returns(resultConcertsDto);

            var result = api.GetConcerts(getConcertDto);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void GetConcertsByArtistOkTest()
        {
            mock.Setup(x => x.GetConcerts(It.IsAny<string>(), concertFilterOk)).Returns(concertsOk);
            mockMapper.Setup(x => x.Map<ConcertFilter>(getConcertDto)).Returns(concertFilterOk);
            mockMapper.Setup(x => x.Map<IEnumerable<ConcertResultConcertArtistDto>>(concertsOk)).Returns(resultConcertsWithUserDto);
            api.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            api.ControllerContext.HttpContext.Request.Headers["token"] = BusinessHelpers.StringGenerator.GenerateRandomToken(64);

            var result = api.GetConcertsByArtist(getConcertDto);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void PostConcertOkTest()
        {
            mock.Setup(x => x.InsertConcert(concertOk)).Returns(concertOk);
            mockMapper.Setup(x => x.Map<Concert>(insertConcertDto)).Returns(concertOk);
            mockMapper.Setup(x => x.Map<ConcertResultConcertDto>(concertOk)).Returns(resultConcertDto);

            var result = api.PostConcert(insertConcertDto);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void PutConcertOkTest()
        {
            mock.Setup(x => x.UpdateConcert(concertOk)).Returns(concertOk);
            mockMapper.Setup(x => x.Map<Concert>(updateConcertDto)).Returns(concertOk);
            mockMapper.Setup(x => x.Map<ConcertResultConcertDto>(concertOk)).Returns(resultConcertDto);

            var result = api.PutConcert(updateConcertDto);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void DeleteConcertOkTest()
        {
            mock.Setup(x => x.DeleteConcert(It.IsAny<int>()));
            var result = api.DeleteConcert(It.IsAny<int>());
            var objectResult = result as OkResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void GetDateRangeConcertsOkTest()
        {
            mock.Setup(x => x.GetConcerts(concertFilterRangeOk)).Returns(concertsOk);
            mockMapper.Setup(x => x.Map<ConcertFilter>(getConcertRangeDto)).Returns(concertFilterRangeOk);
            mockMapper.Setup(x => x.Map<IEnumerable<ConcertResultConcertDto>>(concertsOk)).Returns(resultConcertsDto);

            var result = api.GetConcerts(getConcertRangeDto);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void GetDateRangeConcertsByArtistOkTest()
        {
            mock.Setup(x => x.GetConcerts(It.IsAny<string>(), concertFilterRangeOk)).Returns(concertsOk);
            mockMapper.Setup(x => x.Map<ConcertFilter>(getConcertRangeDto)).Returns(concertFilterRangeOk);
            mockMapper.Setup(x => x.Map<IEnumerable<ConcertResultConcertArtistDto>>(concertsOk)).Returns(resultConcertsWithUserDto);
            api.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            api.ControllerContext.HttpContext.Request.Headers["token"] = BusinessHelpers.StringGenerator.GenerateRandomToken(64);

            var result = api.GetConcertsByArtist(getConcertRangeDto);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void GetUpcomingConcertsOkTest()
        {
            mock.Setup(x => x.GetConcerts(concertFilteUpcomingrOk)).Returns(concertsOk);
            mockMapper.Setup(x => x.Map<ConcertFilter>(getConcertUpcomingDto)).Returns(concertFilteUpcomingrOk);
            mockMapper.Setup(x => x.Map<IEnumerable<ConcertResultConcertDto>>(concertsOk)).Returns(resultConcertsDto);

            var result = api.GetConcerts(getConcertUpcomingDto);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void GetUpcomingConcertsByArtistOkTest()
        {
            mock.Setup(x => x.GetConcerts(It.IsAny<string>(), concertFilteUpcomingrOk)).Returns(concertsOk);
            mockMapper.Setup(x => x.Map<ConcertFilter>(getConcertUpcomingDto)).Returns(concertFilteUpcomingrOk);
            mockMapper.Setup(x => x.Map<IEnumerable<ConcertResultConcertArtistDto>>(concertsOk)).Returns(resultConcertsWithUserDto);
            api.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            api.ControllerContext.HttpContext.Request.Headers["token"] = BusinessHelpers.StringGenerator.GenerateRandomToken(64);

            var result = api.GetConcertsByArtist(getConcertUpcomingDto);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }
    }
}
