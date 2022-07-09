using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using netCoreAPI.Models.Dtos;
using netCoreAPI.Models.Entities;
using netCoreAPI.Models.Requests;
using System;

namespace netCoreAPI
{
    public static class MapperExtensions
    {
        public static IServiceCollection AddEntityMapper(this IServiceCollection services)
        {
            //models have been binded by auto mapper
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PersonalEntity, PersonalDto>();
                cfg.CreateMap<PersonalRequest, PersonalEntity>();
            });
            services.AddSingleton<IMapper>(mapperConfiguration.CreateMapper());

            return services;
        }
    }
}