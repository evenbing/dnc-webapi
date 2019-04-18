using MyRestfulAPI.Core.DomainModels;
using MyRestfulAPI.Core.Interfaces;
using MyRestfulAPI.Infrastucture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyRestfulAPI.Infrastucture.Repositories
{
    public class CityRepository : ICityRepository
    {
        public readonly MyContext _myContext;

        public CityRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public void AddCityForCountry(int countryId, City city)
        {
            city.CountryId = countryId;
            _myContext.Cities.Add(city);
        }

        public void DeleteCity(City city)
        {
            _myContext.Cities.Remove(city);
        }

        public async Task<List<City>> GetCitiesForCountryAsync(int countryId)
        {
            return await _myContext.Cities.Where(x => x.CountryId == countryId).ToListAsync();
        }

        public async Task<City> GetCityCountryAsync(int countryId, int cityId)
        {
            return await _myContext.Cities.SingleOrDefaultAsync(x => x.CountryId == countryId && x.Id == cityId);
        }

        public void UpdateCityForCountry(City city)
        {
            _myContext.Update(city);
        }
    }
}
