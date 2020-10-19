using System;
using Bat.Core;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Bat.EntityFrameworkCore
{
    public static class DbSetExtention
    {
        public static DbContext GetDbContext<TEntity>(this DbSet<TEntity> dbSet) where TEntity : class
        {
            var infrastructure = dbSet as IInfrastructure<IServiceProvider>;
            var serviceProvider = infrastructure.Instance;
            var currentDbContext = serviceProvider.GetService(typeof(ICurrentDbContext)) as ICurrentDbContext;
            return currentDbContext.Context;
        }

        public static string SerializeDbSetToJson(this object entry)
        {
            object fieldValue;
            try
            {
                var Json = "{";
                var record = entry.GetType().GetProperties();
                foreach (var field in record)
                {
                    if (!field.PropertyType.IsSerializable) continue;
                    if (field.PropertyType.Name == "Binary") continue;
                    fieldValue = field.GetValue(entry);
                    Json += ("\"" + field.Name + "\":\"" + (fieldValue == null ? string.Empty : fieldValue.ToString()) + "\",");
                }
                return Json.Substring(0, Json.Length - 1) + "}";
            }
            catch
            {
                return ("{ \"Serializing Error\" : \"true\" }");
            }
        }

        public static void SaveEventLog<TEntity>(this DbSet<TEntity> dbSet) where TEntity : class, new()
        {
            try
            {
                var dbContext = dbSet.GetDbContext();
                if (dbSet is IEventLogProperties)
                {
                    dbContext.Add(dbSet);
                    dbContext.SaveChanges();
                }
            }
            catch { }
        }

        public static IQueryable<TEntity> ExecuteQuery<TEntity>(this DbSet<TEntity> dbSet, string sql, params object[] parameters) where TEntity : class
            => dbSet.FromSqlRaw(sql, parameters);

        public static IEnumerable<TEntity> ExecuteQueryList<TEntity>(this DbSet<TEntity> dbSet, string sql, params object[] parameters) where TEntity : class
            => dbSet.FromSqlRaw(sql, parameters).ToList();

        public async static Task<IEnumerable<TEntity>> ExecuteQueryListAsync<TEntity>(this DbSet<TEntity> dbSet, string sql, params object[] parameters) where TEntity : class
            => await dbSet.FromSqlRaw(sql, parameters).ToListAsync();
    }
}