using AutoMapper;
using Samp.Basket.API.Models.Dtos;
using Samp.Cart.Database.Entities;
using Samp.Contract.Payment.Cart;

namespace Samp.Cart.API.Profiles
{
    public class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<CartItemEntity, CartItemDto>();
            CreateMap<CartItemEntity, CartItemEntityResponseMessage>();
        }
    }
}