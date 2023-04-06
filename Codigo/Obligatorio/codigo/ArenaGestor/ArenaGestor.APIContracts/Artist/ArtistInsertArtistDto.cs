using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Artist
{
    public class ArtistInsertArtistDto
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public int UserId { get; set; }
    }
}
