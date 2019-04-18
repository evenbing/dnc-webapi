using MyRestfulAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRestfulAPI.Infrastucture.Services
{
    /// <summary>
    /// 资源映射
    /// </summary>
    public interface IPropertyMappingContainer
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Register<T>() where T : IPropertyMapping, new();

        IPropertyMapping Resolve<TSource, TDestingnation>() where TDestingnation : IEntity;

        bool ValidMappingExistsFor<TSource, TDestingation>(string fields)
            where TDestingation : IEntity;

    }
}
