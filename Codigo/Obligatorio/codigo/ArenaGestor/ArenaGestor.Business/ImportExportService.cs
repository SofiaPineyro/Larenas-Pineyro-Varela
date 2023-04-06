using ArenaGestor.BusinessInterface;
using ArenaGestor.Domain;
using ArenaGestor.Extensions.DTO;
using ArenaGestor.Extensions;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace ArenaGestor.Business
{
    public class ImportExportService : IImportExportService
    {
        private readonly IConcertsService concertsService;
        private readonly IReflectionHelpers reflectionHelpers;
        private readonly IMapper mapper;

        public ImportExportService(IConcertsService concertsService, IReflectionHelpers reflectionHelpers, IMapper mapper)
        {
            this.concertsService = concertsService;
            this.reflectionHelpers = reflectionHelpers;
            this.mapper = mapper;
        }

        public List<string> GetMethods()
        {
            return this.reflectionHelpers.GetMethods();
        }

        public void ExportData(string method, string path)
        {
            IImportExportMethod importExportMethod = this.reflectionHelpers.GetMethod(method);
            if (importExportMethod == null)
            {
                throw new ArgumentException("ImportExport method not found");
            }
            var concerts = concertsService.GetConcerts();
            var resultDto = mapper.Map<IEnumerable<ConcertDto>>(concerts);
            importExportMethod.Export(path, resultDto);
        }


        public ConcertsInsertResult ImportData(string method, string path)
        {
            IImportExportMethod importExportMethod = this.reflectionHelpers.GetMethod(method);
            if (importExportMethod == null)
            {
                throw new ArgumentException("ImportExport method not found");
            }
            var concerts = importExportMethod.Import(path);
            List<Concert> concertsMapped = new List<Concert>();
            foreach (var concert in concerts)
            {
                Concert concertMapped = mapper.Map<Concert>(concert);
                concertsMapped.Add(concertMapped);
            }
            ConcertsInsertResult result = concertsService.InsertConcerts(concertsMapped);
            return result;
        }

    }
}
