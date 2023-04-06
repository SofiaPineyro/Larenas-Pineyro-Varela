using ArenaGestor.APIContracts.Concert;
using Microsoft.AspNetCore.Mvc;

namespace ArenaGestor.APIContracts
{
    public interface IConcertsAppService
    {
        IActionResult GetConcertById(int concertId);
        IActionResult GetConcerts(ConcertGetConcertsDto concertFilter = null);
        IActionResult GetConcertsByArtist(ConcertGetConcertsDto concertFilter = null);
        IActionResult PostConcert(ConcertInsertConcertDto insertConcert);
        IActionResult PutConcert(ConcertUpdateConcertDto updateConcert);
        IActionResult DeleteConcert(int concertId);
    }
}
