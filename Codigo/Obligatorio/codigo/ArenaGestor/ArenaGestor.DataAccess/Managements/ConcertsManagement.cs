using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArenaGestor.DataAccess.Managements
{
    public class ConcertsManagement : IConcertsManagement
    {
        private readonly DbSet<Concert> concerts;
        private readonly DbContext context;

        public ConcertsManagement(DbContext context)
        {
            this.concerts = context.Set<Concert>();
            this.context = context;
        }

        public Concert GetConcertById(int concertId)
        {
            Func<Concert, bool> filter = new Func<Concert, bool>(concert => concert.ConcertId == concertId);
            return GetConcerts(filter).FirstOrDefault();
        }

        public IEnumerable<Concert> GetConcerts(Func<Concert, bool> filter = null)
        {
            if (filter == null)
            {
                filter = new Func<Concert, bool>(concert => true);
            }
            return concerts.Include(x => x.Tickets)
                .Include(x => x.Location).ThenInclude(x => x.Country)
                .Include(x => x.Protagonists).ThenInclude(x => x.Protagonist).ThenInclude(x => x.Gender)
                .Include(x => x.Protagonists).ThenInclude(x => x.Protagonist).ThenInclude(x => (x as Band).Artists)
                .Include(x => x.Protagonists).ThenInclude(x => x.Protagonist).ThenInclude(x => (x as Soloist).Artist)
                .Include(x => x.Protagonists).ThenInclude(x => x.Protagonist).ThenInclude(x => (x as Band).Artists).ThenInclude(x => x.Artist)
                .Include(x => x.Protagonists).ThenInclude(x => x.Protagonist).ThenInclude(x => (x as Soloist).Artist).ThenInclude(x => x.User)
                .Include(x => x.Protagonists).ThenInclude(x => x.Protagonist).ThenInclude(x => (x as Band).Artists).ThenInclude(x => x.Artist).ThenInclude(x => x.User)
                .AsNoTracking().Where(filter);
        }

        public IEnumerable<Concert> GetDateRangeConcertsByMusicalProtagonist(DateRange dateRange, int musicalProtagonistId)
        {
            return concerts.Where(x => x.Protagonists.Select(x => x.MusicalProtagonistId)
                                                    .Contains(musicalProtagonistId) &&
                                                    x.Date >= dateRange.StartDate &&
                                                    x.Date <= dateRange.EndDate).AsNoTracking();
        }

        public void InsertConcert(Concert concert)
        {
            concert.ConcertId = 0;
            foreach (ConcertProtagonist protagonist in concert.Protagonists)
            {
                protagonist.ConcertId = 0;
                if (protagonist.Protagonist != null)
                {
                    context.Entry<MusicalProtagonist>(protagonist.Protagonist).State = EntityState.Unchanged;
                }
            }

            concerts.Add(concert);
        }

        public void UpdateConcert(Concert concert)
        {
            foreach (ConcertProtagonist protagonist in concert.Protagonists)
            {
                protagonist.ConcertId = concert.ConcertId;
                context.Entry<ConcertProtagonist>(protagonist).State = EntityState.Deleted;
            }
            if (concert != null && concert.Location != null && concert.Location.Country != null)
            {
                context.Attach<Country>(concert.Location.Country);
            }

            concerts.Update(concert);
        }

        public void DeleteConcert(Concert concert)
        {
            concert.Protagonists = null;
            concerts.Remove(concert);
        }

        public void Save()
        {

            context.SaveChanges();
        }

    }
}
