using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyRestfulAPI.Core.Interfaces;

namespace MyRestfulAPI.Infrastucture.Services
{
    /// <summary>
    /// 容器代码实现
    /// </summary>
    public class PropertyMappingContainer : IPropertyMappingContainer
    {
        private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();


        public void Register<T>() where T : IPropertyMapping, new()
        {
            _propertyMappings.Add(new T());
        }

        public IPropertyMapping Resolve<TSource, TDestingnation>() where TDestingnation : IEntity
        {
            var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestingnation>>().ToList();
            if (matchingMapping.Count == 1)
            {
                return matchingMapping.First();
            }

            throw new Exception($"cannot find property mapping instance for <{typeof(TSource)},{typeof(TDestingnation)}>");
        }

        public bool ValidMappingExistsFor<TSource, TDestingation>(string fields) where TDestingation : IEntity
        {
            var propertyMapping = Resolve<TSource, TDestingation>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                return false;
            }

            var fieldsAfterSplit = fields.Split(',');
            foreach (var field in fieldsAfterSplit)
            {
                var trimmField = field.Trim();
                var indexOffirstSpace = trimmField.IndexOf(" ", StringComparison.Ordinal);
                var propertyName = indexOffirstSpace == -1 ? trimmField : trimmField.Remove(indexOffirstSpace);
                if (!propertyMapping.MappingDictionary.ContainsKey(propertyName))
                {
                    return false;
                }
            }
            return true;


        }
    }
}
