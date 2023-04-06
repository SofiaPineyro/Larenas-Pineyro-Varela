using ArenaGestor.Business;
using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace ArenaGestor.BusinessTest
{

    [TestClass]
    public class CountrysServiceTest
    {
        private Mock<ICountrysManagement> managementMock;
        private CountrysService managementService;

        private Country countryOK;
        private Country countryNull;
        private int countryIdZero;
        private int countryIdInexistant;
        private List<Country> countrys;

        [TestInitialize]
        public void InitTest()
        {
            managementMock = new Mock<ICountrysManagement>(MockBehavior.Strict);
            managementService = new CountrysService(managementMock.Object);

            countryOK = new Country()
            {
                CountryId = 1,
                Name = "Uruguay"
            };

            countrys = new List<Country>
            {
                countryOK
            };
            countryNull = null;
            countryIdZero = 0;
            countryIdInexistant = 2;
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void GetCountryByIdInvalidIdTest()
        {
            managementService.GetCountryById(countryIdZero);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void GetCountryByIdNonExistTest()
        {
            managementMock.Setup(x => x.GetCountryById(countryIdInexistant)).Returns(countryNull);
            managementService.GetCountryById(countryIdInexistant);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetCountryByIdOkTest()
        {
            managementMock.Setup(x => x.GetCountryById(countryOK.CountryId)).Returns(countryOK);
            managementService.GetCountryById(countryOK.CountryId);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetCountrysOkTest()
        {
            managementMock.Setup(x => x.GetCountrys()).Returns(countrys);
            managementService.GetCountrys();
            managementMock.VerifyAll();
        }
    }
}
