using ArenaGestor.API.Controllers;
using ArenaGestor.APIContracts.ImportExport;
using ArenaGestor.BusinessInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace ArenaGestor.APITest
{
    [TestClass]
    public class ImportExportControllerTest
    {
        private Mock<IImportExportService> mock;

        private ImportExportController api;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IImportExportService>(MockBehavior.Strict);

            api = new ImportExportController(mock.Object);
        }

        [TestMethod]
        public void GetMethodsOk()
        {
            mock.Setup(x => x.GetMethods()).Returns(It.IsAny<List<string>>);
            var result = api.GetMethods();
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void ExportMethodsOk()
        {
            mock.Setup(x => x.ExportData(It.IsAny<string>(), It.IsAny<string>()));
            var result = api.Export(new ImportExportDto());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void ImportMethodsOk()
        {
            mock.Setup(x => x.ImportData(It.IsAny<string>(), It.IsAny<string>())).Returns(new Domain.ConcertsInsertResult());
            var result = api.Import(new ImportExportDto());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }
    }
}
