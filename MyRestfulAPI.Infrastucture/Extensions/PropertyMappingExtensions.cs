using Microsoft.Extensions.DependencyInjection;
using MyRestfulAPI.Infrastucture.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRestfulAPI.Infrastucture.Extensions
{
    /// <summary>
    /// 注册容器
    /// </summary>
    public static class PropertyMappingExtensions
    {
        public static void AddPropertyMappings(this IServiceCollection services)
        {
            var propertyMappingContainer = new PropertyMappingContainer();
            propertyMappingContainer.Register<CountryPropertyMapping>();

            services.AddSingleton<IPropertyMappingContainer>(propertyMappingContainer);
            
        }
    }
}
