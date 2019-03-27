using System;
using System.Collections.Generic;
using System.Text;

namespace MyRestfulAPI.Infrastucture.Dto.City
{
    public class CityAddOrUpdateDto
    {
        public virtual string Name { get; set; } 
        public virtual string Description { get; set; }
    }
}
