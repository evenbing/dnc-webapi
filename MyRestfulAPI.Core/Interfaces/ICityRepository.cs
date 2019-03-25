using MyRestfulAPI.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyRestfulAPI.Core.Interfaces
{
    public interface ICityRepository
    {
        Task<List<City>> GetCitiesForCountryAsync(int countryId);
        Task<City> GetCityCountryAsync(int countryId, int cityId);
        void AddCityForCountry(int countryId, City city);
        void DeleteCity(City city);
        void UpdateCityForCountry(City city);
    }
}
