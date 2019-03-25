using MyRestfulAPI.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyRestfulAPI.Core.Interfaces
{
    public interface ICountryRepository
    {
        Task<PaginatedList<Country>> GetCountriesAsync(CountryDtoParamters paramters);
        void AddCountry(Country country);
        Task<Country> GetCountryByIdAsync(int id, bool includeCities = false);
        Task<bool> CountriesExistAsync(int countryId);
        Task<IEnumerable<Country>> GetCountriesAsync(IEnumerable<int> ids);
        void DeleteCountry(Country country);
        void UpdateCountry(Country country);
    }
}
