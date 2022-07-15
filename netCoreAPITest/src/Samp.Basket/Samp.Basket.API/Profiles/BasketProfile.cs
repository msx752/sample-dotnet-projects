using AutoMapper;
using Samp.Basket.API.Models.Dtos;
using Samp.Basket.Database.Entities;

namespace Samp.Movie.API.Profiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketEntity, BasketDto>();
        }
    }
}