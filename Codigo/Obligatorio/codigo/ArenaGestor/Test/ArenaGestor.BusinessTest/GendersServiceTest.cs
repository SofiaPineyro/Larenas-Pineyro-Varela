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
    public class GendersServiceTest
    {
        private Mock<IGendersManagement> managementMock;
        private GendersService managementService;

        private Gender genderOK;
        private Gender genderWithProtagonists;
        private Gender genderNull;
        private IEnumerable<Gender> gendersOK;
        private IEnumerable<Gender> gendersEmpty;
        private Gender genderEmptyName;
        private Gender genderNullName;

        private int genderIdZero;
        private int genderIdInexistant;

        [TestInitialize]
        public void InitTest()
        {
            managementMock = new Mock<IGendersManagement>(MockBehavior.Strict);
            managementService = new GendersService(managementMock.Object);

            genderOK = new Gender()
            {
                GenderId = 1,
                Name = "Rock"
            };

            genderWithProtagonists = new Gender()
            {
                GenderId = 1,
                Name = "Rock",
                MusicalProtagonists = new List<MusicalProtagonist>()
                {
                     new Soloist()
                     {
                         Name = "Test soloist"
                     }
                }
            };

            genderEmptyName = new Gender()
            {
                GenderId = 1,
                Name = ""
            };

            genderNullName = new Gender()
            {
                GenderId = 1,
                Name = null
            };

            genderNull = null;
            gendersOK = new List<Gender>() { genderOK };
            gendersEmpty = new List<Gender>() { };
            genderIdZero = 0;
            genderIdInexistant = 2;
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void GetGenderByIdInvalidIdTest()
        {
            managementService.GetGenderById(genderIdZero);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void GetGenderByIdNonExistTest()
        {
            managementMock.Setup(x => x.GetGenderById(genderIdInexistant)).Returns(genderNull);
            managementService.GetGenderById(genderIdInexistant);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetGenderByIdOkTest()
        {
            managementMock.Setup(x => x.GetGenderById(genderOK.GenderId)).Returns(genderOK);
            managementService.GetGenderById(genderOK.GenderId);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetAllGendersTest()
        {
            managementMock.Setup(x => x.GetGenders()).Returns(gendersOK);
            managementService.GetGenders();
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetFilterGenderTest()
        {
            managementMock.Setup(x => x.GetGenders(It.IsAny<Func<Gender, bool>>())).Returns(gendersOK);
            managementService.GetGenders(genderOK);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetFilterGenderEmptyNameTest()
        {
            managementMock.Setup(x => x.GetGenders()).Returns(gendersOK);
            managementService.GetGenders(genderEmptyName);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetFilterGenderNullNameTest()
        {
            managementMock.Setup(x => x.GetGenders()).Returns(gendersOK);
            managementService.GetGenders(genderNullName);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertNullTest()
        {
            managementService.InsertGender(genderNull);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertEmptyNameTest()
        {
            managementService.InsertGender(genderEmptyName);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertNullNameTest()
        {
            managementService.InsertGender(genderNullName);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertGenderNameExists()
        {
            managementMock.Setup(x => x.GetGenders(It.IsAny<Func<Gender, bool>>())).Returns(gendersOK);
            managementService.InsertGender(genderOK);
        }

        [TestMethod]
        public void InsertGenderOkTest()
        {
            managementMock.Setup(x => x.GetGenders(It.IsAny<Func<Gender, bool>>())).Returns(gendersEmpty);
            managementMock.Setup(x => x.InsertGender(genderOK));
            managementMock.Setup(x => x.Save());
            managementService.InsertGender(genderOK);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateNullTest()
        {
            managementService.UpdateGender(null);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateEmptyNameTest()
        {
            managementService.UpdateGender(genderEmptyName);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateNullNameTest()
        {
            managementService.UpdateGender(genderNullName);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void UpdateGenderNonExistTest()
        {
            managementMock.Setup(x => x.GetGenderById(genderOK.GenderId)).Returns(genderNull);
            managementService.UpdateGender(genderOK);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void UpdateGenderOkTest()
        {
            managementMock.Setup(x => x.GetGenderById(It.IsAny<int>())).Returns(genderOK);
            managementMock.Setup(x => x.UpdateGender(genderOK));
            managementMock.Setup(x => x.Save());
            managementService.UpdateGender(genderOK);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void DeleteGenderInvalidIdTest()
        {
            managementService.DeleteGender(genderIdZero);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void DeleteGenderNonExistTest()
        {
            managementMock.Setup(x => x.GetGenderById(genderIdInexistant)).Returns(genderNull);
            managementService.DeleteGender(genderIdInexistant);
            managementMock.VerifyAll();
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void DeleteGenderWithProtagonistsTest()
        {
            managementMock.Setup(x => x.GetGenderById(genderIdInexistant)).Returns(genderWithProtagonists);
            managementService.DeleteGender(genderIdInexistant);
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void DeleteGenderOkTest()
        {
            managementMock.Setup(x => x.GetGenderById(genderOK.GenderId)).Returns(genderOK);
            managementMock.Setup(x => x.DeleteGender(genderOK));
            managementMock.Setup(x => x.Save());
            managementService.DeleteGender(genderOK.GenderId);
            managementMock.VerifyAll();
        }
    }
}
