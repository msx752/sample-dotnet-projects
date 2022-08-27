﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.Core.Database;
using SampleProject.Core.Interfaces.DbContexts;
using System;

namespace SampleProject.Core.Extensions
{
    public static class ContextSeedExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="contextSeeds"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static IServiceCollection AddDbContextSeed(this IServiceCollection services, params Type[] contextSeeds)
        {
            if (contextSeeds.Length == 0)
                return services;

            foreach (var item in contextSeeds)
            {
                if (item == null)
                    continue;

                if (!typeof(IContextSeed).IsAssignableFrom(item))
                {
                    throw new Exception($"{item.FullName} must implement {typeof(ContextSeed).FullName}");
                }

                services.AddScoped(typeof(IContextSeed), item);
            }

            return services;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseContextSeed(this IApplicationBuilder application)
        {
            using (var scope = application.ApplicationServices.CreateScope())
            {
                var seeds = scope.ServiceProvider.GetServices<IContextSeed>();

                foreach (var seed in seeds)
                {
                    seed.Execute();
                }
            }
            return application;
        }
    }
}