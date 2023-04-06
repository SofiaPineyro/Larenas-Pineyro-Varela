using ArenaGestor.DataAccess.Managements;
using ArenaGestor.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArenaGestor.DataAccessTest
{
    [TestClass]
    public class TicketStatusManagementTest : ManagementTest
    {
        private DbContext context;
        private TicketStatusManagement management;

        private TicketStatus ticketStatusOk;

        [TestInitialize]
        public void InitTest()
        {
            ticketStatusOk = new TicketStatus()
            {
                TicketStatusId = TicketCode.Comprado
            };

            CreateDataBase();
        }

        private void CreateDataBase()
        {
            context = CreateDbContext();
            context.Set<TicketStatus>().Add(ticketStatusOk);
            context.SaveChanges();

            management = new TicketStatusManagement(context);
        }

        [TestMethod]
        public void GetById()
        {
            TicketStatus ticketStatus = management.GetStatus(TicketCode.Comprado);
            Assert.AreEqual(ticketStatusOk, ticketStatus);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.context.Database.EnsureDeleted();
        }

    }
}
