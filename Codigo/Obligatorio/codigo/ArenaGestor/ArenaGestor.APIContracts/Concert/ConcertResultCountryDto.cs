using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Concert
{
    public class ConcertResultCountryDto
    {
        public int CountryId { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
    }
}
