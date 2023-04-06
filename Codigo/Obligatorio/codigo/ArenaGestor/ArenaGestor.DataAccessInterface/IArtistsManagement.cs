using ArenaGestor.Domain;
using System;
using System.Collections.Generic;

namespace ArenaGestor.DataAccessInterface
{
    public interface IArtistsManagement
    {
        IEnumerable<Artist> GetArtists(Func<Artist, bool> filter);
        IEnumerable<Artist> GetArtists();
        void InsertArtist(Artist artist);
        Artist GetArtistById(int artistId);
        void DeleteArtist(Artist artist);
        void UpdateArtist(Artist artist);
        void Save();
    }
}
