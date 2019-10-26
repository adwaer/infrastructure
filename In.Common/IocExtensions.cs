using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace In.Common
{
    public static class IocExtensions
    {
        public static IServiceCollection RegisterAssemblyImplementationsScoped(this IServiceCollection services,
            Assembly[] assemblies, Type type)
        {
            return services.Scan(
                x =>
                    x.FromAssemblies(assemblies)
                        .AddClasses(classes => classes.AssignableTo(type))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());
        }
        
        public static IServiceCollection RegisterAssemblyImplementationsSingleton(this IServiceCollection services,
            Assembly[] assemblies, Type type)
        {
            return services.Scan(
                x =>
                    x.FromAssemblies(assemblies)
                        .AddClasses(classes => classes.AssignableTo(type))
                        .AsImplementedInterfaces()
                        .WithSingletonLifetime());
        }

        public static IServiceCollection AddScopedGenerics(this IServiceCollection services,
            Assembly[] assemblies, Type baseType, Type[] excludeTypes = null)
        {
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract);
                if (excludeTypes != null)
                {
                    types = types.Except(excludeTypes);
                }

                foreach (var type in types)
                {
                    var bases = new List<Type>();
                    bases.AddRange(type.GetInterfaces());
                    bases.AddRange(GetBaseTypes(type));

                    foreach (var i in bases)
                    {
                        if (i.IsGenericType && i.GetGenericTypeDefinition() == baseType)
                        {
                            // NOTE: Due to a limitation of Microsoft.DependencyInjection we cannot 
                            // register an open generic interface type without also having an open generic 
                            // implementation type. So, we convert to a closed generic interface 
                            // type to register.
                            var interfaceType = baseType.MakeGenericType(i.GetGenericArguments());
                            services.AddScoped(interfaceType, type);
                        }
                    }
                }
            }

            return services;
        }

        private static IEnumerable<Type> GetBaseTypes(Type type)
        {
            List<Type> baseClasses = new List<Type>();

            if (type.BaseType != typeof(object))
            {
                baseClasses.AddRange(GetBaseTypes(type.BaseType));
                baseClasses.Add(type.BaseType);
            }

            return baseClasses;
        }
    }
}