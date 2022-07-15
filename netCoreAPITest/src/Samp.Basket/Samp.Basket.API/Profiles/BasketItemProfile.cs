using AutoMapper;
using Samp.Basket.API.Models.Dtos;
using Samp.Basket.Database.Entities;

namespace Samp.Movie.API.Profiles
{
    public class BasketItemProfile : Profile
    {
        public BasketItemProfile()
        {
            CreateMap<BasketItemEntity, BasketItemDto>();
        }
    }
}