using ArenaGestor.BusinessHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.Domain
{
    public class Concert
    {
        public int ConcertId { get; set; }
        [MaxLength(50)]
        [Required]
        public string TourName { get; set; }
        public DateTime Date { get; set; }
        public int TicketCount { get; set; }
        public double Price { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public List<ConcertProtagonist> Protagonists { get; set; }
        public Location Location { get; set; }

        [Required]
        public int LocationId { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Concert other
                && (ConcertId == other.ConcertId ||
                (!string.IsNullOrEmpty(TourName) && !string.IsNullOrEmpty(other.TourName) && TourName.Trim().ToUpper() == other.TourName.Trim().ToUpper()));
        }

        public void ValidConcert()
        {
            if (!CommonValidations.ValidRequiredString(this.TourName))
            {
                throw new ArgumentException("The name must have at least one character");
            }
            if (this.TourName.Length > 50)
            {
                throw new ArgumentException("Name must be less than 50 characters");
            }
            if (this.Date <= DateTime.Now)
            {
                throw new ArgumentException("The concert must start in the future");
            }
            if (this.Price <= 0)
            {
                throw new ArgumentException("The concert price must be higher than 0");
            }
            if (this.TicketCount <= 0)
            {
                throw new ArgumentException("The concert ticket count must be higher than 0");
            }
            if (this.Protagonists.Count <= 0)
            {
                throw new ArgumentException("The concert must have at least one protagonist");
            }
            if (this.Location == null)
            {
                throw new ArgumentException("The concert must have one location");
            }

            Location.ValidLocation();
        }

    }
}
