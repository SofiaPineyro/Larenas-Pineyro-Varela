using System;
using System.Collections.Generic;

namespace ArenaGestor.APIContracts.Band
{
    public class BandResultBandDto
    {
        public int MusicalProtagonistId { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public BandResultGenderDto Gender { get; set; }

        public List<BandResultArtistDto> Artists { get; set; }

        public List<BandResultConcertDto> Concerts { get; set; }
    }
}
