namespace Samp.Movie.API.Models.Dtos
{
    public class RatingDto
    {
        public Guid Id { get; set; }
        public double AverageRating { get; set; }
        public int NumVotes { get; set; }
        public string MovieId { get; set; }
    }
}