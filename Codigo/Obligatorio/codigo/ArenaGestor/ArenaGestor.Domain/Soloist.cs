using System;
using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.Domain
{
    public class Soloist : MusicalProtagonist
    {
        public Artist Artist { get; set; }

        [Required]
        public int ArtistId { get; set; }

        [Required]
        public RoleArtist RoleArtist { get; set; }
        [Required]
        public RoleArtistCode RoleArtistId { get; set; }

        public override bool UserComposes(User user)
        {
            if (this.Artist != null && this.Artist.User != null)
            {
                return this.Artist.User.UserId == user.UserId;
            }
            return false;
        }

        public void ValidSoloist()
        {
            if (this.ArtistId == 0)
            {
                throw new ArgumentException("The soloist must have an artist");
            }

            if (this.RoleArtistId <= 0)
            {
                throw new ArgumentException("The soloist must have a role");
            }

            this.ValidMusicalProtagonist();
        }
    }
}
