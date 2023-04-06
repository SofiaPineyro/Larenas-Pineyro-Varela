using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Artist
{
    public class ArtistGetArtistsDto
    {
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
