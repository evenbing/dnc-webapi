using FluentValidation;
using MyRestfulAPI.Infrastucture.Dto.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRestfulAPI.API.Validators
{
    public class CityUpdateDtoValidator
         :CityAddOrUpdateDtoValidator<CityUpdateDto>
    {
        public CityUpdateDtoValidator()
        {
            RuleFor(c => c.Description).NotEmpty()
                .WithMessage("描述")
                .WithMessage("{propertyName}是必填项");
        }
    }
}
