using ArenaGestor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGestor.DataAccessInterface
{
    public interface ISnacksManagement
    {
        IEnumerable<Snack> GetSnacks(Func<Snack, bool> filter);
        void InsertSnack(Snack snack);
        void DeleteSnack(Snack snack);
        bool ExistsSnack(Func<Snack, bool> filter);
        void Save();
    }
}
