using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Concert
{
    public class ConcertResultLocationDto
    {
        [Required]
        public int LocationId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Place { get; set; }

        [Required]
        public ConcertResultCountryDto Country { get; set; }

        [MaxLength(500)]
        [Required]
        public string Street { get; set; }

        [Required]
        public int Number { get; set; }
    }
}
