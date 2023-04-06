using ArenaGestor.BusinessHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.Domain
{
    public abstract class MusicalProtagonist
    {
        public int MusicalProtagonistId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public Gender Gender { get; set; }
        [Required]
        public int GenderId { get; set; }
        public List<ConcertProtagonist> Concerts { get; set; }

        public abstract bool UserComposes(User user);

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return obj is MusicalProtagonist other
                && (MusicalProtagonistId == other.MusicalProtagonistId ||
                (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(other.Name) && Name.Trim().ToUpper() == other.Name.Trim().ToUpper()));
        }

        public void ValidMusicalProtagonist()
        {
            ValidStartDate();
            ValidGender();
            ValidName();
        }

        private void ValidStartDate()
        {
            if (this.StartDate > DateTime.Now)
            {
                throw new ArgumentException("Start date must be less than now");
            }
            if (this.StartDate < DateTime.Now.AddYears(-50))
            {
                throw new ArgumentException("Start date must be greater than 50 years ago");
            }
        }

        private void ValidGender()
        {
            if (this.GenderId == 0)
            {
                throw new ArgumentException("You must have at least a gender");
            }
        }

        private void ValidName()
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
