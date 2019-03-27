using MyRestfulAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRestfulAPI.Infrastucture.Services
{
    public class PropertyMapping<TSource, TDestination> : IPropertyMapping where TDestination : IEntity
    {
        public Dictionary<string, List<MappedProperty>> MappingDictionary { get; set; }

        protected PropertyMapping(Dictionary<string, List<MappedProperty>> mappingDictionary)
        {
            MappingDictionary = mappingDictionary;
            MappingDictionary[nameof(IEntity.Id)] = new List<MappedProperty>
            {
                new MappedProperty{ Name=nameof(IEntity.Id), Revert =false}
            };
        }

    }
}
