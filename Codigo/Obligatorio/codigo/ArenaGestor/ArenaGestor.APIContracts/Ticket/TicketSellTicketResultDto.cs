using System;

namespace ArenaGestor.APIContracts.Ticket
{
    public class TicketSellTicketResultDto
    {
        public Guid TicketId { get; set; }
        public TicketStatusDto TicketStatus { get; set; }
        public string Email { get; set; }
        public int ConcertId { get; set; }
    }
}
