using AutoMapper;
using Cart.Database.Entities;
using SampleProject.Cart.API.Models.Dtos;
using SampleProject.Contract.Payment.Cart;

namespace SampleProject.Cart.API.Profiles
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