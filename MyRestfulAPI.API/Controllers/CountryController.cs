using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyRestfulAPI.Core.Interfaces;
using MyRestfulAPI.Infrastucture.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRestfulAPI.API.Controllers
{
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
        /// 获取指定ID的省份数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields"></param>
        /// <param name="includeCities"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetCountry(int id,string fields= null,bool includeCities =false)
        {
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost(Name ="create")]
        public async Task<IActionResult> CreateCountry()
        {
            return null;
        }
    }
}
