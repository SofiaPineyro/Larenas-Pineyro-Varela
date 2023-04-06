using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Band
{
    public class BandInsertArtistDto
    {
        [Required]
        public int ArtistId { get; set; }

        [Required]
        public int RoleArtistId { get; set; }

    }
}
