using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Ticket
{
    public class TicketBuyTicketDto
    {
        public int ConcertId { get; set; }

        public int Amount { get; set; }
    }
}
