using ArenaGestor.Domain;
using System;
using System.Collections.Generic;

namespace ArenaGestor.DataAccessInterface
{
    public interface ISoloistsManagement
    {
        IEnumerable<Soloist> GetSoloists(Func<Soloist, bool> filter);
        IEnumerable<Soloist> GetSoloists();
        void InsertSoloist(Soloist soloist);
        Soloist GetSoloistById(int musicalProtagonistId);
        void DeleteSoloist(Soloist soloist);
        void UpdateSoloist(Soloist soloist);
        void Save();
    }
}
