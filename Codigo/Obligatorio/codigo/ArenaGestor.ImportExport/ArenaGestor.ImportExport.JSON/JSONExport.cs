using ArenaGestor.Extensions;
using ArenaGestor.Extensions.DTO;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace ArenaGestor.ImportExport.JSON
{
    public class JSONExport : IImportExportMethod
    {
        public string Name { get { return "JSON"; } }

        public void Export(string path, IEnumerable<ConcertDto> data)
        {
            string jsonString = JsonSerializer.Serialize(data);
            File.WriteAllText(path, jsonString);
        }

        IEnumerable<ConcertDto> IImportExportMethod.Import(string path)
        {
            if (File.Exists(path))
            {
                string jsonString = File.ReadAllText(path);
                List<ConcertDto> concerts = JsonSerializer.Deserialize<List<ConcertDto>>(jsonString);
                return concerts;
            }
            else
            {
                throw new ArgumentException("File doesn't exists");
            }
        }
    }
}
