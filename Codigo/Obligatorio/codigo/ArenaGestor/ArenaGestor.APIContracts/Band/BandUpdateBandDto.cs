using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Band
{
    public class BandUpdateBandDto
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
        public List<BandUpdateArtistDto> Artists { get; set; }
    }
}
