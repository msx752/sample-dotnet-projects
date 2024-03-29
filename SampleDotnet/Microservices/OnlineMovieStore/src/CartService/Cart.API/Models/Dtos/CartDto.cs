﻿using Cart.Database.Enums;
using Newtonsoft.Json;
using SampleProject.Basket.API.Models.Dtos;

namespace SampleProject.Cart.API.Models.Dtos
{
    public class CartDto
    {
        public CartDto()
        {
            Items = new List<CartItemDto>();
        }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("status")]
        private CartStatus Satus { get; set; }

        [JsonProperty("items")]
        public List<CartItemDto> Items { get; set; }
    }
}