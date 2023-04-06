using ArenaGestor.Domain;
using System.Collections.Generic;

namespace ArenaGestor.BusinessInterface
{
    public interface IMusicalProtagonistService
    {
        IEnumerable<MusicalProtagonist> GetMusicalProtagonist(MusicalProtagonist musicalProtagonist = null);
        MusicalProtagonist GetMusicalProtagonistById(int musicalProtagonistId);
    }
}
