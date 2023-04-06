using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Soloist
{
    public class SoloistInsertRoleArtistDto
    {
        [Required]
        public int RoleArtistId { get; set; }
    }
}
