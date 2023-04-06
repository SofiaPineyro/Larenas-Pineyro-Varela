using System;
using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.Domain
{
    public class ArtistBand
    {
        public int ArtistId { get; set; }
        public int MusicalProtagonistId { get; set; }
        public Artist Artist { get; set; }
        public Band Band { get; set; }
        public RoleArtist RoleArtist { get; set; }

        [Required]
        public RoleArtistCode RoleArtistId { get; set; }

        public void ValidArtistBand()
        {
            if (this.RoleArtistId <= 0 )
            {
                throw new ArgumentException("The band must have a role");
            }
        }

    }
}
