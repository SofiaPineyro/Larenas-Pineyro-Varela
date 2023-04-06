using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Soloist
{
    public class SoloistUpdateRoleArtistDto
    {
        [Required]
        public int RoleArtistId { get; set; }
    }
}
