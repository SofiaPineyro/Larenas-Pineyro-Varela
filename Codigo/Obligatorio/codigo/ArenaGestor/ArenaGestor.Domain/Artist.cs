using ArenaGestor.BusinessHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.Domain
{
    public class Artist
    {
        public int ArtistId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public User User { get; set; }

        public int? UserId { get; set; }

        public virtual ICollection<ArtistBand> Bands { get; set; }

        public virtual ICollection<Soloist> Soloists { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public Artist()
        {
            this.Bands = new HashSet<ArtistBand>();
            this.Soloists = new HashSet<Soloist>();
        }

        public override bool Equals(object obj)
        {
            return obj is Artist other
                && (ArtistId == other.ArtistId ||
                (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(other.Name) && Name.Trim().ToUpper() == other.Name.Trim().ToUpper()));
        }

        public void ValidArtist()
        {
            if (!CommonValidations.ValidRequiredString(this.Name))
            {
                throw new ArgumentException("The name must have at least one character");
            }
            if (this.Name.Length > 50)
            {
                throw new ArgumentException("Name must be less than 50 characters");
            }
        }

    }
}
