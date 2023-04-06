using ArenaGestor.Extensions.DTO;
using System.Collections.Generic;

namespace ArenaGestor.Extensions
{
    public interface IImportExportMethod
    {
        string Name { get; }

        public IEnumerable<ConcertDto> Import(string path);
        public void Export(string path, IEnumerable<ConcertDto> data);
    }
}
