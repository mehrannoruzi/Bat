using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bat.EntityFrameworkCore.Tools
{
    public static class ModelBuilderExtension
    {
        public static void OverrideMaxLength(this ModelBuilder modelBuilder, Type propertyType, int maxLength = 500)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entity.GetProperties().Where(p => p.ClrType == propertyType))
                    property.SetMaxLength(maxLength);
            }
        }

        public static void OverrideDeleteBehavior(this ModelBuilder modelBuilder, DeleteBehavior deleteBehaviour = DeleteBehavior.Restrict)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = deleteBehaviour;
        }

        public static void OverrideSqlDefaultValue(this ModelBuilder modelBuilder, Type propertyType, string sqlDefaultValue)
        {
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties().Where(p => p.ClrType == propertyType))
                    property.SetDefaultValueSql(sqlDefaultValue);
            }
        }

        public static void OverrideGuidToSequentialGuid(this ModelBuilder modelBuilder)
        {
            modelBuilder.OverrideSqlDefaultValue(typeof(Guid), "NEWSEQUENTIALID()");
        }

        public static void RegisterAllEntities<BaseType>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(a => a.GetExportedTypes())
                            .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(BaseType).IsAssignableFrom(c));

            foreach (var type in types)
                modelBuilder.Entity(type);
        }

        public static void RegisterEntityTypeConfiguration(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
                modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            //MethodInfo applyGenericMethod = typeof(ModelBuilder).GetMethods().First(m => m.Name == nameof(ModelBuilder.ApplyConfiguration));
            //IEnumerable<Type> types = assemblies.SelectMany(a => a.GetExportedTypes())
            //                                    .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic);
            //foreach (var type in types)
            //{
            //    foreach (var iface in type.GetInterfaces())
            //    {
            //        if (iface.IsConstructedGenericType && iface.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
            //        {
            //            MethodInfo applyConcreteMethod = applyGenericMethod.MakeGenericMethod(iface.GenericTypeArguments[0]);
            //            applyConcreteMethod.Invoke(modelBuilder, new object[] { Activator.CreateInstance(type) });
            //        }
            //    }
            //}
        }
    }
}