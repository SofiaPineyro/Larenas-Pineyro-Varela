namespace ArenaGestor.Domain
{
    public class ConcertProtagonist
    {
        public int ConcertId { get; set; }
        public int MusicalProtagonistId { get; set; }
        public Concert Concert { get; set; }
        public MusicalProtagonist Protagonist { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ConcertProtagonist other
                && (ConcertId == other.ConcertId || (this.Concert != null && this.Concert.Equals(other.Concert)))
                && (MusicalProtagonistId == other.MusicalProtagonistId || (this.Protagonist != null && this.Protagonist.Equals(other.Protagonist)));
        }
    }
}
