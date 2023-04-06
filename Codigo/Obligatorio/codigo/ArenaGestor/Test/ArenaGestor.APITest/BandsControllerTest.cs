using ArenaGestor.API.Controllers;
using ArenaGestor.APIContracts.Band;
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
    public class BandsControllerTest
    {
        private Mock<IBandsService> mock;
        private Mock<IMapper> mockMapper;
        private BandsController api;

        private Band bandOK;
        private IEnumerable<Band> bandsOK;
        private Gender genderOK;
        private ArtistBand artistOK;

        private BandResultBandDto resultBandDto;
        private IEnumerable<BandResultBandDto> resultBandsDto;
        private BandResultArtistDto resultArtistDto;
        private BandResultGenderDto resultGenderDto;
        private BandResultConcertDto resultConcertDto;
        private BandGetBandsDto getBandsDto;
        private BandGetArtistsDto getArtistsDto;
        private BandInsertBandDto insertBandDto;
        private BandInsertArtistDto insertArtistDto;
        private BandUpdateBandDto updateBandDto;
        private BandUpdateArtistDto updateArtistDto;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IBandsService>(MockBehavior.Strict);
            mockMapper = new Mock<IMapper>(MockBehavior.Strict);

            api = new BandsController(mock.Object, mockMapper.Object);

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
                }
            };

            bandOK = new Band()
            {
                MusicalProtagonistId = 1,
                Name = "Nirvana",
                Artists = new List<ArtistBand>()
                {
                    artistOK,
                    new ArtistBand()
                    {
                        ArtistId = 2,
                        Artist = new Artist()
                        {
                            Name = "Dave Grohl"
                        }
                    },
                    new ArtistBand()
                    {
                        ArtistId = 3,
                        Artist = new Artist()
                        {
                            Name = "Krist Novoselic"
                        }
                    }
                },
                Gender = genderOK,
                StartDate = new DateTime(1987, 08, 01)
            };

            bandsOK = new List<Band>() { bandOK };

            resultArtistDto = new BandResultArtistDto()
            {
                ArtistId = 1,
                Name = "Kurt Cobain",
                RoleArtist = new BandResultRoleArtistDto()
                {
                    Name = RoleArtistCode.Cantante.ToString()
                }
            };

            resultGenderDto = new BandResultGenderDto()
            {
                GenderId = 1,
                Name = "Rock"
            };

            resultConcertDto = new BandResultConcertDto()
            {
                ConcertId = 1,
                TourName = "Olé Tour",
                Date = DateTime.Now.AddDays(10),
                Price = 100,
                TicketCount = 500
            };

            resultBandDto = new BandResultBandDto()
            {
                MusicalProtagonistId = 1,
                Name = "Nirvana",
                Artists = new List<BandResultArtistDto>()
                {
                    new BandResultArtistDto()
                    {
                        ArtistId = 1,
                        Name = "Kurt Cobain",
                        RoleArtist = new BandResultRoleArtistDto()
                        {
                            Name = RoleArtistCode.Cantante.ToString()
                        }
                    },
                    new BandResultArtistDto()
                    {
                        ArtistId = 2,
                        Name = "Dave Grohl",
                        RoleArtist = new BandResultRoleArtistDto()
                        {
                            Name = RoleArtistCode.Baterista.ToString()
                        }
                    },
                    new BandResultArtistDto()
                    {
                        ArtistId = 3,
                        Name = "Krist Novoselic",
                        RoleArtist = new BandResultRoleArtistDto()
                        {
                            Name = RoleArtistCode.Bajista.ToString()
                        }
                    }
                },
                Gender = resultGenderDto,
                StartDate = new DateTime(1987, 08, 01),
                Concerts = new List<BandResultConcertDto>()
                {
                    resultConcertDto
                }
            };

            getBandsDto = new BandGetBandsDto()
            {
                Name = "Nirvana"
            };

            resultBandsDto = new List<BandResultBandDto>()
            {
                resultBandDto
            };

            getArtistsDto = new BandGetArtistsDto()
            {
                Name = "Kurt Cobain"
            };

            insertArtistDto = new BandInsertArtistDto()
            {
                ArtistId = 1,
                RoleArtistId = (int)RoleArtistCode.Cantante
            };

            insertBandDto = new BandInsertBandDto()
            {
                Artists = new List<BandInsertArtistDto>()
                {
                    insertArtistDto,
                    new BandInsertArtistDto()
                    {
                        ArtistId = 2,
                        RoleArtistId = (int)RoleArtistCode.Cantante
                    },
                    new BandInsertArtistDto()
                    {
                        ArtistId = 3,
                        RoleArtistId = (int)RoleArtistCode.Cantante
                    }
                },
                GenderId = 1,
                Name = "Kurt Cobain",
                StartDate = new DateTime(1987, 08, 01)
            };

            updateArtistDto = new BandUpdateArtistDto()
            {
                ArtistId = 1,
                RoleArtistId = (int)RoleArtistCode.Cantante
            };

            updateBandDto = new BandUpdateBandDto()
            {
                Artists = new List<BandUpdateArtistDto>()
                {
                    updateArtistDto,
                    new BandUpdateArtistDto()
                    {
                        ArtistId = 2,
                        RoleArtistId = (int)RoleArtistCode.Cantante
                    },
                    new BandUpdateArtistDto()
                    {
                        ArtistId = 3,
                        RoleArtistId = (int)RoleArtistCode.Cantante
                    }
                },
                GenderId = 1,
                Name = "Kurt Cobain",
                StartDate = new DateTime(1987, 08, 01)
            };
        }

        [TestMethod]
        public void GetBandByIdOkTest()
        {
            mock.Setup(x => x.GetBandById(bandOK.MusicalProtagonistId)).Returns(bandOK);
            mockMapper.Setup(x => x.Map<BandResultBandDto>(bandOK)).Returns(resultBandDto);

            var result = api.GetBandById(bandOK.MusicalProtagonistId);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void GetBandsOkTest()
        {
            mock.Setup(x => x.GetBands(bandOK)).Returns(bandsOK);
            mockMapper.Setup(x => x.Map<Band>(getBandsDto)).Returns(bandOK);
            mockMapper.Setup(x => x.Map<IEnumerable<BandResultBandDto>>(bandsOK)).Returns(resultBandsDto);

            var result = api.GetBands(getBandsDto);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void GetBandsByArtistOkTest()
        {
            mock.Setup(x => x.GetBandsByArtist(artistOK.Artist)).Returns(bandsOK);
            mockMapper.Setup(x => x.Map<Artist>(getArtistsDto)).Returns(artistOK.Artist);
            mockMapper.Setup(x => x.Map<IEnumerable<BandResultBandDto>>(bandsOK)).Returns(resultBandsDto);

            var result = api.GetBandsByArtist(getArtistsDto);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void PostBandOkTest()
        {
            mock.Setup(x => x.InsertBand(bandOK)).Returns(bandOK);
            mockMapper.Setup(x => x.Map<Band>(insertBandDto)).Returns(bandOK);
            mockMapper.Setup(x => x.Map<BandResultBandDto>(bandOK)).Returns(resultBandDto);

            var result = api.PostBand(insertBandDto);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void PutBandOkTest()
        {
            mock.Setup(x => x.UpdateBand(bandOK)).Returns(bandOK);
            mockMapper.Setup(x => x.Map<Band>(updateBandDto)).Returns(bandOK);
            mockMapper.Setup(x => x.Map<BandResultBandDto>(bandOK)).Returns(resultBandDto);

            var result = api.PutBand(updateBandDto);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void DeleteBandOkTest()
        {
            mock.Setup(x => x.DeleteBand(It.IsAny<int>()));
            var result = api.DeleteBand(It.IsAny<int>());
            var objectResult = result as OkResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

    }
}
