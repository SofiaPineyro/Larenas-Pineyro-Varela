using ArenaGestor.Domain;
using System;
using System.Collections.Generic;

namespace ArenaGestor.BusinessInterface
{
    public interface ITicketService
    {
        Ticket SellTicket(TicketSell ticketSell);
        Ticket BuyTicket(string token, TicketBuy ticketBuy);
        Ticket ScanTicket(Guid ticketId);
        IEnumerable<Ticket> GetTicketsByUser(string token);
    }
}
