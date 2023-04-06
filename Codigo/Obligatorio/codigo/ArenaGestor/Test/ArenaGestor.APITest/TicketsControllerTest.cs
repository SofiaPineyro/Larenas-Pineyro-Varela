using ArenaGestor.API.Controllers;
using ArenaGestor.APIContracts.Ticket;
using ArenaGestor.BusinessInterface;
using ArenaGestor.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace ArenaGestor.APITest
{
    [TestClass]
    public class TicketsControllerTest
    {
        private Mock<ITicketService> mock;
        private Mock<IMapper> mockMapper;

        private TicketsController api;
        private TicketScanTicketDto ticketScannedDtoOk;
        private TicketSellTicketDto ticketSelledDtoOk;
        private TicketSellTicketDto ticketSelledBadAmountDtoOk;
        private TicketBuyTicketDto ticketBuyedDtoOk;
        private TicketBuyTicketDto ticketBuyedBadAmountDtoOk;

        private List<TicketGetTicketResultDto> ticketsResultDtoOk;

        private Ticket ticketBuyedOk;
        private List<Ticket> ticketsOK;
        private string randomToken;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<ITicketService>(MockBehavior.Strict);
            mockMapper = new Mock<IMapper>(MockBehavior.Strict);
            api = new TicketsController(mock.Object, mockMapper.Object);

            randomToken = BusinessHelpers.StringGenerator.GenerateRandomToken(64);

            ticketSelledDtoOk = new TicketSellTicketDto()
            {
                ConcertId = 1,
                Email = "test@test.com",
                Amount = 1
            };

            ticketSelledBadAmountDtoOk = new TicketSellTicketDto()
            {
                ConcertId = 1,
                Email = "test@test.com",
                Amount = 5000
            };

            ticketBuyedDtoOk = new TicketBuyTicketDto()
            {
                ConcertId = 1,
                Amount = 1
            };

            ticketBuyedBadAmountDtoOk = new TicketBuyTicketDto()
            {
                ConcertId = 1,
                Amount = 5000
            };

            ticketScannedDtoOk = new TicketScanTicketDto()
            {
                TicketId = Guid.NewGuid()
            };

            TicketStatus buyedStatus = new TicketStatus()
            {
                TicketStatusId = TicketCode.Comprado,
                Status = TicketCode.Comprado.ToString()
            };

            Concert concertOk = new Concert()
            {
                ConcertId = 1,
                TourName = "Olé Tour",
                Date = DateTime.Now.AddDays(10),
                Price = 100,
                TicketCount = 500,
                Tickets = new List<Ticket>()
                {
                    ticketBuyedOk
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

            ticketBuyedOk = new Ticket()
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

            ticketsResultDtoOk = new List<TicketGetTicketResultDto>()
            {
                new TicketGetTicketResultDto()
                {
                    ConcertId = concertOk.ConcertId,
                    Email = ticketBuyedOk.Email,
                    TicketId= ticketBuyedOk.TicketId,
                    TicketStatus = new TicketStatusDto()
                    {
                        Status = ticketBuyedOk.TicketStatus.ToString()
                    }
                }
            };
        }

        [TestMethod]
        public void BuyTicketOk()
        {
            mock.Setup(x => x.SellTicket(It.IsAny<TicketSell>())).Returns(It.IsAny<Ticket>());
            mockMapper.Setup(x => x.Map<TicketSell>(It.IsAny<TicketSellTicketDto>())).Returns(It.IsAny<TicketSell>());

            mockMapper.Setup(x => x.Map<TicketSellTicketResultDto>(It.IsAny<Ticket>())).Returns(It.IsAny<TicketSellTicketResultDto>());

            var result = api.PostTickets(ticketSelledDtoOk);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void ScanTicketOk()
        {
            mock.Setup(x => x.ScanTicket(It.IsAny<Guid>())).Returns(It.IsAny<Ticket>());

            mockMapper.Setup(x => x.Map<TicketScanTicketResultDto>(It.IsAny<Ticket>())).Returns(It.IsAny<TicketScanTicketResultDto>());

            var result = api.PutTickets(ticketScannedDtoOk);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void GetTicketsOkTest()
        {
            mock.Setup(x => x.GetTicketsByUser(It.IsAny<string>())).Returns(ticketsOK);
            mockMapper.Setup(x => x.Map<IEnumerable<TicketGetTicketResultDto>>(ticketsOK)).Returns(ticketsResultDtoOk);
            api.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            api.ControllerContext.HttpContext.Request.Headers["token"] = randomToken;

            var result = api.GetTickets();
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void BuyTicketOkTest()
        {
            mock.Setup(x => x.BuyTicket(It.IsAny<string>(), It.IsAny<TicketBuy>())).Returns(It.IsAny<Ticket>());
            mockMapper.Setup(x => x.Map<TicketBuy>(It.IsAny<TicketBuyTicketDto>())).Returns(It.IsAny<TicketBuy>());

            mockMapper.Setup(x => x.Map<TicketBuyTicketResultDto>(It.IsAny<Ticket>())).Returns(It.IsAny<TicketBuyTicketResultDto>());

            api.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            api.ControllerContext.HttpContext.Request.Headers["token"] = randomToken;

            var result = api.PostTickets(ticketBuyedDtoOk);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

    }
}
