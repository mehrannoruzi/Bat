﻿using System;
using DryIoc;
using Bat.Core;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Bat.Di
{
    public static class DryIocContainerExtension
    {
        public static void AddBatDryIocDynamicTransient(this IRegistrator container, Assembly assembly)
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
                    container.Register(typeInterface.GetGenericTypeDefinition(), type.GetGenericTypeDefinition(), Reuse.Transient);
                else
                {
                    if (typeInterface != null)
                        container.Register(typeInterface, type, Reuse.Transient);
                    else
                        container.Register(type, Reuse.Transient);
                }
            };
        }

        public static void AddBatDryIocDynamicScoped(this IRegistrator container, Assembly assembly)
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
                    container.Register(typeInterface.GetGenericTypeDefinition(), type.GetGenericTypeDefinition(), Reuse.Scoped);
                else
                {
                    if (typeInterface != null)
                        container.Register(typeInterface, type, Reuse.Scoped);
                    else
                        container.Register(type, Reuse.Scoped);
                }
            };
        }

        public static void AddBatDryIocDynamicSingleton(this IRegistrator container, Assembly assembly)
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
                    container.Register(typeInterface.GetGenericTypeDefinition(), type.GetGenericTypeDefinition(), Reuse.Singleton);
                else
                {
                    if (typeInterface != null)
                        container.Register(typeInterface, type, Reuse.Singleton);
                    else
                        container.Register(type, Reuse.Singleton);
                }
            };
        }
    }
}