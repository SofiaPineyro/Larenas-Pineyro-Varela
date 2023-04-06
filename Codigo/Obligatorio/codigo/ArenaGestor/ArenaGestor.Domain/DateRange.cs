using System;
using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.Domain
{
    public class DateRange
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

    }
}
