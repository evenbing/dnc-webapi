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
                     .AfterMap((CountryUpdateDto,Country) =>
                     {
                         //Remove

                         //add 

                         //update

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
