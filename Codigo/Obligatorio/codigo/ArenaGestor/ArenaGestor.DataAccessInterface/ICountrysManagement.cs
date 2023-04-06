using ArenaGestor.Domain;
using System.Collections.Generic;

namespace ArenaGestor.DataAccessInterface
{
    public interface ICountrysManagement
    {
        Country GetCountryById(int countryId);
        IEnumerable<Country> GetCountrys();
    }
}
