using MyRestfulAPI.Infrastucture.Dto.City;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRestfulAPI.Infrastucture.Dto.Country
{
    public class CountryUpdateDto
    {
        public CountryUpdateDto()
        {
            Cities = new List<CityDto>();
        }

        public string EnglishName { get; set; }

        public string ChineseName { get; set; }

        public string Abbreviation { get; set; }

        public List<CityDto> Cities { get; set; }
    }
}
