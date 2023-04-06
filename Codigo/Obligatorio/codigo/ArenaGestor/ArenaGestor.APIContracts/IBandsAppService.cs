using ArenaGestor.APIContracts.Band;
using Microsoft.AspNetCore.Mvc;

namespace ArenaGestor.APIContracts
{
    public interface IBandsAppService
    {
        IActionResult GetBandById(int bandId);
        IActionResult GetBands(BandGetBandsDto bandFilter = null);
        IActionResult GetBandsByArtist(BandGetArtistsDto artistFilter = null);
        IActionResult PostBand(BandInsertBandDto insertBand);
        IActionResult PutBand(BandUpdateBandDto updateBand);
        IActionResult DeleteBand(int bandId);
    }
}
