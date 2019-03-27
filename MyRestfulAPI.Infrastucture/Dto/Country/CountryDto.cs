using MyRestfulAPI.Infrastucture.Dto.City;
using MyRestfulAPI.Infrastucture.Dto.Hateoas;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRestfulAPI.Infrastucture.Dto.Country
{
    public class CountryDto:LinkResourceBase
    {
        public CountryDto()
        {
            Cities = new List<CityDto>();
        }
        
        public int Id { get; set;}
        public string EnglishName { get; set; }
        public string ChineseName { get; set; }
        public string Abbreviation { get; set; }

        public List<CityDto> Cities { get; set; }
    }
}
