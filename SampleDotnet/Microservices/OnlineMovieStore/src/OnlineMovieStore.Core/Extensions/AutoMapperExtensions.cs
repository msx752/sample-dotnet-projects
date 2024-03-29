﻿using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace SampleProject.Core.Extensions
{
    public static class AutoMapperExtensions
    {
        /// <summary>
        /// https://docs.automapper.org/en/stable/Configuration.html?highlight=profiles#assembly-scanning-for-auto-configuration
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEntityMapper(this IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                var assemblies = Utility.GetLoadedAssemblies(f =>
                    !f.IsDynamic
                    && f.DefinedTypes.Any(x => x.IsAssignableTo(typeof(AutoMapper.Profile))
                    && f.FullName != typeof(AutoMapper.Profile).Assembly.FullName)
                );

                cfg.AddMaps(assemblies);
            });

            services.AddSingleton(config.CreateMapper());

            return services;
        }
    }
}