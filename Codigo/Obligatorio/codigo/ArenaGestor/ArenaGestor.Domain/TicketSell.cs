using System;
using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.Domain
{
    public class TicketSell
    {
        [MaxLength(50)]
        [Required]
        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
        public string Email { get; set; }
        [Required]
        public int ConcertId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Amount { get; set; }
    }
}
