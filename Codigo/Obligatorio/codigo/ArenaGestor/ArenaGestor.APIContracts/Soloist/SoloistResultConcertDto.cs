using System;

namespace ArenaGestor.APIContracts.Soloist
{
    public class SoloistResultConcertDto
    {
        public int ConcertId { get; set; }
        public string TourName { get; set; }
        public DateTime Date { get; set; }
        public int TicketCount { get; set; }
        public double Price { get; set; }
        public SoloistResultLocationDto Location { get; set; }

    }
}
