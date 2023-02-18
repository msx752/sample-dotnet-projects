namespace SampleProject.Core.AppSettings
{
    public class ApplicationSettings
    {
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public string ASPNETCORE_ENVIRONMENT { get; set; }
        public JwtBearerOptions JwtBearerOptions { get; set; }
        public RabbitMqOptions RabbitMqOptions { get; set; }
    }
}