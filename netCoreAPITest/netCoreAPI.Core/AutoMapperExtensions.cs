using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace netCoreAPI.Core
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
            var config = new MapperConfiguration(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));

            services.AddSingleton(config.CreateMapper());

            return services;
        }
    }
}