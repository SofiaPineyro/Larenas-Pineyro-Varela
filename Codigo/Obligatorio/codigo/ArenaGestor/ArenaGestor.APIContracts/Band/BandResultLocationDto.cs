namespace ArenaGestor.APIContracts.Band
{
    public class BandResultLocationDto
    {

        public int LocationId { get; set; }
        public string Place { get; set; }
        public BandResultCountryDto Country { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
    }
}
