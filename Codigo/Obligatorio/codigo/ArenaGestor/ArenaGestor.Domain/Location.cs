using ArenaGestor.BusinessHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.Domain
{
    public class Location
    {
        public int LocationId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Place { get; set; }

        [Required]
        public Country Country { get; set; }

        [Required]
        public int CountryId { get; set; }

        [MaxLength(500)]
        [Required]
        public string Street { get; set; }

        [Required]
        public int Number { get; set; }

        public void ValidLocation()
        {
            if (!CommonValidations.ValidRequiredString(this.Place))
            {
                throw new ArgumentException("The place must have at least one character");
            }
            if (!CommonValidations.ValidRequiredString(this.Street))
            {
                throw new ArgumentException("The street must have at least one character");
            }
            if (this.Number <= 0)
            {
                throw new ArgumentException("The location number must be higher than 0");
            }
            if (this.CountryId <= 0)
            {
                throw new ArgumentException("The location must have one country");
            }
        }
    }
}
