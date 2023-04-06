using System;
using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.Domain
{
    public class TicketBuy
    {
        [Required]
        public int ConcertId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Amount { get; set; }
    }
}
