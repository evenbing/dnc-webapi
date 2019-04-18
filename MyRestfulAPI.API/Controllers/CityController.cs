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
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CityController: ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CityController> _logger;
        private readonly ICountryRepository _countryRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="countryRepository"></param>
        /// <param name="cityRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
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

        #region private methods
        private LinkCollectionResourceWrapper<CityDto> CreateLinksForCities(LinkCollectionResourceWrapper<CityDto> citieswrapper)
        {
            citieswrapper.Links.Add(new LinkResource(
                  href: "",
                  rel: "self",
                  method: "GET"
                ));
            return citieswrapper;
        }

        #endregion



        /// <summary>
        /// 获取指定省份下面城市信息
        /// </summary>
        /// <param name="countryId">省份ID</param>
        /// <returns>城市信息</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet]
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

        /// <summary>
        /// 获取城市信息
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        [HttpGet]
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


    }
}
