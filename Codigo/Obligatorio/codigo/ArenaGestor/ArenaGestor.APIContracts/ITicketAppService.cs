using ArenaGestor.APIContracts.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace ArenaGestor.APIContracts
{
    public interface ITicketAppService
    {
        IActionResult PostTickets(TicketSellTicketDto sellTicket);
        IActionResult PostTickets(TicketBuyTicketDto buyTicket);
        IActionResult PutTickets(TicketScanTicketDto scanTicket);
        IActionResult GetTickets();
    }
}
