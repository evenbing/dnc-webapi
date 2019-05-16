using AutoMapper;
using MyRestfulAPI.Core.DomainModels;
using MyRestfulAPI.Infrastucture.Dto.City;
using MyRestfulAPI.Infrastucture.Dto.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRestfulAPI.API.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();

            CreateMap<CountryAddDto, Country>();

            CreateMap<CountryUpdateDto, Country>()
                     .ForMember(c => c.Cities, opt => opt.Ignore())
                     .AfterMap((countryUpdateDto,country) =>
                     {
                         //Remove
                         var countryUpdateCityIds = countryUpdateDto.Cities.Select(x => x.Id).ToList();
                         var removeCities = country.Cities.Where(x => !countryUpdateCityIds.Contains(x.Id)).ToList();
                         foreach (var city in removeCities)
                         {
                             country.Cities.Remove(city); 
                         }
                         //add 
                         var addedCitiesDto = countryUpdateDto.Cities.Where(x => x.Id == 0);
                         var addedCities = Mapper.Map<IEnumerable<City>>(addedCitiesDto);
                         foreach (var city in addedCities)
                         {
                             country.Cities.Add(city);
                         }
                         //update
                         var updateCities = country.Cities.Where(x => x.Id != 0).ToList();
                         foreach (var city in updateCities)
                         {
                             var cityDto = countryUpdateDto.Cities.Single(x => x.Id == city.Id);
                             Mapper.Map(cityDto,city);
                         }
                     });


            //City
            CreateMap<City, CityDto>();
            CreateMap<CityDto, City>();
            CreateMap<CityAddDto, City>();
            CreateMap<CityUpdateDto, City>();
            CreateMap<City, CityUpdateDto>();
                     
        }
    }
}
