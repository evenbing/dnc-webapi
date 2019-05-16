using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyRestfulAPI.Core.DomainModels;
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
    /// city控制器
    /// </summary>
    [Route("api/countries/{countryId}/cities")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        // private readonly ILogger<CityController> _logger;
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
            ICityRepository cityRepository, IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
            //_logger = logger;
        }

        #region private methods
        //private LinkCollectionResourceWrapper<CityDto> createLinksForCities()
        //{
        //    LinkCollectionResourceWrapper<CityDto>
        //}
        #endregion



        /// <summary>
        /// 获取指定国家下面城市信息
        /// </summary>
        /// <param name="countryId">国家Id</param>
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
            var citiesDto = _mapper.Map<IEnumerable<CityDto>>(cities);
            //var wrapper = new LinkCollectionResourceWrapper<CityDto>(citiesDto);
            //return Ok();
            return Ok(citiesDto);
        }

        /// <summary>
        /// 获取城市信息
        /// </summary>
        /// <param name="countryId">国家Id</param>
        /// <param name="cityId">城市id</param>
        /// <returns></returns>
        [HttpGet("{cityId}")]
        public async Task<ActionResult> GetCityForCountry(int countryId, int cityId)
        {
            if (!await _countryRepository.CountriesExistAsync(countryId))
            {
                //_logger.LogInformation("NotFound");
                return NotFound();
            }

            var cityForCountry = await _cityRepository.GetCityCountryAsync(countryId, cityId);
            var cityDto = _mapper.Map<CityDto>(cityForCountry);
            return Ok(cityDto);
        }

        /// <summary>
        /// 创建城市
        /// </summary>
        /// <param name="countryId">国家Id</param>
        /// <param name="city">城市信息</param>
        /// <returns>状态码201</returns>
        [HttpPost]
        public async Task<IActionResult> CreateCityForCountry(int countryId, [FromBody] CityDto city)
        {
            if (city ==null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            if (!await _countryRepository.CountriesExistAsync(countryId))
            {
                return NotFound();
            }

            var cityModel = _mapper.Map<City>(city);
            _cityRepository.AddCityForCountry(countryId, cityModel);
            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception("Error occurred");
            }
            var cityDto = _mapper.Map<CityDto>(cityModel);
            return CreatedAtRoute("GetCityForCountry", new { countryId, cityId = cityModel.Id });
        }

        /// <summary>
        /// 删除指定id城市数据
        /// </summary>
        /// <param name="countryId">国家Id</param>
        /// <param name="cityId">城市Id</param>
        /// <returns>状态码204</returns>
        [HttpDelete(Name ="DeleteCityForCountry")]
        public async Task<IActionResult> DeleteCityForCountry(int countryId, int cityId)
        {
            if (!await _countryRepository.CountriesExistAsync(countryId))
            {
                return NotFound();
            }

            var city = await _cityRepository.GetCityCountryAsync(countryId, cityId);
            if (city == null)
            {
                return NotFound();
            }

            _cityRepository.DeleteCity(city);

            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception($"Deleting city {cityId} for country {countryId} failed when saving.");
            }

            return NotFound();
        }


        /// <summary>
        /// 局部更新city
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="cityId"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> PatchCityForCountry(int countryId, int cityId,[FromBody]JsonPatchDocument<CityUpdateDto> patchDocument)
        {
            if (patchDocument==null)
            {
                return BadRequest();
            }
            if (!await _countryRepository.CountriesExistAsync(countryId))
            {
                return NotFound();
            }

            var city = await _cityRepository.GetCityCountryAsync(countryId,cityId);
            if (city==null)
            {
                return NotFound();
            }
            var cityToPatch = _mapper.Map<CityUpdateDto>(city);
            patchDocument.ApplyTo(cityToPatch);
            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            _mapper.Map(cityToPatch, city);


            return NoContent();
        }
    }
}
