using ArenaGestor.API.Controllers;
using ArenaGestor.APIContracts.Country;
using ArenaGestor.BusinessInterface;
using ArenaGestor.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace ArenaGestor.APITestcountry
{

    [TestClass]
    public class CountrysControllerTest
    {
        private Mock<ICountrysService> mock;
        private Mock<IMapper> mockMapper;
        private CountrysController api;
        private IEnumerable<Country> countrysOk;
        private IEnumerable<CountryResultDto> resultCountrysDto;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<ICountrysService>(MockBehavior.Strict);
            mockMapper = new Mock<IMapper>(MockBehavior.Strict);

            api = new CountrysController(mock.Object, mockMapper.Object);
            countrysOk = new List<Country>()
            {
                new Country() {
                    CountryId = 1,
                    Name = "Uruguay"
                }
            };

            resultCountrysDto = new List<CountryResultDto>()
            {
                new CountryResultDto()
                {
                    CountryId = 1,
                    Name = "Uruguay"
                }
            };
        }

        [TestMethod]
        public void GetCountrysOkTest()
        {
            mock.Setup(x => x.GetCountrys()).Returns(countrysOk);
            mockMapper.Setup(x => x.Map<IEnumerable<CountryResultDto>>(countrysOk)).Returns(resultCountrysDto);

            var result = api.GetCountrys();
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

    }
}
