using ArenaGestor.Domain;
using System.Collections.Generic;

namespace ArenaGestor.BusinessInterface
{
    public interface IImportExportService
    {
        public List<string> GetMethods();
        public ConcertsInsertResult ImportData(string method, string path);
        public void ExportData(string method, string path);
    }
}
