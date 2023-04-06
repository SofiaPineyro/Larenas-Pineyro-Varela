using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Concert
{
    public class ConcertInsertLocationDto
    {
        [MaxLength(50)]
        [Required]
        public string Place { get; set; }

        [Required]
        public int CountryId { get; set; }

        [MaxLength(500)]
        [Required]
        public string Street { get; set; }

        [Required]
        public int Number { get; set; }
    }
}
