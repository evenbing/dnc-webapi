using FluentValidation;
using MyRestfulAPI.Infrastucture.Dto.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRestfulAPI.API.Validators
{
    public class CityAddOrUpdateDtoValidator<T>
        :AbstractValidator<T> where T :CityAddOrUpdateDto
    {
        public CityAddOrUpdateDtoValidator()
        {
            RuleFor(c => c.Name)
                 .NotEmpty().WithName("名称")
                 .WithMessage("{PropertyName}是必填项")
                 .MaximumLength(50)
                 .WithMessage("{PropertyName}的长度不能超过{MaxLength}");
            RuleFor(c => c.Description)
                 .MaximumLength(100).WithName("描述")
                 .WithMessage("{PropertyName}的长度不能超过{MaxLength}");
        }
    }
}
