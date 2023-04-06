using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Band
{
    public class BandGetBandsDto
    {
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
