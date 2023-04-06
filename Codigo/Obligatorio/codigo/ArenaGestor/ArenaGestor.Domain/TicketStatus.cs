using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.Domain
{
    public class TicketStatus
    {
        public TicketCode TicketStatusId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Status { get; set; }

    }
}
