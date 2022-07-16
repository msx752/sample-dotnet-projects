using AutoMapper;
using Samp.Basket.Database.Entities;
using Samp.Cart.API.Models.Dtos;

namespace Samp.Cart.API.Profiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CartEntity, BasketDto>();
        }
    }
}