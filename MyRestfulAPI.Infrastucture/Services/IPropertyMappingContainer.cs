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
        void Register<T>() where T : IPropertyMapping, new();

    }
}
