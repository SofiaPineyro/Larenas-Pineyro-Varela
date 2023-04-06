using ArenaGestor.Domain;
using System;
using System.Collections.Generic;

namespace ArenaGestor.DataAccessInterface
{
    public interface IBandsManagement
    {
        IEnumerable<Band> GetBands(Func<Band, bool> filter);
        IEnumerable<Band> GetBands();
        void InsertBand(Band band);
        Band GetBandById(int musicalProtagonistId);
        void DeleteBand(Band band);
        void UpdateBand(Band band);
        void Save();
    }
}
