using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.Domain
{
    public class RoleArtist
    {
        public RoleArtistCode RoleArtistId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
    }
}
