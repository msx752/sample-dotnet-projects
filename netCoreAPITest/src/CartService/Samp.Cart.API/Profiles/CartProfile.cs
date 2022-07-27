using AutoMapper;
using Samp.Basket.Database.Entities;
using Samp.Cart.API.Models.Dtos;
using Samp.Contract.Payment.Cart;

namespace Samp.Cart.API.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartEntity, CartDto>();
            CreateMap<CartEntity, CartEntityResponseMessage>();
        }
    }
}