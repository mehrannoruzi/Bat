using Dapper;
using Bat.Core;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bat.Dapper
{
    public static class ParameterExtension
    {
        /// <summary>
        /// This extension converts an enumerable set to a Dapper TVP
        /// </summary>
        /// <typeparam name="T">type of enumerbale</typeparam>
        /// <param name="parameter">list of values</param>
        /// <param name="typeName">database type name</param>
        /// <returns>a custom query parameter</returns>
        public static SqlMapper.ICustomQueryParameter ToTableValuedParameter<T>
            (this List<T> parameter, string typeName)
        {
            var dataTable = new DataTable();
            var assemblyName = typeof(T).Assembly.FullName.Split(',')[0];
            var fields = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => x.CanRead && x.CanWrite
                    && x.GetCustomAttribute(typeof(NotMappedAttribute)) == null
                    && x.GetCustomAttribute(typeof(ForeignKeyAttribute)) == null
                    && (x.PropertyType.FullName.StartsWith("Bat.") ||
                    x.PropertyType.FullName.StartsWith(assemblyName) ||
                    (x.PropertyType.FullName.StartsWith("System.")
                    && (!x.PropertyType.FullName.Contains("System.Collection"))))).ToArray();

            foreach (var field in fields)
            {
                if (field.PropertyType.IsEnum)
                    if (field.PropertyType.IsInheritFrom(typeof(byte))) dataTable.Columns.Add(field.Name, typeof(byte));
                    else dataTable.Columns.Add(field.Name, typeof(int));
                else
                    dataTable.Columns.Add(field.Name, field.PropertyType);
            }

            foreach (T obj in parameter)
                dataTable.Rows.Add(fields.Select(x => x.GetValue(obj, null)).ToArray());

            return dataTable.AsTableValuedParameter(typeName);
        }

        /// <summary>
        /// This extension converts an enumerable set to a Dapper TVP
        /// </summary>
        /// <typeparam name="T">type of enumerbale</typeparam>
        /// <param name="parameter">list of values</param>
        /// <param name="typeName">database type name</param>
        /// <param name="columnNames">if more than one column in a TVP, 
        /// columns order must mtach order of columns in TVP</param>
        /// <returns>a custom query parameter</returns>
        public static SqlMapper.ICustomQueryParameter ToTableValuedParameter<T>
            (this T parameter, string typeName, string columnNames = null)
        {
            var dataTable = new DataTable();
            if (typeof(T).IsValueType)// || typeof(T).FullName.Equals("System.String"))
            {
                dataTable.Columns.Add(columnNames == null ? "NONAME" : columnNames, typeof(T));
                dataTable.Rows.Add(parameter);
            }
            else
            {
                var assemblyName = typeof(T).Assembly.FullName.Split(',')[0];
                var fields = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => x.CanRead && x.CanWrite
                    && x.GetCustomAttribute(typeof(NotMappedAttribute)) == null
                    && x.GetCustomAttribute(typeof(ForeignKeyAttribute)) == null
                    && (x.PropertyType.FullName.StartsWith("Bat.") ||
                    x.PropertyType.FullName.StartsWith(assemblyName) ||
                    (x.PropertyType.FullName.StartsWith("System.")
                    && (!x.PropertyType.FullName.Contains("System.Collection"))))).ToArray();

                foreach (var field in fields)
                {
                    if (field.PropertyType.IsEnum)
                        if (field.PropertyType.IsInheritFrom(typeof(byte))) dataTable.Columns.Add(field.Name, typeof(byte));
                        else dataTable.Columns.Add(field.Name, typeof(int));
                    else
                        dataTable.Columns.Add(field.Name, field.PropertyType);
                }

                dataTable.Rows.Add(fields.Select(x => x.GetValue(parameter, null)).ToArray());
            }

            return dataTable.AsTableValuedParameter(typeName);
        }
    }
}