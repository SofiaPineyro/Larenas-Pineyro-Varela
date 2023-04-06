using Microsoft.AspNetCore.Mvc;

namespace ArenaGestor.APIContracts
{
    public interface IRolesAppService
    {
        IActionResult GetUserRoles();
        IActionResult GetArtistRoles();
    }
}
