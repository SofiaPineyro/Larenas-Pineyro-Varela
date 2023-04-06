using ArenaGestor.Domain;
using System.Collections.Generic;

namespace ArenaGestor.BusinessInterface
{
    public interface ISoloistsService
    {
        Soloist GetSoloistById(int soloistId);
        IEnumerable<Soloist> GetSoloists(Soloist soloist = null);
        IEnumerable<Soloist> GetSoloistsByArtist(Artist artist = null);
        Soloist InsertSoloist(Soloist soloist);
        Soloist UpdateSoloist(Soloist soloist);
        void DeleteSoloist(int soloistId);
    }
}
