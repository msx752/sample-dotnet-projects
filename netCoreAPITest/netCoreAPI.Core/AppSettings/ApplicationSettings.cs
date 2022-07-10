namespace netCoreAPI.Core.AppSettings
{
    public class ApplicationSettings
    {
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public string ASPNETCORE_ENVIRONMENT { get; set; }
        public JWT JWT { get; set; }
    }
}