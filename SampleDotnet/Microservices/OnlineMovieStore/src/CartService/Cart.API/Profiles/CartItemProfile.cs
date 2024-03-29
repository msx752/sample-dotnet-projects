﻿using AutoMapper;
using Cart.Database.Entities;
using SampleProject.Basket.API.Models.Dtos;
using SampleProject.Contract.Payment.Cart;

namespace SampleProject.Cart.API.Profiles
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