using Microsoft.EntityFrameworkCore;
using MyRestfulAPI.Core.DomainModels;
using MyRestfulAPI.Core.Interfaces;
using MyRestfulAPI.Infrastucture.Data;
using MyRestfulAPI.Infrastucture.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRestfulAPI.Infrastucture.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly MyContext _myContext;
        private readonly IPropertyMappingContainer _propertyMappingContainer;

        public CountryRepository(MyContext myContext,IPropertyMappingContainer propertyMappingContainer)
        {
            _myContext = myContext;
            _propertyMappingContainer = propertyMappingContainer;
        }

        public void AddCountry(Country country)
        {
            _myContext.Countries.Add(country);
        }

        public async Task<bool> CountriesExistAsync(int countryId)
        {
            return await _myContext.Countries.AnyAsync(x => x.Id == countryId);
        }

        public void DeleteCountry(Country country)
        {
            _myContext.Countries.Remove(country);
        }

        public Task<PaginatedList<Country>> GetCountriesAsync(CountryDtoParamters paramters)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Country>> GetCountriesAsync(IEnumerable<int> ids)
        {
            return await _myContext.Countries.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<Country> GetCountryByIdAsync(int id, bool includeCities = false)
        {
            if (!includeCities)
            {
                return await _myContext.Countries.FindAsync(id);
            }
            return await _myContext.Countries.Include(x => x.Cities).SingleOrDefaultAsync(x => x.Id==id);

        }

        public void UpdateCountry(Country country)
        {
            _myContext.Countries.Update(country);
        }
    }
}
