using ArenaGestor.Domain;
using System.Collections.Generic;

namespace ArenaGestor.BusinessInterface
{
    public interface IBandsService
    {
        Band GetBandById(int bandId);
        IEnumerable<Band> GetBands(Band band = null);
        IEnumerable<Band> GetBandsByArtist(Artist artist = null);
        Band InsertBand(Band band);
        Band UpdateBand(Band band);
        void DeleteBand(int bandId);
    }
}
