﻿using System;
using Autofac;
using Bat.Core;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Bat.Di
{
    public static class AutofacIocExtention
    {
        public static ContainerBuilder AddBatAutofacDynamicTransient(this ContainerBuilder container, Assembly assembly)
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
                    container.RegisterGeneric(type).As(typeInterface).InstancePerDependency();
                else
                {
                    if (typeInterface != null)
                        container.RegisterType(type).As(typeInterface).InstancePerDependency();
                    else
                        container.RegisterType(type).InstancePerDependency();
                }
            };

            return container;
        }

        public static ContainerBuilder AddBatAutofacDynamicScoped(this ContainerBuilder container, Assembly assembly)
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
                    container.RegisterGeneric(type).As(typeInterface).InstancePerLifetimeScope();
                else
                {
                    if (typeInterface != null)
                        container.RegisterType(type).As(typeInterface).InstancePerLifetimeScope();
                    else
                        container.RegisterType(type).InstancePerLifetimeScope();
                }
            };

            return container;
        }

        public static ContainerBuilder AddBatAutofacDynamicSingleton(this ContainerBuilder container, Assembly assembly)
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
                    container.RegisterGeneric(type).As(typeInterface).SingleInstance();
                else
                {
                    if (typeInterface != null)
                        container.RegisterType(type).As(typeInterface).SingleInstance();
                    else
                        container.RegisterType(type).SingleInstance();
                }
            };

            return container;
        }
    }
}