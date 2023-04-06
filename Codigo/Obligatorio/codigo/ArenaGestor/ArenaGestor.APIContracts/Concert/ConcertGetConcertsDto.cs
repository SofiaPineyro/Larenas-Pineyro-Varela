namespace ArenaGestor.APIContracts.Concert
{
    public class ConcertGetConcertsDto
    {
        public string TourName { get; set; }
        public ConcertGetDateRangeConcertsDto DateRange { get; set; }
        public bool Upcoming { get; set; }

    }
}
