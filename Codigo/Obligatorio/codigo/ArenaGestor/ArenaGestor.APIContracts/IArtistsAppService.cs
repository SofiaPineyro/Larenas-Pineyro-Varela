using ArenaGestor.APIContracts.Artist;
using Microsoft.AspNetCore.Mvc;

namespace ArenaGestor.APIContracts
{
    public interface IArtistsAppService
    {
        IActionResult GetArtistById(int artistId);
        IActionResult GetArtists(ArtistGetArtistsDto artistFilter = null);
        IActionResult PostArtist(ArtistInsertArtistDto insertArtist);
        IActionResult PutArtist(ArtistUpdateArtistDto updateArtist);
        IActionResult DeleteArtist(int artistId);
    }
}
