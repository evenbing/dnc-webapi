using MyRestfulAPI.Core.DomainModels;
using MyRestfulAPI.Infrastucture.Dto.Country;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRestfulAPI.Infrastucture.Services
{
    /// <summary>
    /// 映射
    /// </summary>
    public class CountryPropertyMapping : PropertyMapping<CountryDto, Country>
    {
        public CountryPropertyMapping() : base(new Dictionary<string, List<MappedProperty>>(StringComparer.OrdinalIgnoreCase)
        {
            [nameof(CountryDto.EnglishName)] = new List<MappedProperty>
            {
                new MappedProperty{ Name = nameof(Country.EnglishName) ,Revert = false }
            },
            [nameof(CountryDto.ChineseName)] = new List<MappedProperty>
            {
                new MappedProperty{ Name= nameof(Country.ChineseName),Revert =false }
            },
            [nameof(CountryDto.Abbreviation)] = new List<MappedProperty>
            {
                new MappedProperty { Name= nameof(CountryDto.Abbreviation),Revert=false }
            }
        })
        {
            
        }

    }
}
