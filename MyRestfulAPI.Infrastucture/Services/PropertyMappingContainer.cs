using System;
using System.Collections.Generic;
using System.Text;

namespace MyRestfulAPI.Infrastucture.Services
{
    public class PropertyMappingContainer : IPropertyMappingContainer
    {
        private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public void Register<T>() where T : IPropertyMapping, new()
        {
            _propertyMappings.Add(new T());
        }

    }
}
