using System;
using System.Collections.Generic;

namespace ArenaGestor.APIContracts.Concert
{
    public class ConcertResultConcertDto
    {
        public int ConcertId { get; set; }
        public string TourName { get; set; }
        public DateTime Date { get; set; }
        public int TicketCount { get; set; }
        public double Price { get; set; }
        public List<ConcertGetMusicalProtagonistDto> Protagonists { get; set; }
        public ConcertResultLocationDto Location { get; set; }
    }
}
