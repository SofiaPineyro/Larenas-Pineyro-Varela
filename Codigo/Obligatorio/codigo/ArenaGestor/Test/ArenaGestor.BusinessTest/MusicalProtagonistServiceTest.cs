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
    public class MusicalProtagonistServiceTest
    {
        private Mock<IMusicalProtagonistManagement> managementMock;
        private MusicalProtagonistService managementService;

        private MusicalProtagonist musicalProtagonistNull;
        private MusicalProtagonist musicalProtagonistOk;

        private IEnumerable<MusicalProtagonist> musicalProtagonistsOk;

        [TestInitialize]
        public void InitTest()
        {
            managementMock = new Mock<IMusicalProtagonistManagement>(MockBehavior.Strict);

            managementService = new MusicalProtagonistService(managementMock.Object);

            musicalProtagonistNull = null;
            musicalProtagonistOk = new Soloist()
            {
                Name = "Test"
            };
            musicalProtagonistsOk = new List<MusicalProtagonist>()
            {
                musicalProtagonistOk
            };
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void GetMusicalProtagonistByIdInvalidId()
        {
            managementService.GetMusicalProtagonistById(0);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void GetMusicalProtagonistByIdNonExist()
        {
            managementMock.Setup(x => x.GetMusicalProtagonistById(It.IsAny<int>())).Returns(musicalProtagonistNull);
            managementService.GetMusicalProtagonistById(1);
            managementMock.VerifyAll();
        }
        [TestMethod]
        public void GetMusicalProtagonistByIdOk()
        {
            managementMock.Setup(x => x.GetMusicalProtagonistById(It.IsAny<int>())).Returns(musicalProtagonistOk);
            managementService.GetMusicalProtagonistById(1);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetAllProtagonistsTest()
        {
            managementMock.Setup(x => x.GetMusicalProtagonist()).Returns(musicalProtagonistsOk);
            managementService.GetMusicalProtagonist();
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetAllProtagonistsFiltersTest()
        {
            managementMock.Setup(x => x.GetMusicalProtagonist(It.IsAny<Func<MusicalProtagonist, bool>>())).Returns(musicalProtagonistsOk);
            managementService.GetMusicalProtagonist(musicalProtagonistOk);
            managementMock.VerifyAll();
        }
    }
}
