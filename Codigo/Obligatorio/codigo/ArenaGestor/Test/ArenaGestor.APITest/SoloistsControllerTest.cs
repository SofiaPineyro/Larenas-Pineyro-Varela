using ArenaGestor.API.Controllers;
using ArenaGestor.APIContracts.Soloist;
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
    public class SoloistsControllerTest
    {
        private Mock<ISoloistsService> mock;
        private Mock<IMapper> mockMapper;
        private SoloistsController api;

        private Soloist soloistOK;
        private IEnumerable<Soloist> soloistsOK;
        private Gender genderOK;
        private Artist artistOK;

        private SoloistResultSoloistDto resultSoloistDto;
        private IEnumerable<SoloistResultSoloistDto> resultSoloistsDto;
        private SoloistResultArtistDto resultArtistDto;
        private SoloistResultGenderDto resultGenderDto;
        private SoloistResultConcertDto resultConcertDto;
        private SoloistGetSoloistsDto getSoloistsDto;
        private SoloistGetArtistsDto getArtistsDto;
        private SoloistInsertSoloistDto insertSoloistDto;
        private SoloistUpdateSoloistDto updateSoloistDto;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<ISoloistsService>(MockBehavior.Strict);
            mockMapper = new Mock<IMapper>(MockBehavior.Strict);

            api = new SoloistsController(mock.Object, mockMapper.Object);

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

            soloistOK = new Soloist()
            {
                MusicalProtagonistId = 1,
                Name = "Nirvana",
                Artist = artistOK,
                Gender = genderOK,
                StartDate = new DateTime(1987, 08, 01)
            };

            soloistsOK = new List<Soloist>() { soloistOK };

            resultArtistDto = new SoloistResultArtistDto()
            {
                ArtistId = 1,
                Name = "Kurt Cobain"
            };

            resultGenderDto = new SoloistResultGenderDto()
            {
                GenderId = 1,
                Name = "Rock"
            };

            resultConcertDto = new SoloistResultConcertDto()
            {
                ConcertId = 1,
                TourName = "Olé Tour",
                Date = DateTime.Now.AddDays(10),
                Price = 100,
                TicketCount = 500
            };

            resultSoloistDto = new SoloistResultSoloistDto()
            {
                MusicalProtagonistId = 1,
                Name = "Nirvana",
                Artist = resultArtistDto,
                Gender = resultGenderDto,
                StartDate = new DateTime(1987, 08, 01),
                Concerts = new List<SoloistResultConcertDto>()
                {
                    resultConcertDto
                },
                RoleArtist = new SoloistResultRoleArtistDto()
                {
                    Name = RoleArtistCode.Cantante.ToString()
                }
            };

            getSoloistsDto = new SoloistGetSoloistsDto()
            {
                Name = "Nirvana"
            };

            resultSoloistsDto = new List<SoloistResultSoloistDto>()
            {
                resultSoloistDto
            };

            getArtistsDto = new SoloistGetArtistsDto()
            {
                Name = "Kurt Cobain"
            };

            insertSoloistDto = new SoloistInsertSoloistDto()
            {
                ArtistId = 1,
                GenderId = 1,
                Name = "Kurt Cobain",
                StartDate = new DateTime(1987, 08, 01),
                RoleArtistId = (int)RoleArtistCode.Cantante
            };

            updateSoloistDto = new SoloistUpdateSoloistDto()
            {
                ArtistId = 1,
                GenderId = 1,
                Name = "Kurt Cobain",
                StartDate = new DateTime(1987, 08, 01),
                RoleArtistId = (int)RoleArtistCode.Cantante
            };
        }

        [TestMethod]
        public void GetSoloistByIdOkTest()
        {
            mock.Setup(x => x.GetSoloistById(soloistOK.MusicalProtagonistId)).Returns(soloistOK);
            mockMapper.Setup(x => x.Map<SoloistResultSoloistDto>(soloistOK)).Returns(resultSoloistDto);

            var result = api.GetSoloistById(soloistOK.MusicalProtagonistId);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void GetSoloistsOkTest()
        {
            mock.Setup(x => x.GetSoloists(soloistOK)).Returns(soloistsOK);
            mockMapper.Setup(x => x.Map<Soloist>(getSoloistsDto)).Returns(soloistOK);
            mockMapper.Setup(x => x.Map<IEnumerable<SoloistResultSoloistDto>>(soloistsOK)).Returns(resultSoloistsDto);

            var result = api.GetSoloists(getSoloistsDto);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void GetSoloistsByArtistOkTest()
        {
            mock.Setup(x => x.GetSoloistsByArtist(artistOK)).Returns(soloistsOK);
            mockMapper.Setup(x => x.Map<Artist>(getArtistsDto)).Returns(artistOK);
            mockMapper.Setup(x => x.Map<IEnumerable<SoloistResultSoloistDto>>(soloistsOK)).Returns(resultSoloistsDto);

            var result = api.GetSoloistsByArtist(getArtistsDto);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void PostSoloistOkTest()
        {
            mock.Setup(x => x.InsertSoloist(soloistOK)).Returns(soloistOK);
            mockMapper.Setup(x => x.Map<Soloist>(insertSoloistDto)).Returns(soloistOK);
            mockMapper.Setup(x => x.Map<SoloistResultSoloistDto>(soloistOK)).Returns(resultSoloistDto);

            var result = api.PostSoloist(insertSoloistDto);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void PutSoloistOkTest()
        {
            mock.Setup(x => x.UpdateSoloist(soloistOK)).Returns(soloistOK);
            mockMapper.Setup(x => x.Map<Soloist>(updateSoloistDto)).Returns(soloistOK);
            mockMapper.Setup(x => x.Map<SoloistResultSoloistDto>(soloistOK)).Returns(resultSoloistDto);

            var result = api.PutSoloist(updateSoloistDto);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void DeleteSoloistOkTest()
        {
            mock.Setup(x => x.DeleteSoloist(It.IsAny<int>()));
            var result = api.DeleteSoloist(It.IsAny<int>());
            var objectResult = result as OkResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

    }
}
