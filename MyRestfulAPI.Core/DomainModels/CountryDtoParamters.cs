using System;
using System.Collections.Generic;
using System.Text;

namespace MyRestfulAPI.Core.DomainModels
{
    public class CountryDtoParamters:PaginationBase
    {
       public string EnglishName { get; set; }
        public string ChineseName { get; set; }
    }
}
