using Samp.Movie.Database.Enums;

namespace Samp.Movie.API.Models.Dtos
{
    public class MovieDto
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public int RuntimeMinutes { get; set; }

        public int StartYear { get; set; }

        public string Description { get; set; }
        public RatingDto Rating { get; set; }
        public MovieType Type { get; set; }

        public decimal UsdPrice { get; set; }
        public string ItemDatabase { get; set; }
    }
}