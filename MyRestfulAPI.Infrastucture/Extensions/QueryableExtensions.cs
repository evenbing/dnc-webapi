using MyRestfulAPI.Infrastucture.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;

namespace MyRestfulAPI.Infrastucture.Extensions
{
    /// <summary>
    /// 属性映射-扩展方法
    /// </summary>
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy, IPropertyMapping propertyMapping)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var mappingDictionary = propertyMapping.MappingDictionary;
            if (mappingDictionary == null)
            {
                throw new ArgumentNullException(nameof(mappingDictionary));
            }

            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return source;
            }
            var orderByAfterSplit = orderBy.Split(',');
            foreach (var orderByClause in orderByAfterSplit.Reverse())
            {
                var trimmedOrderByClause = orderByClause.Trim();
                var orderDescending = trimmedOrderByClause.EndsWith(" desc");
                var indexOffirstSpace = trimmedOrderByClause.IndexOf(" ", StringComparison.Ordinal);
                var propertyName = indexOffirstSpace == -1 ? trimmedOrderByClause : trimmedOrderByClause.Remove(indexOffirstSpace);
                if (!mappingDictionary.ContainsKey(propertyName))
                {
                    throw new ArgumentException();
                }

                var mappedProperties = mappingDictionary[propertyName];
                if (mappedProperties==null)
                {
                    throw new ArgumentException();
                }
                mappedProperties.Reverse();
                foreach (var destinationProperty in mappedProperties)
                {
                    if (destinationProperty.Revert)
                    {
                        orderDescending = !orderDescending;
                    }
                    source = source.OrderBy(destinationProperty.Name + (orderDescending ? "descending" : "ascending")); 
                }

            }
            return source;
        }

    //    public static IQueryable<object> ToDynamicQueryable<TSource>(
    //        this IQueryable<TSource> source,string fields,Dictionary<string,List<MappedProperty>> mappingDictionary)
    //    {
    //        if (source == null)
    //        {

    //        }
    //    }
    //}
}
