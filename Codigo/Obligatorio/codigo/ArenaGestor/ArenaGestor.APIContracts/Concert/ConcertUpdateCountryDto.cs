using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Concert
{
    public class ConcertUpdateCountryDto
    {
        [Required]
        public int CountryId { get; set; }
    }
}
