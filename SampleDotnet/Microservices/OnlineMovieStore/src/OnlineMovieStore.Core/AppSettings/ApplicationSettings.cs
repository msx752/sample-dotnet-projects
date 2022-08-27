namespace SampleProject.Core.AppSettings
{
    public class ApplicationSettings
    {
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public string ASPNETCORE_ENVIRONMENT { get; set; }
        public JWTOptions JWTOptions { get; set; }
        public RabbitMqOptions RabbitMqOptions { get; set; }
    }
}