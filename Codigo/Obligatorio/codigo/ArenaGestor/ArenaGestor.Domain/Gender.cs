using ArenaGestor.BusinessHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.Domain
{
    public class Gender
    {

        public int GenderId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        public IEnumerable<MusicalProtagonist> MusicalProtagonists { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return obj is Gender other
                && (GenderId == other.GenderId ||
                (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(other.Name) && Name.Trim().ToUpper() == other.Name.Trim().ToUpper()));
        }

        public void ValidGender()
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
