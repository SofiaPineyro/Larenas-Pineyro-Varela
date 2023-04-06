using ArenaGestor.Domain;
using System;
using System.Collections.Generic;

namespace ArenaGestor.DataAccessInterface
{
    public interface IConcertsManagement
    {
        IEnumerable<Concert> GetConcerts(Func<Concert, bool> filter = null);
        IEnumerable<Concert> GetDateRangeConcertsByMusicalProtagonist(DateRange dateRange, int musicalProtagonistId);
        void InsertConcert(Concert concert);
        Concert GetConcertById(int concertId);
        void DeleteConcert(Concert concert);
        void UpdateConcert(Concert concert);
        void Save();
    }
}
