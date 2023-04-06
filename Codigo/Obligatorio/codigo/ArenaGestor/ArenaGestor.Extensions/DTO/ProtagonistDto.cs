using System;

namespace ArenaGestor.Extensions.DTO
{
    public class ProtagonistDto
    {
        public int MusicalProtagonistId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public GenderDto Gender { get; set; }
        public int GenderId { get; set; }
    }
}
