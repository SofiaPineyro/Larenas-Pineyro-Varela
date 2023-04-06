using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Band
{
    public class BandResultRoleArtistDto
    {
        public int RoleArtistId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
