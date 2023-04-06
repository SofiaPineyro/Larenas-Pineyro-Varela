using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Band
{
    public class BandGetArtistsDto
    {
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
