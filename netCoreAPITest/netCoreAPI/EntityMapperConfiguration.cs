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
                cfg.CreateMap<Personal, PersonalDto>();
                cfg.CreateMap<PersonalModel, Personal>();
            });
            services.AddSingleton<IMapper>(mapperConfiguration.CreateMapper());

            return services;
        }
    }
}