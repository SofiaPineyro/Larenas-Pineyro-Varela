using System;
using System.Collections.Generic;

namespace ArenaGestor.Extensions.DTO
{
    public class ConcertDto
    {
        public int ConcertId { get; set; }
        public string TourName { get; set; }
        public DateTime Date { get; set; }
        public int TicketCount { get; set; }
        public double Price { get; set; }
        public List<ConcertProtagonistDto> Protagonists { get; set; }
    }
}
