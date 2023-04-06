using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Concert
{
    public class ConcertInsertCountryDto
    {
        [Required]
        public int CountryId { get; set; }
    }
}
