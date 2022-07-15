using Samp.Basket.Database.Enums;

namespace Samp.Basket.API.Models.Dtos
{
    public class BasketDto
    {
        public BasketDto()
        {
            Items = new List<BasketItemDto>();
        }

        public Guid Id { get; set; }
        private BasketStatus Satus { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}