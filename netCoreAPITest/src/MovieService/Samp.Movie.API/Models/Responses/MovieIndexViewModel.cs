using Samp.Movie.API.Models.Dtos;

namespace Samp.Movie.API.Models.Responses
{
    public class MovieIndexViewModel
    {
        public MovieIndexViewModel()
        {
            HighRatings = new();
            All = new();
            RecentlyAdded = new();
        }

        public List<MovieDto> HighRatings { get; set; }
        public List<MovieDto> All { get; set; }

        public List<MovieDto> RecentlyAdded { get; set; }
    }
}