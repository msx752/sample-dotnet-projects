namespace SampleProject.Contract.Cart.Movie
{
    public class MovieEntityResponseMessage : IResponseMessage
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int RuntimeMinutes { get; set; }
        public int StartYear { get; set; }
        public string Description { get; set; }
        public Guid RatingId { get; set; }
        public string Type { get; set; }
        public string ProductDatabase { get; set; }
        public double UsdPrice { get; set; }
        public string BusErrorMessage { get; set; }
    }
}