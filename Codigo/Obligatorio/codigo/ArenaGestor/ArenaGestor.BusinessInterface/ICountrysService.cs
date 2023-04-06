using ArenaGestor.Domain;
using System.Collections.Generic;

namespace ArenaGestor.BusinessInterface
{
    public interface ICountrysService
    {
        Country GetCountryById(int countryId);
        IEnumerable<Country> GetCountrys();

    }
}
