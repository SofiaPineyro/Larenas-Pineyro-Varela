using ArenaGestor.APIContracts.Soloist;
using Microsoft.AspNetCore.Mvc;

namespace ArenaGestor.APIContracts
{
    public interface ISoloistsAppService
    {
        IActionResult GetSoloistById(int soloistId);
        IActionResult GetSoloists(SoloistGetSoloistsDto soloistFilter = null);
        IActionResult GetSoloistsByArtist(SoloistGetArtistsDto artistFilter = null);
        IActionResult PostSoloist(SoloistInsertSoloistDto insertSoloist);
        IActionResult PutSoloist(SoloistUpdateSoloistDto updateSoloist);
        IActionResult DeleteSoloist(int soloistId);
    }
}
