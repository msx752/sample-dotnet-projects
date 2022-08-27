using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Samp.Core.Extensions
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
                    && f.DefinedTypes.Any(x => x.IsAssignableTo(typeof(AutoMapper.Profile)))
                );

                cfg.AddMaps(assemblies);
            });

            services.AddSingleton(config.CreateMapper());

            return services;
        }
    }
}