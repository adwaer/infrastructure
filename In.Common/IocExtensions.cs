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
            Type type, params Assembly[] assemblies)
        {
            return services.Scan(
                x =>
                    x.FromAssemblies(assemblies)
                        .AddClasses(classes => classes.AssignableTo(type))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());
        }
        
        public static IServiceCollection RegisterAssemblyImplementationsTransient(this IServiceCollection services,
            Type type, params Assembly[] assemblies)
        {
            return services.Scan(
                x =>
                    x.FromAssemblies(assemblies)
                        .AddClasses(classes => classes.AssignableTo(type))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime());
        }
        
        public static IServiceCollection RegisterAssemblyImplementationsSingleton(this IServiceCollection services,
            Type type, params Assembly[] assemblies)
        {
            return services.Scan(
                x =>
                    x.FromAssemblies(assemblies)
                        .AddClasses(classes => classes.AssignableTo(type))
                        .AsImplementedInterfaces()
                        .WithSingletonLifetime());
        }
        
    }
}