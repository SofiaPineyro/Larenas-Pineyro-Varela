using System;
using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Soloist
{
    public class SoloistUpdateSoloistDto
    {
        [Required]
        public int MusicalProtagonistId { get; set; }

        [MaxLength(50)]
        [Required]
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
