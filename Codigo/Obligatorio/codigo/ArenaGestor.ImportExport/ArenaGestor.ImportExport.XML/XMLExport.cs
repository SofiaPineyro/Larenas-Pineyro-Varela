using ArenaGestor.Extensions;
using ArenaGestor.Extensions.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ArenaGestor.ImportExport.XML
{
    public class XMLImportExport : IImportExportMethod
    {
        public string Name { get { return "XML"; } }

        public void Export(string path, IEnumerable<ConcertDto> data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ConcertDto>), new XmlRootAttribute("concert_list"));

            StringWriter textWriter = new StringWriter();
            serializer.Serialize(textWriter, data);

            File.WriteAllText(path, textWriter.ToString());
        }

        IEnumerable<ConcertDto> IImportExportMethod.Import(string path)
        {
            if (File.Exists(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<ConcertDto>), new XmlRootAttribute("concert_list"));

                StreamReader reader = new StreamReader(path);

                List<ConcertDto> concerts = (List<ConcertDto>)serializer.Deserialize(reader);

                return concerts;
            }
            else
            {
                throw new ArgumentException("File doesn't exists");
            }
        }
    }
}
