using ArenaGestor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGestor.BusinessInterface
{
    public interface ISnacksService
    {
        IEnumerable<Snack> GetSnacks(Snack snack = null);
        Snack InsertSnack(Snack snack);
        void DeleteSnack(int snackId);
    }
}
