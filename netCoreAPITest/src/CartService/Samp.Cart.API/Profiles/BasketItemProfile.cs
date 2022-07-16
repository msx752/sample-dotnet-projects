using AutoMapper;
using Samp.Basket.API.Models.Dtos;
using Samp.Cart.Database.Entities;

namespace Samp.Cart.API.Profiles
{
    public class BasketItemProfile : Profile
    {
        public BasketItemProfile()
        {
            CreateMap<CartItemEntity, CartItemDto>();
        }
    }
}