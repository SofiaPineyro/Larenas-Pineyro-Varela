using ArenaGestor.Business.Helpers;
using ArenaGestor.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace ArenaGestor.BusinessTest
{
    [TestClass]
    public class ReflectionHelpersTest
    {
        [TestInitialize]
        public void InitTest()
        {
            File.Copy(@"../../../../../appsettings.json", "appsettings.json", true);
        }

        [TestMethod]
        public void GetAllMethods()
        {
            ReflectionHelpers helpers = new ReflectionHelpers();
            List<string> methods = helpers.GetMethods();
            Assert.AreEqual(2, methods.Count);
        }

        [TestMethod]
        public void GetMethodNull()
        {
            ReflectionHelpers helpers = new ReflectionHelpers();
            IImportExportMethod method = helpers.GetMethod("DB");
            Assert.IsNull(method);
        }

        [TestMethod]
        public void GetMethod()
        {
            ReflectionHelpers helpers = new ReflectionHelpers();
            IImportExportMethod method = helpers.GetMethod("JSON");
            Assert.IsNotNull(method);
        }
    }
}
