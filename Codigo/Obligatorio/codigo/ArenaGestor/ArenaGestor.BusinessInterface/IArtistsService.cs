using ArenaGestor.Domain;
using System.Collections.Generic;

namespace ArenaGestor.BusinessInterface
{
    public interface IArtistsService
    {
        Artist GetArtistById(int artistId);
        IEnumerable<Artist> GetArtists(Artist artist = null);
        Artist InsertArtist(Artist artist);
        Artist UpdateArtist(Artist artist);
        void DeleteArtist(int artistId);
    }
}
