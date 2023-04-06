using ArenaGestor.BusinessHelpers;
using ArenaGestor.BusinessInterface;
using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArenaGestor.Business
{
    public class ConcertsService : IConcertsService
    {
        private readonly IConcertsManagement concertManagement;
        private readonly IMusicalProtagonistService musicalProtagonistService;
        private readonly ICountrysService countrysService;
        private readonly ISecurityService securityService;

        public ConcertsService(IConcertsManagement concertManagement, IMusicalProtagonistService musicalProtagonistService,
            ICountrysService countrysService, ISecurityService securityService)
        {
            this.concertManagement = concertManagement;
            this.musicalProtagonistService = musicalProtagonistService;
            this.countrysService = countrysService;
            this.securityService = securityService;
        }

        public Concert GetConcertById(int concertId)
        {
            CommonValidations.ValidId(concertId);
            Concert concert = concertManagement.GetConcertById(concertId);
            if (concert == null)
            {
                throw new NullReferenceException("The concert doesn't exists");
            }
            return concert;
        }

        public IEnumerable<Concert> GetConcerts(ConcertFilter concertFilter = null)
        {
            ValidDateRange(concertFilter);

            List<Concert> result = new List<Concert>();

            if (concertFilter != null && !(string.IsNullOrWhiteSpace(concertFilter.TourName)))
            {
                Func<Concert, bool> filter = new Func<Concert, bool>(x => x.TourName.Trim().ToUpper() == concertFilter.TourName.Trim().ToUpper());
                result = concertManagement.GetConcerts(filter).ToList();
            }
            else
            {
                result = concertManagement.GetConcerts().ToList();
            }

            if (concertFilter != null && concertFilter.DateRange != null)
            {
                Func<Concert, bool> filter = new Func<Concert, bool>(x => x.Date >= concertFilter.DateRange.StartDate && x.Date <= concertFilter.DateRange.EndDate);
                result = result.Where(filter).ToList();
            }

            if (concertFilter != null && concertFilter.Upcoming)
            {
                Func<Concert, bool> filter = new Func<Concert, bool>(x => x.Date >= DateTime.Now);
                result = result.Where(filter).ToList();
            }

            if (result != null && result.Any())
            {
                result = result.OrderBy(x => x.Date).ToList();
            }
            return result;
        }

        public IEnumerable<Concert> GetConcerts(string token, ConcertFilter concertFilter = null)
        {
            var user = securityService.GetUserOfToken(token);

            if (user == null)
            {
                throw new NullReferenceException("User is not logged in");
            }

            if (!securityService.UserHaveRole(RoleCode.Artista, token))
            {
                throw new NullReferenceException("User is not artist");
            }

            var concerts = this.GetConcerts(concertFilter)
                .Where(c => c.Protagonists != null && c.Protagonists.Any() &&
                c.Protagonists.Exists(p => p.Protagonist != null && p.Protagonist.UserComposes(user)));

            return concerts;
        }

        public Concert InsertConcert(Concert concert)
        {
            ValidConcert(concert);

            concertManagement.InsertConcert(concert);
            concertManagement.Save();
            return concert;
        }

        public ConcertsInsertResult InsertConcerts(List<Concert> concerts)
        {
            ConcertsInsertResult result = new ConcertsInsertResult();
            int ConcertNumber = 1;
            foreach (var concert in concerts)
            {
                try
                {
                    InsertConcert(concert);
                    result.InsertedRecords++;
                }
                catch (Exception ex)
                {
                    result.NotInsertedRecords++;
                    result.Messages.Add($"The concert number {ConcertNumber} have the following error: " + ex.Message);
                }
                ConcertNumber++;
            }
            return result;
        }

        public Concert UpdateConcert(Concert concert)
        {
            ValidConcert(concert);

            Concert concertToUpdate = concertManagement.GetConcertById(concert.ConcertId);

            if (concertToUpdate == null)
            {
                throw new NullReferenceException($"The concert with identifier: {concert.ConcertId} doesn't exists.");
            }

            concertManagement.UpdateConcert(concert);
            concertManagement.Save();

            return concert;
        }

        public void DeleteConcert(int concertId)
        {
            CommonValidations.ValidId(concertId);
            Concert concertToDelete = concertManagement.GetConcertById(concertId);

            if (concertToDelete == null)
            {
                throw new NullReferenceException($"The concert with identifier: {concertId} doesn't exists.");
            }
            if (concertToDelete.Tickets != null && concertToDelete.Tickets.Count > 0)
            {
                throw new InvalidOperationException("You cannot delete a concert with tickets attached");
            }

            concertManagement.DeleteConcert(concertToDelete);
            concertManagement.Save();
        }

        private void ValidConcert(Concert concert)
        {
            if (concert == null)
            {
                throw new ArgumentException("You must send the concert");
            }

            concert.ValidConcert();

            foreach (ConcertProtagonist concertProtagonist in concert.Protagonists)
            {
                MusicalProtagonist musicalProtagonist = musicalProtagonistService.GetMusicalProtagonistById(concertProtagonist.MusicalProtagonistId);

                if (musicalProtagonist == null)
                {
                    throw new NullReferenceException($"The musical protagonist with id: {concertProtagonist.MusicalProtagonistId} doesn't exists");
                }
            }

            Country country = countrysService.GetCountryById(concert.Location.CountryId);

            if (country == null)
            {
                throw new NullReferenceException($"The country with id: {concert.Location.CountryId} doesn't exists");
            }

            ValidDateRangeConcert(concert);
        }

        private void ValidDateRangeConcert(Concert concert)
        {
            DateRange dateRange = new DateRange()
            {
                StartDate = concert.Date.Date,
                EndDate = concert.Date.Date.AddDays(1).AddMilliseconds(-1)
            };

            foreach (ConcertProtagonist protagonist in concert.Protagonists)
            {
                var otherConcerts = concertManagement.GetDateRangeConcertsByMusicalProtagonist(dateRange, protagonist.MusicalProtagonistId);

                if (otherConcerts != null && otherConcerts.Any())
                {
                    throw new ArgumentException("Bands or artists can perform one concert per day");
                }
            }
        }

        private void ValidDateRange(ConcertFilter concertFilter)
        {
            if (concertFilter != null && concertFilter.DateRange != null && concertFilter.DateRange.StartDate >= concertFilter.DateRange.EndDate)
            {
                throw new ArgumentException("Start date must be less than end date");
            }
        }

    }
}
