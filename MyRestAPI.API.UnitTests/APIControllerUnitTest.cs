using System;
using Xunit;
using MyRestfulAPI.API.Controllers;
using MyRestfulAPI.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MyRestfulAPI.Core.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Microsoft.Extensions.Logging;
using MyRestfulAPI.Core.Interfaces;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MyRestfulAPI.Infrastucture.Dto.City;

namespace MyRestAPI.API.UnitTests
{
    public class APIControllerUnitTest
    {

        private MyDbContext GetMyDbContext()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
            var dbContext = new MyDbContext(options);

            if (!dbContext.Countries.Any())
            {
                dbContext.Countries.AddRange(
                      new List<Country>{
                            new Country{
                                EnglishName = "China",
                                ChineseName = "中华人民共和国",
                                Abbreviation = "中国",
                                Cities = new List<City>
                                {
                                    new City{ Name = "北京", Description = "首都"},
                                    new City{ Name = "上海", Description = "魔都" },
                                    new City{ Name = "深圳" },
                                    new City{ Name = "杭州" },
                                    new City{ Name = "天津" }
                                }
                            },
                            new Country{
                                EnglishName = "USA",
                                ChineseName = "美利坚合众国",
                                Abbreviation = "美国",
                                Cities = new List<City>
                                {
                                    new City{ Name = "New York" },
                                    new City{ Name = "Chicago" },
                                    new City{ Name = "San Fransisco" },
                                    new City{ Name = "Los Angeles" },
                                    new City{ Name = "Miami" }
                                }
                            },
                            new Country{
                                EnglishName = "Finland",
                                ChineseName = "芬兰",
                                Abbreviation = "芬兰",
                                Cities = new List<City>
                                {
                                    new City{ Name = "Helsinki" },
                                    new City{ Name = "Espoo" },
                                    new City{ Name = "Tampere" }
                                }
                            },
                            new Country{
                                EnglishName = "UK",
                                ChineseName = "大不列颠及北爱尔兰联合王国",
                                Abbreviation = "英国",
                                Cities = new List<City>
                                {
                                    new City{ Name = "London" },
                                    new City{ Name = "Liverpool" },
                                    new City{ Name = "Manchester" },
                                    new City{ Name = "Birmingham" },
                                    new City{ Name = "Glasgow" }
                                }
                            },
                            new Country{
                                EnglishName = "Denmark",
                                ChineseName = "丹麦",
                                Abbreviation = "丹麦",
                                Cities = new List<City>
                                {
                                    new City{ Name = "Copenhagen " }
                                }
                            },
                            new Country{
                                EnglishName = "Norway",
                                ChineseName = "挪威",
                                Abbreviation = "挪威",
                                Cities = new List<City>
                                {
                                    new City{ Name = "Oslo" }
                                }
                            },
                            new Country{
                                EnglishName = "Sweden",
                                ChineseName = "瑞典",
                                Abbreviation = "瑞典",
                                Cities = new List<City>
                                {
                                    new City{ Name = "Stockholm" }
                                }
                            },
                            new Country{
                                EnglishName = "Germany",
                                ChineseName = "德意志联邦共和国",
                                Abbreviation = "德国",
                                Cities = new List<City>
                                {
                                    new City{ Name = "Berlin" }
                                }
                            },
                            new Country{
                                EnglishName = "Poland",
                                ChineseName = "波兰",
                                Abbreviation = "波兰",
                                Cities = new List<City>
                                {
                                    new City{ Name = "Warsaw" }
                                }
                            },
                            new Country{
                                EnglishName = "Switzerland",
                                ChineseName = "瑞士",
                                Abbreviation = "瑞士",
                                Cities = new List<City>
                                {
                                    new City{ Name = "Bern" }
                                }
                            },
                            new Country{
                                EnglishName = "Austria",
                                ChineseName = "奥地利",
                                Abbreviation = "奥地利",
                                Cities = new List<City>
                                {
                                    new City{ Name = "Vienna" }
                                }
                            }
                     }
                 );
            }
            dbContext.SaveChanges();

            return dbContext;
        }


        [Fact]
        public void Get_ReturnCountry_WithNotParamter()
        {
            
        }

        [Fact]
        public async Task Get_Cities__WithParamter()
        {
            var dbContext = GetMyDbContext();
            var countrryId = dbContext.Countries.SingleOrDefault(x => x.Abbreviation == "中国").Id;
            var countryRepository = new Mock<ICountryRepository>().Object;
            var cityRepository = new Mock<ICityRepository>().Object;
            var mapper = new Mock<IMapper>().Object;
            var unitWork = new Mock<IUnitOfWork>().Object;
            var flag = await countryRepository.CountriesExistAsync(countrryId);
            var controller = new CityController(unitWork, countryRepository, cityRepository, mapper);

            ActionResult<OkObjectResult>  response = await controller.GetCities(countrryId);

            //assert
            response.Result.Should().NotBeNull();
            response.Value.Should().BeOfType<IEnumerable<CityDto>>().Should().NotBeNull();
            
        }

        [Fact]
        public async Task Get_City_WithParamter()
        {

            var dbContext = GetMyDbContext();
            var countryRepository = new Mock<ICountryRepository>().Object;
            var cityRepository = new Mock<ICityRepository>().Object;
            var mapper = new Mock<IMapper>().Object;
            var unitWork = new Mock<IUnitOfWork>().Object;
            var controller = new CityController(unitWork, countryRepository, cityRepository, mapper);

            ActionResult<OkObjectResult> response = await controller.GetCityForCountry(1,1);

            response.Result.Should().NotBeNull();
            response.Value.Should().BeOfType<IEnumerable<CityDto>>().Should().NotBeNull();

        }
    }
}
