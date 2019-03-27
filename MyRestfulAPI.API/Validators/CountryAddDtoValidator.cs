using FluentValidation;
using MyRestfulAPI.Infrastucture.Dto.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRestfulAPI.API.Validators
{
    /// <summary>
    /// fluent-validator
    /// </summary>
    public class CountryAddDtoValidator:AbstractValidator<CountryAddDto>
    {
        public CountryAddDtoValidator()
        {
            RuleFor(c => c.EnglishName)
                  .NotEmpty()
                  .WithName("英文名")
                  .WithMessage("{PropertyName}是必填项")
                  .MaximumLength(100).WithMessage("{PropertyName}的长度不可超过100");

            RuleFor(c => c.ChineseName)
                  .NotEmpty()
                  .WithName("中文名")
                  .WithMessage("{PropertyName}是必填项")
                  .MaximumLength(100).WithMessage("{PropertyName}的长度不可能超过{MaxLength}");

            RuleFor(c=>c.Abbreviation)
                  .NotEmpty()
                  .WithName("缩写")
                  .WithMessage("{PropertyNam}是必填项")
                  .MaximumLength(5).WithMessage("{PropertyName}的长度不可能超过{MaxLength}")
        }
    }
}
