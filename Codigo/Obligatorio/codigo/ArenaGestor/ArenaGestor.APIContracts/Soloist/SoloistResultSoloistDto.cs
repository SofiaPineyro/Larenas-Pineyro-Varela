using System;
using System.Collections.Generic;

namespace ArenaGestor.APIContracts.Soloist
{
    public class SoloistResultSoloistDto
    {
        public int MusicalProtagonistId { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public SoloistResultGenderDto Gender { get; set; }

        public SoloistResultArtistDto Artist { get; set; }

        public List<SoloistResultConcertDto> Concerts { get; set; }

        public SoloistResultRoleArtistDto RoleArtist { get; set; }

    }
}
