namespace ArenaGestor.APIContracts.Soloist
{
    public class SoloistResultLocationDto
    {
        public int LocationId { get; set; }
        public string Place { get; set; }
        public SoloistResultCountryDto Country { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
    }
}
