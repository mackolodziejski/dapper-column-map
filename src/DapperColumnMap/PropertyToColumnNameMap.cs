using System.Reflection;

namespace DapperColumnMap
{
    internal sealed class PropertyToColumnNameMap
    {
        internal PropertyToColumnNameMap(PropertyInfo property, string columnName)
        {
            Property = property;
            ColumnName = columnName;
        }

        internal PropertyInfo Property { get; }
        internal string ColumnName { get; }
    }
}
