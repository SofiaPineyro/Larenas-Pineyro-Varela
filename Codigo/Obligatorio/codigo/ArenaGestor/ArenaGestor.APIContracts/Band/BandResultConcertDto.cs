using System;

namespace ArenaGestor.APIContracts.Band
{
    public class BandResultConcertDto
    {
        public int ConcertId { get; set; }
        public string TourName { get; set; }
        public DateTime Date { get; set; }
        public int TicketCount { get; set; }
        public double Price { get; set; }
        public BandResultLocationDto Location { get; set; }
    }
}
