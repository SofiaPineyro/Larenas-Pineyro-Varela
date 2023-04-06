using ArenaGestor.DataAccess.Managements;
using ArenaGestor.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArenaGestor.DataAccessTest
{
    [TestClass]
    public class TicketManagementTest : ManagementTest
    {
        private DbContext context;
        private TicketManagement management;

        private Ticket ticketBuyedOk;
        private Ticket ticketScannedOk;
        private Ticket ticketNotExists;
        private List<Ticket> ticketsOK;
        private List<Ticket> ticketsAdded;

        private Concert concertOk;
        private User userOK;

        private TicketStatus buyedStatus;
        private TicketStatus usedStatus;

        [TestInitialize]
        public void InitTest()
        {

            concertOk = new Concert()
            {
                ConcertId = 1,
                TourName = "Olé Tour",
                Protagonists = new List<ConcertProtagonist>()
                {
                    new ConcertProtagonist()
                    {
                        Protagonist = new Band()
                        {
                            MusicalProtagonistId = 1,
                            Name = "The Rolling Stones",
                            StartDate = DateTime.Now,
                            Gender = new Gender()
                            {
                                GenderId = 1,
                                Name = "Rock"
                            },
                            Artists = new List<ArtistBand>()
                        }
                    }
                }
            };

            buyedStatus = new TicketStatus()
            {
                TicketStatusId = TicketCode.Comprado
            };

            usedStatus = new TicketStatus()
            {
                TicketStatusId = TicketCode.Utilizado
            };

            ticketBuyedOk = new Ticket()
            {
                TicketId = Guid.NewGuid(),
                TicketStatusId = buyedStatus.TicketStatusId,
                Concert = concertOk,
                TicketStatus = buyedStatus,
                Email = "test@user.com",
                ConcertId = concertOk.ConcertId
            };

            ticketScannedOk = new Ticket()
            {
                TicketId = Guid.NewGuid(),
                TicketStatusId = usedStatus.TicketStatusId,
                Concert = concertOk,
                TicketStatus = usedStatus,
                Email = "test@user.com",
                ConcertId = concertOk.ConcertId
            };

            ticketNotExists = new Ticket()
            {
                TicketId = Guid.NewGuid(),
                TicketStatusId = buyedStatus.TicketStatusId,
                Concert = concertOk,
                TicketStatus = buyedStatus,
                Email = "test@user.com",
                ConcertId = concertOk.ConcertId
            };

            ticketsOK = new List<Ticket>
            {
                ticketBuyedOk
            };

            ticketsAdded = new List<Ticket>
            {
                ticketBuyedOk,
                ticketNotExists
            };

            userOK = new User()
            {
                UserId = 1,
                Name = "Test",
                Surname = "User",
                Email = "test@user.com",
                Password = "testuser123",
                Roles = new List<UserRole>() {
                    new UserRole()
                    {
                        RoleId = RoleCode.Administrador
                    }
                }
            };

            CreateDataBase();
        }

        private void CreateDataBase()
        {
            context = CreateDbContext();
            context.Set<Ticket>().Add(ticketBuyedOk);
            context.Set<TicketStatus>().Add(buyedStatus);
            context.Set<TicketStatus>().Add(usedStatus);
            context.SaveChanges();

            management = new TicketManagement(context);
        }

        [TestMethod]
        public void GetTest()
        {
            var result = management.GetTickets().ToList();
            Assert.IsTrue(ticketsOK.SequenceEqual(result));
        }

        [TestMethod]
        public void GetWithFilterThatExistsTest()
        {
            Func<Ticket, bool> filter = new Func<Ticket, bool>(x => x.TicketId == ticketBuyedOk.TicketId);
            var result = management.GetTickets(filter).ToList();
            Assert.IsTrue(ticketsOK.SequenceEqual(result));
        }

        [TestMethod]
        public void GetWithFilterThatNotExistsTest()
        {
            Func<Ticket, bool> filter = new Func<Ticket, bool>(x => x.TicketId == ticketNotExists.TicketId);
            int size = management.GetTickets(filter).ToList().Count;
            Assert.AreEqual(0, size);
        }

        [TestMethod]
        public void GetWithFilterByIdTest()
        {
            Func<Ticket, bool> filter = new Func<Ticket, bool>(x => x.TicketId == ticketBuyedOk.TicketId);
            var result = management.GetTickets(filter).ToList();
            Assert.IsTrue(ticketsOK.SequenceEqual(result));
        }

        [TestMethod]
        public void GetById()
        {
            Ticket ticket = management.GetTicketById(ticketBuyedOk.TicketId);
            Assert.AreEqual(ticketBuyedOk, ticket);
        }

        [TestMethod]
        public void GetTicketsByUserThatExistsTest()
        {
            var result = management.GetTicketsByUser(ticketBuyedOk.Email).ToList();
            Assert.IsTrue(ticketsOK.SequenceEqual(result));
        }

        [TestMethod]
        public void GetTicketsByUserThatNotExistsTest()
        {
            int size = management.GetTicketsByUser("notexists@gmail.com").ToList().Count;
            Assert.AreEqual(0, size);
        }

        [TestMethod]
        public void InsertTest()
        {
            management.InsertTicket(ticketNotExists);
            management.Save();
            var result = management.GetTickets().ToList();
            Assert.IsTrue(ticketsAdded.SequenceEqual(result));
        }

        [TestMethod]
        public void UpdateTest()
        {
            ticketBuyedOk.TicketStatusId = usedStatus.TicketStatusId;
            management.UpdateTicket(ticketBuyedOk);
            management.Save();
            TicketCode newState = management.GetTickets().Where(g => g.TicketId == ticketBuyedOk.TicketId).First().TicketStatus.TicketStatusId;
            Assert.AreEqual(usedStatus.TicketStatusId, newState);
        }

        [TestMethod]
        public void DeleteTest()
        {
            management.DeleteTicket(ticketBuyedOk);
            management.Save();
            int size = management.GetTickets().ToList().Count;
            Assert.AreEqual(0, size);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.context.Database.EnsureDeleted();
        }
    }
}
