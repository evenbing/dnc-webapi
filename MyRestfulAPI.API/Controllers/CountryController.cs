using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyRestfulAPI.Core.DomainModels;
using MyRestfulAPI.Core.Interfaces;
using MyRestfulAPI.Infrastucture.Dto.Country;
using MyRestfulAPI.Infrastucture.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRestfulAPI.API.Controllers
{
    /// <summary>
    /// 国家控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController:ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        private readonly IUrlHelper _urlHelper;
        private readonly IPropertyMappingContainer _propertyMappingContainer;
        private readonly ITypeHelperService _typeHelperService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="countryRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="urlHelper"></param>
        /// <param name="propertyMappingContainer"></param>
        /// <param name="typeHelperService"></param>
        public CountryController(IUnitOfWork unitOfWork,
            ICountryRepository countryRepository,
            IMapper mapper,
            IUrlHelper urlHelper,
            IPropertyMappingContainer propertyMappingContainer,
            ITypeHelperService typeHelperService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _urlHelper = urlHelper;
            _countryRepository = countryRepository;
            _propertyMappingContainer = propertyMappingContainer;
            _typeHelperService = typeHelperService;

        }

        /// <summary>
        /// 获取省份数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            return null;
        }

        /// <summary>
        /// 获取指定ID的国家数据
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="fields">请求的字段</param>
        /// <param name="includeCities">获取下一级别的数据</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCountry(int id,string fields= null,bool includeCities =false)
        {
            if (!_typeHelperService.TypeHasProperties<CountryDto>(fields))
            {
                return BadRequest("Can't find the fields on CountryDto");
            }
            var country = await _countryRepository.GetCountryByIdAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            var countryDto = _mapper.Map<CountryDto>(country);

            return Ok(countryDto);
        }



        /// <summary>
        /// 创建国家数据
        /// </summary>
        /// <returns></returns>
        [HttpPost(Name = "create")]
        public async Task<IActionResult> CreateCountry([FromBody] CountryAddDto countryDto)
        {
            if (countryDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var country = _mapper.Map<Country>(countryDto);

            _countryRepository.AddCountry(country);
            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception("Error occured when adding");
            }

            var countryAddDto = Mapper.Map<CountryDto>(country);
            //hateos

            return CreatedAtRoute("GetCountry",new { },countryAddDto);
        }


        /// <summary>
        /// 删除国家数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countryRepository.GetCountryByIdAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            _countryRepository.DeleteCountry(country);

            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception($"Deleting country {id} failed when saving.");
            }
            return NoContent();
        }

        /// <summary>
        /// 更新国家数据
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="countryDto">dto</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] CountryUpdateDto countryDto)
        {
            if (countryDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }
            var country = await _countryRepository.GetCountryByIdAsync(id, includeCities: true);
            if (country == null)
            {
                return NotFound();
            }

            _mapper.Map(countryDto, country);
            _countryRepository.UpdateCountry(country);
            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception($"Updating country {id} failed when saving.");
            }
            return NoContent();
        }

    }
}
