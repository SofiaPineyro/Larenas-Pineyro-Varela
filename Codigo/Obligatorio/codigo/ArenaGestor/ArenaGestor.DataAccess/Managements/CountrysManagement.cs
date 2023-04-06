using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ArenaGestor.DataAccess.Managements
{
    public class CountrysManagement : ICountrysManagement
    {

        private readonly DbSet<Country> countrys;
        private readonly DbContext context;

        public CountrysManagement(DbContext context)
        {
            this.countrys = context.Set<Country>();
            this.context = context;
        }

        public Country GetCountryById(int countryId)
        {
            return countrys.AsNoTracking().FirstOrDefault(artist => artist.CountryId == countryId);
        }

        public IEnumerable<Country> GetCountrys()
        {
            return countrys.AsNoTracking();
        }

    }
}
