using System;
using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Soloist
{
    public class SoloistInsertSoloistDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public int GenderId { get; set; }

        [Required]
        public int ArtistId { get; set; }

        [Required]
        public int RoleArtistId { get; set; }
    }
}
