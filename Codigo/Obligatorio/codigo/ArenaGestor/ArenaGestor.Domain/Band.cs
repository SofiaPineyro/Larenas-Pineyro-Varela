using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ArenaGestor.Domain
{
    public class Band : MusicalProtagonist
    {
        [Required]
        public List<ArtistBand> Artists { get; set; }

        public override bool UserComposes(User user)
        {
            if (this.Artists != null && this.Artists.Count > 0)
            {
                return this.Artists.Exists(a => a.Artist.User != null && a.Artist.User.UserId == user.UserId);
            }
            return false;
        }

        public void ValidBand()
        {
            if (this.Artists == null || this.Artists.Count == 0)
            {
                throw new ArgumentException("The band must have at least one artist");
            }

            foreach (var artist in this.Artists)
            {
                artist.ValidArtistBand();
            }

            this.ValidMusicalProtagonist();
        }

    }
}
