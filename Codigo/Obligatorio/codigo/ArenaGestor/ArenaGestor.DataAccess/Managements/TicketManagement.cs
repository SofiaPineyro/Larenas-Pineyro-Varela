using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArenaGestor.DataAccess.Managements
{
    public class TicketManagement : ITicketManagement
    {
        private readonly DbSet<Ticket> tickets;
        private readonly DbContext context;

        public TicketManagement(DbContext context)
        {
            this.tickets = context.Set<Ticket>();
            this.context = context;
        }

        public Ticket GetTicketById(Guid guid)
        {
            return tickets.AsNoTracking().Include(x => x.TicketStatus).FirstOrDefault(ticket => ticket.TicketId == guid);
        }

        public IEnumerable<Ticket> GetTickets(Func<Ticket, bool> filter)
        {
            return tickets.Include(x => x.TicketStatus).Where(filter);
        }

        public IEnumerable<Ticket> GetTickets()
        {
            return tickets;
        }

        public IEnumerable<Ticket> GetTicketsByUser(string email)
        {
            return tickets.Include(t => t.TicketStatus).Include(t => t.Concert).Where(t => t.Email == email);
        }

        public void InsertTicket(Ticket ticket)
        {
            tickets.Add(ticket);
        }

        public void UpdateTicket(Ticket ticket)
        {
            tickets.Update(ticket);
        }

        public void DeleteTicket(Ticket ticket)
        {
            tickets.Remove(ticket);
        }

        public void Save()
        {
            context.SaveChanges();
        }

    }
}
