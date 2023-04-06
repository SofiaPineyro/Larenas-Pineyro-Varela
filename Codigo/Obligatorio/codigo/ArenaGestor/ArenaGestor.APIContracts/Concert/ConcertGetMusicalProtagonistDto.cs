using System.ComponentModel.DataAnnotations;

namespace ArenaGestor.APIContracts.Concert
{
    public class ConcertGetMusicalProtagonistDto
    {
        public int MusicalProtagonistId { get; set; }
        public string Name { get; set; }
    }
}
