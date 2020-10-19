using System;
using Bat.Core;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Bat.Di
{
    public static class MicrosoftIocExtention
    {
        public static IServiceCollection AddBatDynamicTransient(this IServiceCollection services, Assembly assembly)
        {
            var allAssembly = new List<Assembly>();
            var allAssemblyNames = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName.Contains(assembly.FullName.Split('.')[0]) || x.FullName.Contains("Bat"))
                .OrderBy(x => x.FullName).ToList();

            allAssemblyNames.ForEach(x => allAssembly.Add(Assembly.Load(x.FullName)));

            var types = allAssembly.SelectMany(a => a.GetExportedTypes())
                .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(ITransientInjection).IsAssignableFrom(c)).ToList();

            foreach (var type in types)
            {
                var typeInterface = type.GetTypeInfo().ImplementedInterfaces.FirstOrDefault(x => x.Name.Contains(type.Name));
                if (type.IsGenericType)
                    services.AddTransient(typeInterface.GetGenericTypeDefinition(), type.GetGenericTypeDefinition());
                else
                {
                    if (typeInterface != null)
                        services.AddTransient(typeInterface, type);
                    else
                        services.AddTransient(type);
                }
            };

            return services;
        }

        public static IServiceCollection AddBatDynamicScoped(this IServiceCollection services, Assembly assembly)
        {
            var allAssembly = new List<Assembly>();
            var allAssemblyNames = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName.Contains(assembly.FullName.Split('.')[0]) || x.FullName.Contains("Bat"))
                .OrderBy(x => x.FullName).ToList();

            allAssemblyNames.ForEach(x => allAssembly.Add(Assembly.Load(x.FullName)));

            var types = allAssembly.SelectMany(a => a.GetExportedTypes())
                .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(IScopedInjection).IsAssignableFrom(c)).ToList();

            foreach (var type in types)
            {
                var typeInterface = type.GetTypeInfo().ImplementedInterfaces.FirstOrDefault(x => x.Name.Contains(type.Name));
                if (type.IsGenericType)
                    services.AddScoped(typeInterface.GetGenericTypeDefinition(), type.GetGenericTypeDefinition());
                else
                {
                    if (typeInterface != null)
                        services.AddScoped(typeInterface, type);
                    else
                        services.AddScoped(type);
                }
            };

            return services;
        }

        public static IServiceCollection AddBatDynamicSingleton(this IServiceCollection services, Assembly assembly)
        {
            var allAssembly = new List<Assembly>();
            var allAssemblyNames = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName.Contains(assembly.FullName.Split('.')[0]) || x.FullName.Contains("Bat"))
                .OrderBy(x => x.FullName).ToList();

            allAssemblyNames.ForEach(x => allAssembly.Add(Assembly.Load(x.FullName)));

            var types = allAssembly.SelectMany(a => a.GetExportedTypes())
                .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(ISingletonInjection).IsAssignableFrom(c)).ToList();

            foreach (var type in types)
            {
                var typeInterface = type.GetTypeInfo().ImplementedInterfaces.FirstOrDefault(x => x.Name.Contains(type.Name));
                if (type.IsGenericType)
                    services.AddSingleton(typeInterface.GetGenericTypeDefinition(), type.GetGenericTypeDefinition());
                else
                {
                    if (typeInterface != null)
                        services.AddSingleton(typeInterface, type);
                    else
                        services.AddSingleton(type);
                }
            };

            return services;
        }
    }
}