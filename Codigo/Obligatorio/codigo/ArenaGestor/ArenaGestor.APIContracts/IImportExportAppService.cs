using ArenaGestor.APIContracts.ImportExport;
using Microsoft.AspNetCore.Mvc;

namespace ArenaGestor.APIContracts
{
    public interface IImportExportAppService
    {
        IActionResult GetMethods();
        IActionResult Export(ImportExportDto request);
        IActionResult Import(ImportExportDto request);
    }
}
