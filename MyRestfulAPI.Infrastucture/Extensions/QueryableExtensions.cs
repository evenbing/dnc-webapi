using MyRestfulAPI.Infrastucture.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            }
        }

        public static IQueryable<object> ToDynamicQueryable<TSource>(
            this IQueryable<TSource> source,string fields,Dictionary<string,List<MappedProperty>> mappingDictionary)
        {
            if (source == null)
            {

            }
        }
    }
}
