using System.Reflection;

using DapperColumnMap.Attributes;

namespace DapperColumnMap.Extensions
{
    internal static class PropertyInfoExtensions
    {
        internal static string GetColumnNameFromDbColumnAttribute(this PropertyInfo property)
        {
            var attribute = property.GetCustomAttribute(typeof(DbColumnAttribute), false);
            if (attribute is DbColumnAttribute columnAttribute)
            {
                return columnAttribute.ColumnName;
            }

            return property.Name;
        }
    }
}
