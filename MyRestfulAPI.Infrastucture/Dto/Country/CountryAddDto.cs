using MyRestfulAPI.Infrastucture.Dto.City;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRestfulAPI.Infrastucture.Dto.Country
{
    public class CountryAddDto
    {
        public CountryAddDto()
        {
            Cities = new List<CityAddDto>();
        }
        public string EnglishName { get; set; }
        public string ChineseName { get; set; }
        public string Abbreviation { get; set; }

        public List<CityAddDto> Cities { get; set; }
    }
}
