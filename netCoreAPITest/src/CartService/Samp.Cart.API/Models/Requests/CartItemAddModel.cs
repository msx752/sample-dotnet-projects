using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Samp.Cart.API.Models.Requests
{
    public class CartItemAddModel
    {
        [Required]
        public string ProductId { get; set; }

        [Required]
        public string ProductDatabase { get; set; }
    }
}