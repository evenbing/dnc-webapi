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
                                ChineseName = "�л����񹲺͹�",
                                Abbreviation = "�й�",
                                Cities = new List<City>
                                {
                                    new City{ Name = "����", Description = "�׶�"},
                                    new City{ Name = "�Ϻ�", Description = "ħ��" },
                                    new City{ Name = "����" },
                                    new City{ Name = "����" },
                                    new City{ Name = "���" }
                                }
                            },
                            new Country{
                                EnglishName = "USA",
                                ChineseName = "��������ڹ�",
                                Abbreviation = "����",
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
                                ChineseName = "����",
                                Abbreviation = "����",
                                Cities = new List<City>
                                {
                                    new City{ Name = "Helsinki" },
                                    new City{ Name = "Espoo" },
                                    new City{ Name = "Tampere" }
                                }
                            },
                            new Country{
                                EnglishName = "UK",
                                ChineseName = "���е߼�����������������",
                                Abbreviation = "Ӣ��",
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
                                ChineseName = "����",
                                Abbreviation = "����",
                                Cities = new List<City>
                                {
                                    new City{ Name = "Copenhagen " }
                                }
                            },
                            new Country{
                                EnglishName = "Norway",
                                ChineseName = "Ų��",
                                Abbreviation = "Ų��",
                                Cities = new List<City>
                                {
                                    new City{ Name = "Oslo" }
                                }
                            },
                            new Country{
                                EnglishName = "Sweden",
                                ChineseName = "���",
                                Abbreviation = "���",
                                Cities = new List<City>
                                {
                                    new City{ Name = "Stockholm" }
                                }
                            },
                            new Country{
                                EnglishName = "Germany",
                                ChineseName = "����־����͹�",
                                Abbreviation = "�¹�",
                                Cities = new List<City>
                                {
                                    new City{ Name = "Berlin" }
                                }
                            },
                            new Country{
                                EnglishName = "Poland",
                                ChineseName = "����",
                                Abbreviation = "����",
                                Cities = new List<City>
                                {
                                    new City{ Name = "Warsaw" }
                                }
                            },
                            new Country{
                                EnglishName = "Switzerland",
                                ChineseName = "��ʿ",
                                Abbreviation = "��ʿ",
                                Cities = new List<City>
                                {
                                    new City{ Name = "Bern" }
                                }
                            },
                            new Country{
                                EnglishName = "Austria",
                                ChineseName = "�µ���",
                                Abbreviation = "�µ���",
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
            var countrryId = dbContext.Countries.SingleOrDefault(x => x.Abbreviation == "�й�").Id;
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
