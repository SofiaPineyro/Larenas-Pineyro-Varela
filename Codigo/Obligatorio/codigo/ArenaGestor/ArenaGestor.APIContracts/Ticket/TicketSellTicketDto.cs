using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Ticket
{
    public class TicketSellTicketDto
    {
        public string Email { get; set; }
        public int ConcertId { get; set; }
        public int Amount { get; set; }
    }
}
