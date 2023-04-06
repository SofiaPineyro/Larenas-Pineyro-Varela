using ArenaGestor.BusinessInterface;
using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArenaGestor.Business
{
    public class TicketService : ITicketService
    {
        private IConcertsService concertService;
        private ITicketManagement ticketManagement;
        private ITicketStatusManagement ticketStatusManagement;
        private ISecurityService securityService;

        public TicketService(IConcertsService concertService, ITicketManagement ticketManagement, ITicketStatusManagement ticketStatusManagement, ISecurityService securityService)
        {
            this.concertService = concertService;
            this.ticketManagement = ticketManagement;
            this.ticketStatusManagement = ticketStatusManagement;
            this.securityService = securityService;
        }

        public Ticket SellTicket(TicketSell ticketSell)
        {
            if (ticketSell == null)
            {
                throw new ArgumentException("Invalid data in the sale");
            }

            if (!BusinessHelpers.CommonValidations.ValidEmail(ticketSell.Email))
            {
                throw new ArgumentException("The email it is invalid");
            }

            if (ticketSell.Amount <= 0)
            {
                throw new ArgumentException("The amount must be higher than 0");
            }

            Concert concert = concertService.GetConcertById(ticketSell.ConcertId);

            if (concert == null)
            {
                throw new NullReferenceException("The concert doesn't exists");
            }

            int ticketsSell = concert.Tickets != null && concert.Tickets.Any() ? concert.Tickets.Sum(t => t.Amount) : 0;

            if (concert.TicketCount <= ticketsSell)
            {
                throw new ArgumentException("The concert has no more tickets");
            }

            if (concert.TicketCount < ticketsSell + ticketSell.Amount)
            {
                throw new ArgumentException("There are fewer tickets than requested, there are currently 5 tickets available");
            }

            Ticket ticket = new Ticket
            {
                TicketId = new Guid(),
                ConcertId = ticketSell.ConcertId,
                Email = ticketSell.Email,
                TicketStatusId = ticketStatusManagement.GetStatus(TicketCode.Comprado).TicketStatusId,
                Amount = ticketSell.Amount
            };

            ticketManagement.InsertTicket(ticket);
            ticketManagement.Save();
            return ticket;
        }

        public Ticket BuyTicket(string token, TicketBuy ticketBuy)
        {
            if (ticketBuy == null)
            {
                throw new ArgumentException("Invalid data in the purchase");
            }

            if (ticketBuy.Amount <= 0)
            {
                throw new ArgumentException("The amount must be higher than 0");
            }

            Concert concert = concertService.GetConcertById(ticketBuy.ConcertId);

            if (concert == null)
            {
                throw new NullReferenceException("The concert doesn't exists");
            }

            int ticketsSell = concert.Tickets != null && concert.Tickets.Any() ? concert.Tickets.Sum(t => t.Amount) : 0;

            if (concert.TicketCount <= ticketsSell)
            {
                throw new ArgumentException("The concert has no more tickets");
            }

            if (concert.TicketCount < ticketsSell + ticketBuy.Amount)
            {
                throw new ArgumentException("There are fewer tickets than requested, there are currently 5 tickets available");
            }

            var user = securityService.GetUserOfToken(token);

            if (user == null)
            {
                throw new NullReferenceException("User is not logged in");
            }

            Ticket ticket = new Ticket
            {
                TicketId = new Guid(),
                ConcertId = ticketBuy.ConcertId,
                Email = user.Email,
                TicketStatusId = ticketStatusManagement.GetStatus(TicketCode.Comprado).TicketStatusId,
                Amount = ticketBuy.Amount
            };

            ticketManagement.InsertTicket(ticket);
            ticketManagement.Save();
            return ticket;
        }

        public Ticket ScanTicket(Guid ticketId)
        {
            Ticket ticketCheck = ticketManagement.GetTicketById(ticketId);
            if (ticketCheck == null)
            {
                throw new NullReferenceException("The ticket doesn't exists");
            }
            if (ticketCheck.TicketStatus.TicketStatusId == TicketCode.Utilizado)
            {
                throw new ArgumentException("Ticket already used");
            }

            ticketCheck.TicketStatus = ticketStatusManagement.GetStatus(TicketCode.Utilizado);
            ticketManagement.UpdateTicket(ticketCheck);
            ticketManagement.Save();
            return ticketCheck;
        }

        public IEnumerable<Ticket> GetTicketsByUser(string token)
        {
            var user = securityService.GetUserOfToken(token);

            return ticketManagement.GetTicketsByUser(user.Email);
        }

    }
}
