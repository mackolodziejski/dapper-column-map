using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Dapper;

using DapperColumnMap.Extensions;

namespace DapperColumnMap
{
    public static class ColumnsMapper
    {
        private static readonly Dictionary<Type, List<PropertyToColumnNameMap>> PropertyMapsByType = new Dictionary<Type, List<PropertyToColumnNameMap>>();

        public static void SetColumnsMappingForType<T>()
        {
            var type = typeof(T);
            if (PropertyMapsByType.ContainsKey(type))
            {
                return;
            }

            var properties = type.GetProperties();

            var maps = (from property in properties
                        let columnName = property.GetColumnNameFromDbColumnAttribute()
                        select new PropertyToColumnNameMap(property, columnName))
            .ToList();

            PropertyMapsByType.Add(type, maps);

            var typeMap = new CustomPropertyTypeMap(type, SelectPropertyBasedOnColumnName);
            SqlMapper.SetTypeMap(type, typeMap);
        }

        private static PropertyInfo SelectPropertyBasedOnColumnName(Type type, string columnName)
        {
            if (!PropertyMapsByType.ContainsKey(type))
            {
                return null;
            }

            var maps = PropertyMapsByType[type];
            var map = maps.FirstOrDefault(x => x.ColumnName == columnName);
            return map?.Property;
        }
    }
}
