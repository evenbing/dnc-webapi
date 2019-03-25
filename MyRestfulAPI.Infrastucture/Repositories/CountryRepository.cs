using MyRestfulAPI.Core.DomainModels;
using MyRestfulAPI.Core.Interfaces;
using MyRestfulAPI.Infrastucture.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyRestfulAPI.Infrastucture.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly MyContext _myContext;


        public void AddCountry(Country country)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CountriesExistAsync(int countryId)
        {
            throw new NotImplementedException();
        }

        public void DeleteCountry(Country country)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedList<Country>> GetCountriesAsync(CountryDtoParamters paramters)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Country>> GetCountriesAsync(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<Country> GetCountryByIdAsync(int id, bool includeCities = false)
        {
            throw new NotImplementedException();
        }

        public void UpdateCountry(Country country)
        {
            throw new NotImplementedException();
        }
    }
}
