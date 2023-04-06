using ArenaGestor.Domain;
using System;
using System.Collections.Generic;

namespace ArenaGestor.DataAccessInterface
{
    public interface IMusicalProtagonistManagement
    {
        IEnumerable<MusicalProtagonist> GetMusicalProtagonist(Func<MusicalProtagonist, bool> filter);
        IEnumerable<MusicalProtagonist> GetMusicalProtagonist();
        MusicalProtagonist GetMusicalProtagonistById(int musicalProtagonistId);
    }
}
