using ArenaGestor.Business;
using ArenaGestor.BusinessInterface;
using ArenaGestor.Domain;
using ArenaGestor.Extensions.DTO;
using ArenaGestor.Extensions;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace ArenaGestor.BusinessTest
{
    [TestClass]
    public class ImportExportServiceTest
    {
        private Mock<IReflectionHelpers> reflectionMock;
        private Mock<IConcertsService> concertsMock;
        private Mock<IMapper> mockMapper;
        private Mock<IImportExportMethod> methodMock;
        private List<string> Methods;
        private ImportExportService service;
        private IImportExportMethod nullMethod;
        private IEnumerable<ConcertDto> concertsDtoOK;
        private IEnumerable<ConcertDto> concertsImportDtoOK;
        private IEnumerable<Concert> concertsOK;
        private Concert concertOK;

        [TestInitialize]
        public void InitTest()
        {
            reflectionMock = new Mock<IReflectionHelpers>(MockBehavior.Strict);
            concertsMock = new Mock<IConcertsService>(MockBehavior.Strict);
            mockMapper = new Mock<IMapper>(MockBehavior.Strict);
            methodMock = new Mock<IImportExportMethod>(MockBehavior.Strict);
            service = new ImportExportService(concertsMock.Object, reflectionMock.Object, mockMapper.Object);

            this.Methods = new List<string>() { "JSON", "XML" };

            nullMethod = null;

            concertOK = new Concert()
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
            concertsOK = new List<Concert>() { concertOK };
            concertsDtoOK = new List<ConcertDto>() { };
            concertsImportDtoOK = new List<ConcertDto>() { new ConcertDto()
            {
                ConcertId = 1,
                TourName = "Olé Tour",
                Date = DateTime.Now.AddDays(10),
                Price = 100,
                TicketCount = 500,
                Protagonists = new List<ConcertProtagonistDto>()
                {
                    new ConcertProtagonistDto()
                    {
                        MusicalProtagonistId = 1
                    }
                }
            }
            };
        }

        [TestMethod]
        public void GetMethodsTest()
        {
            reflectionMock.Setup(x => x.GetMethods()).Returns(this.Methods);
            List<string> methodsTest = service.GetMethods();
            Assert.AreEqual(2, methodsTest.Count);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ExportMethodNotExist()
        {
            reflectionMock.Setup(x => x.GetMethod(It.IsAny<string>())).Returns(this.nullMethod);
            service.ExportData("", "");
        }

        [TestMethod]
        public void ExportMethodSuccess()
        {
            mockMapper.Setup(x => x.Map<IEnumerable<ConcertDto>>(It.IsAny<IEnumerable<Concert>>())).Returns(concertsDtoOK);
            concertsMock.Setup(x => x.GetConcerts(It.IsAny<ConcertFilter>())).Returns(concertsOK);
            methodMock.Setup(x => x.Export(It.IsAny<string>(), It.IsAny<IEnumerable<ConcertDto>>()));
            reflectionMock.Setup(x => x.GetMethod(It.IsAny<string>())).Returns(this.methodMock.Object);
            service.ExportData("", "");
            concertsMock.VerifyAll();
            reflectionMock.VerifyAll();
            mockMapper.VerifyAll();
            methodMock.VerifyAll();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ImportMethodNotExist()
        {
            reflectionMock.Setup(x => x.GetMethod(It.IsAny<string>())).Returns(this.nullMethod);
            service.ImportData("", "");
        }

        [TestMethod]
        public void ImportMethodSuccess()
        {
            reflectionMock.Setup(x => x.GetMethod(It.IsAny<string>())).Returns(this.methodMock.Object);
            methodMock.Setup(x => x.Import(It.IsAny<string>())).Returns(concertsImportDtoOK);
            mockMapper.Setup(x=>x.Map<Concert>(It.IsAny<ConcertDto>())).Returns(concertOK);
            ConcertsInsertResult testResult = new ConcertsInsertResult();
            concertsMock.Setup(x => x.InsertConcerts(It.IsAny<List<Concert>>())).Returns(testResult);
            ConcertsInsertResult result = service.ImportData("", "");

            concertsMock.VerifyAll();
            reflectionMock.VerifyAll();
            mockMapper.VerifyAll();
            methodMock.VerifyAll();
        }
    }
}
