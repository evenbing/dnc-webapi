using MyRestfulAPI.Infrastucture.Dto.Hateoas;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRestfulAPI.Infrastucture.Dto.City
{
    public class CityDto:LinkResourceBase
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
