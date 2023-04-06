using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Artist
{
    public class ArtistUpdateArtistDto
    {
        [Required]
        public int ArtistId { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public int UserId { get; set; }
    }
}
