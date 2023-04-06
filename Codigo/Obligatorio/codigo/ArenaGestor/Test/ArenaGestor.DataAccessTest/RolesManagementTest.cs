using ArenaGestor.DataAccess.Managements;
using ArenaGestor.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ArenaGestor.DataAccessTest
{
    [TestClass]
    public class RolesManagementTest : ManagementTest
    {
        private DbContext context;
        private RolesManagement management;

        private Role roleUserOk;
        private RoleArtist roleArtistOk;

        [TestInitialize]
        public void InitTest()
        {
            roleUserOk = new Role() { Name = RoleCode.Administrador.ToString() };
            roleArtistOk = new RoleArtist() { Name = RoleArtistCode.Cantante.ToString() };

            CreateDataBase();
        }

        private void CreateDataBase()
        {
            context = CreateDbContext();
            context.Set<Role>().Add(roleUserOk);
            context.Set<RoleArtist>().Add(roleArtistOk);
            context.SaveChanges();
            management = new RolesManagement(context);
        }

        [TestMethod]
        public void GetUserRolesTest()
        {
            var result = management.GetUserRoles().ToList();
            Assert.IsTrue(result.Count == 1);
        }

        [TestMethod]
        public void GetArtistRolesTest()
        {
            var result = management.GetArtistRoles().ToList();
            Assert.IsTrue(result.Count == 1);
        }
    }
}
