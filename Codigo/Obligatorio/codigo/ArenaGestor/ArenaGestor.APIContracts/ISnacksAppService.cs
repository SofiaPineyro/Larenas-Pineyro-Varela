using ArenaGestor.APIContracts.Snack;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGestor.APIContracts
{
    public interface ISnacksAppService
    {
        IActionResult GetSnacks();
        IActionResult PostSnack(InsertSnackDto insertSnack);
        IActionResult DeleteSnack(int snackId);
    }
}
