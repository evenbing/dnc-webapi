using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyRestfulAPI.Core.Interfaces;
using MyRestfulAPI.Infrastucture.Dto.City;
using MyRestfulAPI.Infrastucture.Dto.Hateoas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRestfulAPI.API.Controllers
{
    [Route("api/[controller]")]
    public class CityController: ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CityController> _logger;
        private readonly ICountryRepository _countryRepository;

        public CityController(IUnitOfWork unitOfWork,
            ICountryRepository countryRepository,
            ICityRepository cityRepository,IMapper mapper,
            ILogger<CityController> logger)
        {
            _unitOfWork = unitOfWork;
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        private LinkCollectionResourceWrapper<CityDto> CreateLinksForCities(LinkCollectionResourceWrapper<CityDto> citieswrapper)
        {
            citieswrapper.Links.Add(new LinkResource(
                  href:"",
                  rel:"self",
                  method:"GET"
                ));
            return citieswrapper;
        }

        public async Task<ActionResult> GetCities(int countryId)
        {
            if (!await _countryRepository.CountriesExistAsync(countryId))
            {
                return NotFound();
            }
            var cities = await _cityRepository.GetCitiesForCountryAsync(countryId);
            if (cities == null)
            {
                return NotFound();
            }
            var citiesDto = _mapper.Map<IEnumerable<CityAddDto>>(cities);
            //var wrapper = new LinkCollectionResourceWrapper<CityDto>(citiesDto);
            return Ok();
        }

        public async Task<ActionResult> GetCityForCountry(int countryId, int cityId)
        {
            if (!await _countryRepository.CountriesExistAsync(countryId))
            {
                _logger.LogInformation("NotFound");
                return NotFound();
            }

            var cityForCountry = await _cityRepository.GetCityCountryAsync(countryId, cityId);
            var cityDto = _mapper.Map<CityDto>(cityForCountry);
            return Ok(cityDto);
        }

        //public async Task<ActionResult> CreateCity(int countryId, [FromBody] CityAddDto city)
        //{
        //    if (city==null)
        //    {
        //        return BadRequest();
        //    }


        //}
    }
}
