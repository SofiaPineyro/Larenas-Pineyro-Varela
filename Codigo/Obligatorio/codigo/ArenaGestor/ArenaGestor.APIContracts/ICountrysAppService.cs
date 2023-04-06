using Microsoft.AspNetCore.Mvc;

namespace ArenaGestor.APIContracts
{
    public interface ICountrysAppService
    {
        IActionResult GetCountrys();
    }
}
