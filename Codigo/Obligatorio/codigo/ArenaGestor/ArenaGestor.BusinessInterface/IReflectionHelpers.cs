using ArenaGestor.Extensions;
using System.Collections.Generic;

namespace ArenaGestor.BusinessInterface
{
    public interface IReflectionHelpers
    {
        public List<string> GetMethods();
        public IImportExportMethod GetMethod(string name);
    }
}
