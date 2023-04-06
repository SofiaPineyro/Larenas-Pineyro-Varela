using System;
using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.Domain
{
    public class Country
    {
        public int CountryId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Country other
                && (CountryId == other.CountryId ||
                (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(other.Name) && Name.Trim().ToUpper() == other.Name.Trim().ToUpper()));
        }

    }
}
