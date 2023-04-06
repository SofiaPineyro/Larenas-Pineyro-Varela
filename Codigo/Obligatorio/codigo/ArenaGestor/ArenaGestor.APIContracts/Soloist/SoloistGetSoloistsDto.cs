using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Soloist
{
    public class SoloistGetSoloistsDto
    {
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
