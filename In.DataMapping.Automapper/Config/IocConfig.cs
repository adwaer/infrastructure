using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using In.Common;
using Microsoft.Extensions.DependencyInjection;

namespace In.DataMapping.Automapper.Config
{
    public static class IocConfig
    {
        public static IServiceCollection AddMappingServices(this IServiceCollection services, Assembly[] assemblies)
        {
            var mappingProfiles = assemblies.SelectMany(a =>
                a.DefinedTypes.Where(t => t.BaseType == typeof(Profile) && !t.IsAbstract && t.IsPublic));

            foreach (var mappingProfile in mappingProfiles)
            {
                services.AddTransient(typeof(Profile), mappingProfile);
            }

            return services.AddSingleton<IConfigurationProvider>(provider => new MapperConfiguration(cfg =>
                {
                    foreach (var profile in provider.GetService<IEnumerable<Profile>>())
                        cfg.AddProfile(profile);
                }))
                .AddScoped(typeof(IMapperService), typeof(DefaultMapperService))
                .AddAutoMapper(assemblies);
        }
    }
}