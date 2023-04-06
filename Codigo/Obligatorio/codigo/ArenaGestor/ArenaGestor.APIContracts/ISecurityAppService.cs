using ArenaGestor.APIContracts.Security;
using Microsoft.AspNetCore.Mvc;

namespace ArenaGestor.APIContracts
{
    public interface ISecurityAppService
    {
        IActionResult Login(SecurityLoginDto LoginRequest);
        IActionResult Logout(string token);
    }
}
