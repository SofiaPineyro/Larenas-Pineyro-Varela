using ArenaGestor.Domain;
using System;
using System.Collections.Generic;

namespace ArenaGestor.DataAccessInterface
{
    public interface ITicketManagement
    {
        IEnumerable<Ticket> GetTickets(Func<Ticket, bool> filter);
        IEnumerable<Ticket> GetTickets();
        void InsertTicket(Ticket ticket);
        Ticket GetTicketById(Guid guid);
        void DeleteTicket(Ticket ticket);
        void UpdateTicket(Ticket ticket);
        void Save();
        IEnumerable<Ticket> GetTicketsByUser(string email);
    }
}
