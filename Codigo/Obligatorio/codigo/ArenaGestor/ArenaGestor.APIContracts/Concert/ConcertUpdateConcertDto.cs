using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Concert
{
    public class ConcertUpdateConcertDto
    {
        public int ConcertId { get; set; }

        [MaxLength(50)]
        [Required]
        public string TourName { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int TicketCount { get; set; }
        [Required]
        public double Price { get; set; }

        [Required]
        public List<ConcertUpdateProtagonistDto> Protagonists { get; set; }

        [Required]
        public ConcertUpdateLocationDto Location { get; set; }

    }
}
