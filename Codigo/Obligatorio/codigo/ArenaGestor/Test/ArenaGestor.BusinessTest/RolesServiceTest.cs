using ArenaGestor.Business;
using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace ArenaGestor.BusinessTest
{
    [TestClass]
    public class RolesServiceTest
    {
        private Mock<IRolesManagement> managementMock;
        private RolesService managementService;
        private IEnumerable<Role> rolesUserOk;
        private IEnumerable<RoleArtist> rolesArtistOk;

        [TestInitialize]
        public void InitTest()
        {
            managementMock = new Mock<IRolesManagement>(MockBehavior.Strict);
            managementService = new RolesService(managementMock.Object);

            rolesUserOk = new List<Role>() { new Role() { RoleId = RoleCode.Administrador, Name = RoleCode.Administrador.ToString() } };
            rolesArtistOk = new List<RoleArtist>() { new RoleArtist() { RoleArtistId = RoleArtistCode.Cantante, Name = RoleArtistCode.Cantante.ToString() } };
        }

        [TestMethod]
        public void GetUserRolesTest()
        {
            managementMock.Setup(x => x.GetUserRoles()).Returns(rolesUserOk);
            managementService.GetUserRoles();
            managementMock.VerifyAll();
        }

        [TestMethod]
        public void GetArtistRolesTest()
        {
            managementMock.Setup(x => x.GetArtistRoles()).Returns(rolesArtistOk);
            managementService.GetArtistRoles();
            managementMock.VerifyAll();
        }

    }
}
