using ArenaGestor.BusinessHelpers;
using ArenaGestor.BusinessInterface;
using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using System;
using System.Collections.Generic;

namespace ArenaGestor.Business
{
    public class CountrysService : ICountrysService
    {

        private readonly ICountrysManagement countryManagement;
        public CountrysService(ICountrysManagement countrysManagement)
        {
            this.countryManagement = countrysManagement;
        }

        public Country GetCountryById(int countryId)
        {
            CommonValidations.ValidId(countryId);
            Country country = countryManagement.GetCountryById(countryId);
            if (country == null)
            {
                throw new NullReferenceException($"The country {countryId} doesn't exists");
            }

            return country;
        }

        public IEnumerable<Country> GetCountrys()
        {
            return countryManagement.GetCountrys();
        }

    }
}