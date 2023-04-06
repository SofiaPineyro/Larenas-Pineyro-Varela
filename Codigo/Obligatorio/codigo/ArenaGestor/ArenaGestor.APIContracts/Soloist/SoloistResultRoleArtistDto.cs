using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Soloist
{
    public class SoloistResultRoleArtistDto
    {
        public int RoleArtistId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
