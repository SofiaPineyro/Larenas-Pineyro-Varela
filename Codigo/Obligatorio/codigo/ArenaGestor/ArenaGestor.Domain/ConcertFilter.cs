namespace ArenaGestor.Domain
{
    public class ConcertFilter
    {
        public string TourName { get; set; } = "";
        public DateRange DateRange { get; set; } = null;
        public bool Upcoming { get; set; } = false;
    }
}
